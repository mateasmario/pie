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
using pie.Classes;
using pie.Classes.ConfigurationEntities;

/**
 * Used for reading and writing to JSON config files
 * 
 * Copyright (c) 2007 James Newton-King
 */
using Newtonsoft.Json;
using System.Reflection;
using pie.Classes.Exceptions;

namespace pie.Services
{
    public class ConfigurationService<T> where T: ConfigurationEntity, new()
    {
        public static List<T> GetFromFile(string file)
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

                            if (!TokenIsValidProperty(token))
                            {
                                throw new InvalidPropertyNameException();
                            }
                        }
                        else
                        {
                            if (TokenTypeMatchesPropertyType(jsonTextReader.TokenType, token))
                            {
                                PropertyInfo propertyInfo = GetProperty(token);

                                if (propertyInfo == null)
                                {
                                    throw new InvalidPropertyNameException();
                                }
                                else
                                {
                                    propertyInfo.SetValue(configurationEntity, jsonTextReader.Value);

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

        internal static void WriteToFile(string file, List<T> configurationEntities)
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

        private static bool TokenIsValidProperty(string token)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach(PropertyInfo propertyInfo in properties) {
                if (propertyInfo.Name.ToLower() == token.ToLower())
                {
                    return true;
                }
            }

            return false;
        }

        private static bool TokenTypeMatchesPropertyType(JsonToken tokenType, string token)
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

                    return false;
                }
            }

            return false;
        }

        private static PropertyInfo GetProperty(string token)
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

        private static bool AllPropertiesAreFilled(T configurationEntity)
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
    }
}
