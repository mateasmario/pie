/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Drawing;
using pie.Classes.ConfigurationEntities;
using pie.Classes.Exceptions;
using pie.Classes;

/**
 * Used for reading and writing to JSON config files
 * 
 * Copyright (c) 2007 James Newton-King
 */
using Newtonsoft.Json;

namespace pie.Services
{
    public class ConfigurationService
    {
        private ParsingService parsingService = new ParsingService();

        public List<T> GetArrayFromFile<T>(string file) where T : ConfigurationEntity, new()
        {
            List<T> configurationEntities = new List<T>();

            using (var textReader = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + file))
            {
                JsonTextReader jsonTextReader = new JsonTextReader(textReader);

                do
                {
                    T configurationEntity = new T();
                    ReadPropertiesFromJson(jsonTextReader, configurationEntity);

                    if (jsonTextReader.TokenType != JsonToken.None)
                    {
                        configurationEntities.Add(configurationEntity);
                    }
                    else
                    {
                        break;
                    }
                } while (jsonTextReader.TokenType != JsonToken.EndArray);
            }

            return configurationEntities;
        }

        public T GetObjectFromFile<T>(string file) where T : ConfigurationEntity, new()
        {
            T configurationEntity = new T();

            using (var textReader = File.OpenText(file))
            {
                JsonTextReader jsonTextReader = new JsonTextReader(textReader);
                ReadPropertiesFromJson(jsonTextReader, configurationEntity);
            }

            return configurationEntity;
        }

