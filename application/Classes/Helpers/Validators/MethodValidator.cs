/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

using pie.Classes.Exceptions;
using System;
using System.Reflection;

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
