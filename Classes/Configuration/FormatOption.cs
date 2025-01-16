

using pie.Enums;
using System;
using System.Reflection;
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
namespace pie.Classes
{
    public class FormatOption
    {
        public string FormatOptionName { get; set; }
        public FormatOptionCategory FormatOptionCategory { get; set; }
        public string FormatOptionDescription { get; set; }
        public object Instance { get; set; }
        public MethodInfo MethodInfo { get; set; }

        public FormatOption(string formatOptionName, FormatOptionCategory formatOptionCategory, string formatOptionDescription)
        {
            this.FormatOptionName = formatOptionName;
            this.FormatOptionCategory = formatOptionCategory;
            this.FormatOptionDescription = formatOptionDescription;
        }

        public FormatOption(string formatOptionName, FormatOptionCategory formatOptionCategory, string formatOptionDescription, object instance, MethodInfo methodInfo)
        {
            this.FormatOptionName = formatOptionName;
            this.FormatOptionCategory = formatOptionCategory;
            this.FormatOptionDescription = formatOptionDescription;
            this.Instance = instance;
            this.MethodInfo = methodInfo;
        }

        public string InvokeMethod(string argument)
        {
            return (string)this.MethodInfo.Invoke(this.Instance, new object[] { argument });
        }
    }
}