        public List<T> GetArrayFromMultipleFiles<T>(string directory, string extension) where T : MultiFileConfigurationEntity, new()
        {
            List<T> configurationList = new List<T>();

            string[] files = null;

            try
            {
                files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + directory);

                foreach (string file in files)
                {
                    if (parsingService.GetFileExtension(file) == extension)
                    {
                        configurationList.Add(GetObjectFromFile<T>(file));
                        configurationList[configurationList.Count - 1].Name = parsingService.RemoveFileExtension(parsingService.GetFileName(file));
                    }
                }

                return configurationList;
            }
            catch (DirectoryNotFoundException)
            {
                return configurationList;
            }
        }

        private void ReadPropertiesFromJson(JsonTextReader jsonTextReader, ConfigurationEntity configurationEntity)
        {
            string token = null;
            bool firstItem = true;

            do
            {
                jsonTextReader.Read();

                if (jsonTextReader.Value != null || (jsonTextReader.TokenType == JsonToken.StartObject && !firstItem))
                {
                    if (jsonTextReader.TokenType == JsonToken.PropertyName)
                    {
                        token = jsonTextReader.Value.ToString();
                        firstItem = false;

                        if (!TokenIsValidProperty(token, configurationEntity.GetType()))
                        {
                            throw new InvalidPropertyNameException();
                        }
                    }
                    else
                    {
                        if (TokenTypeMatchesPropertyType(jsonTextReader.TokenType, token, configurationEntity.GetType()))
                        {
                            PropertyInfo propertyInfo = GetProperty(token, configurationEntity.GetType());

                            if (propertyInfo == null)
                            {
                                throw new InvalidPropertyNameException();
                            }
                            else
                            {
                                MapConfigurationEntityPropertyToTokenValue(configurationEntity, propertyInfo, jsonTextReader);
                            }
                        }
                        else
                        {
                            throw new InvalidPropertyTypeException();
                        }
                    }

                }


            } while (jsonTextReader.TokenType != JsonToken.EndObject && jsonTextReader.TokenType != JsonToken.None);

            jsonTextReader.Read();
        }

        private bool TokenIsValidProperty(string token, Type type)
        {
            return type.GetProperty(token.Substring(0, 1).ToUpper() + token.Substring(1)) != null;
        }

        private bool TokenTypeMatchesPropertyType(JsonToken tokenType, string token, Type type)
        {
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo propertyInfo in properties)
            {
                if (propertyInfo.Name.ToLower() == token.ToLower())
                {
                    if (propertyInfo.PropertyType == typeof(int) && tokenType == JsonToken.Integer)
                    {
                        return true;
                    }
                    else if (propertyInfo.PropertyType == typeof(string) && tokenType == JsonToken.String)
                    {
                        return true;
                    }
                    else if (propertyInfo.PropertyType == typeof(Color) && tokenType == JsonToken.String)
                    {
                        return true;
                    }
                    else if (propertyInfo.PropertyType == typeof(bool) && tokenType == JsonToken.Boolean)
                    {
                        return true;
                    }
                    else if (propertyInfo.PropertyType.IsEnum && tokenType != JsonToken.StartObject && tokenType != JsonToken.StartArray)
                    {
                        return true;
                    }
                    else if (propertyInfo.PropertyType is object && tokenType == JsonToken.StartObject)
                    {
                        return true;
                    }

                    return false;
                }
            }

            return false;
        }

        private PropertyInfo GetProperty(string token, Type type)
        {
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo propertyInfo in properties)
            {
                if (propertyInfo.Name.ToLower() == token.ToLower())
                {
                    return propertyInfo;
                }
            }

            return null;
        }

        private void MapConfigurationEntityPropertyToTokenValue(ConfigurationEntity configurationEntity, PropertyInfo propertyInfo, JsonTextReader jsonTextReader)
        {
            // Treat special cases
            if (propertyInfo.PropertyType == typeof(Color))
            {
                string colorValue = jsonTextReader.Value.ToString();

                string[] rgbValues = colorValue.Split(',');

                int red, green, blue;
                int.TryParse(rgbValues[0].Trim(), out red);
                int.TryParse(rgbValues[1].Trim(), out green);
                int.TryParse(rgbValues[2].Trim(), out blue);

                propertyInfo.SetValue(configurationEntity, Color.FromArgb(red, green, blue));
            }
            else if (propertyInfo.PropertyType == typeof(string) || propertyInfo.PropertyType == typeof(bool))
            {
                propertyInfo.SetValue(configurationEntity, jsonTextReader.Value);
            }
            else if (propertyInfo.PropertyType == typeof(int))
            {
                propertyInfo.SetValue(configurationEntity, Convert.ToInt32(jsonTextReader.Value));
            }
            else if (propertyInfo.PropertyType.IsEnum)
            {
                propertyInfo.SetValue(configurationEntity, Enum.Parse(propertyInfo.PropertyType, jsonTextReader.Value.ToString()));
            }
            // Treat nested object properties
            else if (propertyInfo.PropertyType is object)
            {
                object instance = Activator.CreateInstance(propertyInfo.PropertyType);

                ReadPropertiesFromJson(jsonTextReader, (ConfigurationEntity)instance);

                propertyInfo.SetValue(configurationEntity, instance);
            }
        }

        public List<T> LoadLinkLibrariesFromMultipleFiles<T>(string directory, MethodValidator nameValidator) where T : DynamicLibraryConfigurationEntity, new()
        {
            List<T> dlls = new List<T>();

            T dll = null;

            string[] files = null;

            try
            {
                files = Directory.GetFiles(directory);

                foreach (string file in files)
                {
                    if (parsingService.GetFileExtension(file) == "dll")
                    {
                        Assembly externalAssembly = Assembly.LoadFrom(file);
                        string className = parsingService.RemoveFileExtension(parsingService.GetFileName(file));
                        Type externalType = externalAssembly.GetType(className);
                        object instance = Activator.CreateInstance(externalType);

                        if (externalType != null)
                        {
                            // Validate
                            MethodInfo method = externalType.GetMethod(nameValidator.MethodName);
                            dll = new T();
                            dll.Name = className;
                            dll.Instance = instance;
                            dll.MethodInfo = method;

                            MethodValidator descriptionValidator = new MethodValidator.Builder()
                                .WithMethodName("Description")
                                .WithMethodParameterCount(0)
                                .WithMethodReturnType(typeof(string))
                                .Build();

                            descriptionValidator.Validate(externalType);
                            dll.Description = (string)externalType.GetMethod(descriptionValidator.MethodName).Invoke(instance, new object[] { });

                            MethodValidator categoryValidator = new MethodValidator.Builder()
                                .WithMethodName("Category")
                                .WithMethodParameterCount(0)
                                .WithMethodReturnType(typeof(string))
                                .Build();

                            categoryValidator.Validate(externalType);
                            dll.Category = (string)externalType.GetMethod(categoryValidator.MethodName).Invoke(instance, new object[] { });

                            dlls.Add(dll);
                        }
                    }
                }

                return dlls;

            }
            catch (DirectoryNotFoundException)
            {
                return dlls;
            }
        }

        public void WriteFilesToDirectory<T>(string directory, List<T> items, string extension) where T : MultiFileConfigurationEntity, new()
        {
            DirectoryInfo di = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + directory);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }


            foreach (T item in items)
            {
                WriteToFile(directory + "/" + item.Name + "." + extension, item);
            }
        }

        public void WriteToFile<T>(string file, T configurationEntity) where T : ConfigurationEntity, new()
        {
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + file, "");

            TextWriter textWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + file);

            using (JsonWriter writer = new JsonTextWriter(textWriter))
            {
                writer.Formatting = Formatting.Indented;

                writer.WriteStartArray();

                PropertyInfo[] properties = typeof(T).GetProperties();

                writer.WriteStartObject();

                foreach (PropertyInfo propertyInfo in properties)
                {
                    writer.WritePropertyName(propertyInfo.Name);

                    if (propertyInfo.PropertyType == typeof(Color))
                    {
                        Color c = (Color)propertyInfo.GetValue(configurationEntity);
                        writer.WriteValue($"{c.R}, {c.G}, {c.B}");
                    }
                    else
                    {
                        writer.WriteValue(propertyInfo.GetValue(configurationEntity));
                    }
                }

                writer.WriteEndObject();
            }
        }

        public void WriteToFile<T>(string file, List<T> configurationEntities) where T : ConfigurationEntity, new()
        {
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + file, "");

            TextWriter textWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + file);

            using (JsonWriter writer = new JsonTextWriter(textWriter))
            {
                writer.Formatting = Formatting.Indented;

                writer.WriteStartArray();

                PropertyInfo[] properties = typeof(T).GetProperties();

                foreach (T configurationEntity in configurationEntities)
                {
                    writer.WriteStartObject();

                    foreach (PropertyInfo propertyInfo in properties)
                    {
                        writer.WritePropertyName(propertyInfo.Name);
                        writer.WriteValue(propertyInfo.GetValue(configurationEntity));
                    }

                    writer.WriteEndObject();
                }

                writer.WriteEndArray();
            }
        }
    }
}
