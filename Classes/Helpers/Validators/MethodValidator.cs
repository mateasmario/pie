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
using System.Reflection;
using pie.Exceptions;

namespace pie.Classes
{
    public class MethodValidator
    {
        public string MethodName { get; private set; }
        public int MethodParameterCount { get; private set; }
        public Type MethodReturnType { get; private set; }

        private MethodValidator(string methodName, int methodParameterCount, Type methodReturnType) {
            MethodName = methodName;
            MethodParameterCount = methodParameterCount;
            MethodReturnType = methodReturnType;
        }

        public void Validate(Type externalType)
        {
            // Validate method name
            MethodInfo method = externalType.GetMethod(MethodName);

            if (method == null)
            {
                throw new IncorrectPublicMethodNameException();
            }

            // Validate method return type: must be TYPE string, not CLASS String
            if (method.ReturnType != MethodReturnType)
            {
                throw new IncorrectPublicMethodReturnTypeException();
            }

            // Validate method parameter number (must be one)
            if (method.GetParameters().Length != MethodParameterCount)
            {
                throw new IncorrectPublicMethodArgumentNumberException();
            }
        }

        public class Builder
        {
            private string MethodName { get; set; }
            private int MethodParameterCount { get; set; }
            private Type MethodReturnType { get; set; }

            public Builder WithMethodName(string methodName)
            {
                MethodName = methodName;
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

            public MethodValidator Build()
            {
                return new MethodValidator(MethodName, MethodParameterCount, MethodReturnType);
            }
        }
    }
}
