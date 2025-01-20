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
using System.Linq;
using System.Reflection;
using pie.Enums;
using pie.Exceptions;

namespace pie.Classes
{
    public class DynamicLibraryValidator
    {
        public DynamicLibraryValidatorFlag[] Flags { get; private set; }
        public string MethodName { get; private set; }
        public int MethodCount { get; private set; }
        public int MethodParameterCount { get; private set; }
        public Type MethodReturnType { get; private set; }

        private DynamicLibraryValidator(DynamicLibraryValidatorFlag[] flags, string methodName, int methodCount, int methodParameterCount, Type methodReturnType) {
            Flags = flags;
            MethodName = methodName;
            MethodCount = methodCount;
            MethodParameterCount = methodParameterCount;
            MethodReturnType = methodReturnType;
        }

        public void Validate(Type externalType)
        {
            List<DynamicLibraryValidatorFlag> flagList = Flags.OfType<DynamicLibraryValidatorFlag>().ToList();

            // Validate number of methods: should be only one public
            MethodInfo[] methodInfos = externalType.GetMethods();

            if (flagList.Contains(DynamicLibraryValidatorFlag.VALIDATE_METHOD_COUNT))
            {
                int count = 0;
                foreach (MethodInfo methodInfo in methodInfos)
                {
                    if (methodInfo.IsPublic)
                    {
                        count++;
                        if (count > MethodCount)
                        {
                            throw new IncorrectPublicMethodCountException();
                        }
                    }
                }
            }

            // Validate method name
            MethodInfo method = externalType.GetMethod(MethodName);

            if (method == null)
            {
                throw new IncorrectPublicMethodNameException();
            }

            // Validate method return type: must be TYPE string, not CLASS String
            if (flagList.Contains(DynamicLibraryValidatorFlag.VALIDATE_METHOD_RETURN_TYPE) && method.ReturnType != MethodReturnType)
            {
                throw new IncorrectPublicMethodReturnTypeException();
            }

            // Validate method parameter number (must be one)
            if (flagList.Contains(DynamicLibraryValidatorFlag.VALIDATE_METHOD_PARAMETER_COUNT) && method.GetParameters().Length != MethodParameterCount)
            {
                throw new IncorrectPublicMethodArgumentNumberException();
            }
        }

        public class Builder
        {
            private DynamicLibraryValidatorFlag[] Flags { get; set; }
            private string MethodName { get; set; }
            private int MethodCount { get; set; }
            private int MethodParameterCount { get; set; }
            private Type MethodReturnType { get; set; }

            public Builder WithFlags(DynamicLibraryValidatorFlag[] flags)
            {
                Flags = flags;
                return this;
            }

            public Builder WithMethodName(string methodName)
            {
                MethodName = methodName;
                return this;
            }

            public Builder WithMethodCount(int methodCount)
            {
                MethodCount = methodCount;
                return this;
            }

            public Builder WithMethodParameterCount(int methodParameterCount)
            {
                MethodParameterCount = methodParameterCount;
                return this;
            }

            public Builder WithMethodReturnType(Type methodReturnType)
            {
                MethodReturnType = methodReturnType;
                return this;
            }

            public DynamicLibraryValidator Build()
            {
                return new DynamicLibraryValidator(Flags, MethodName, MethodCount, MethodParameterCount, MethodReturnType);
            }
        }
    }
}
