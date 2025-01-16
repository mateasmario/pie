/** Copyright (C) 2023  Mario-Mihai Mateas
 * 
 * This file is part of pie.
 * 
 * pie is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * 
 * along with this program.  If not, see <https://www.gnu.org/licenses/>. 
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Drawing;
using pie.Classes.ConfigurationEntities;
using pie.Classes.Exceptions;

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

        public List<T> GetFromFile<T>(string file) where T : ConfigurationEntity, new()
        {
            List<T> configurationEntities = new List<T>();

            T configurationEntity = new T();

            string token = null;

            using (var textReader = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + file))
            {
                JsonTextReader jsonTextReader = new JsonTextReader(textReader);

                while (jsonTextReader.Read())
                {
                    if (jsonTextReader.Value != null)
                    {
                        if (jsonTextReader.TokenType == JsonToken.PropertyName)
                        {
                            token = jsonTextReader.Value.ToString();

                            if (!TokenIsValidProperty<T>(token))
                            {
                                throw new InvalidPropertyNameException();
                            }
                        }
                        else
                        {
                            if (TokenTypeMatchesPropertyType<T>(jsonTextReader.TokenType, token))
                            {
                                PropertyInfo propertyInfo = GetProperty<T>(token);

                                if (propertyInfo == null)
                                {
                                    throw new InvalidPropertyNameException();
                                }
                                else
                                {
                                    MapConfigurationEntityPropertyToTokenValue(configurationEntity, propertyInfo, jsonTextReader);

                                    if (AllPropertiesAreFilled(configurationEntity))
                                    {
                                        configurationEntities.Add(configurationEntity);
                                        configurationEntity = new T();
                                    }
                                }
                            }
                            else
                            {
                                throw new InvalidPropertyTypeException();
                            }
                        }
                    }
                }
            }

            return configurationEntities;
        }

        public T GetSingleFromFile<T>(string file) where T : ConfigurationEntity, new()
        {
            T configurationEntity = new T();

            string token = null;

            using (var textReader = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + file))
            {
                JsonTextReader jsonTextReader = new JsonTextReader(textReader);

                while (jsonTextReader.Read())
                {
                    if (jsonTextReader.Value != null)
                    {
                        if (jsonTextReader.TokenType == JsonToken.PropertyName)
                        {
                            token = jsonTextReader.Value.ToString();

                            if (!TokenIsValidProperty<T>(token))
                            {
                                throw new InvalidPropertyNameException();
                            }
                        }
                        else
                        {
                            if (TokenTypeMatchesPropertyType<T>(jsonTextReader.TokenType, token))
                            {
                                PropertyInfo propertyInfo = GetProperty<T>(token);

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
                }
            }

            return configurationEntity;
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
                    writer.WriteValue(propertyInfo.GetValue(configurationEntity));
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
                    
                    foreach(PropertyInfo propertyInfo in properties)
                    {
                        writer.WritePropertyName(propertyInfo.Name);
                        writer.WriteValue(propertyInfo.GetValue(configurationEntity));
                    }

                    writer.WriteEndObject();
                }

                writer.WriteEndArray();
            }
        }

        public List<T> LoadFromFolder<T>(string directory, string extension) where T : MultiFileConfigurationEntity, new()
        {
            List<T> configurationList = new List<T>();

            T configuration = null;

            string[] files = null;

            try
            {
                files = Directory.GetFiles(directory);

                foreach (string file in files)
                {
                    if (parsingService.GetFileExtension(file) == extension)
                    {
                        configurationList.Add(GetSingleFromFile<T>(file));
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

        private bool TokenIsValidProperty<T>(string token) where T : ConfigurationEntity, new()
        {
            return typeof(T).GetProperty(token.Substring(0, 1).ToUpper() + token.Substring(1)) != null;
        }

        private bool TokenTypeMatchesPropertyType<T>(JsonToken tokenType, string token) where T : ConfigurationEntity, new()
        {
            PropertyInfo[] properties = typeof(T).GetProperties();

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

                    return false;
                }
            }

            return false;
        }

        private PropertyInfo GetProperty<T>(string token) where T : ConfigurationEntity, new()
        {
            PropertyInfo[] properties = typeof(T).GetProperties();

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
            // Treat normal cases (Strings, Integers, etc.)
            else
            {
                propertyInfo.SetValue(configurationEntity, jsonTextReader.Value);
            }
        }

        private bool AllPropertiesAreFilled<T>(T configurationEntity) where T : ConfigurationEntity, new()
        {
            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (PropertyInfo propertyInfo in properties)
            {
                if (propertyInfo.GetValue(configurationEntity) == null)
                {
                    return false;
                }
            }

            return true;
        }

        public void WriteFilesToDirectory<T>(string directory, List<T> items) where T : MultiFileConfigurationEntity, new()
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
                WriteToFile(directory + item.Name, item);
            }
        }

        private string ExtractRGBFromColor(Color color)
        {
            return color.R + ", " + color.G + ", " + color.B;
        }
    }
}
