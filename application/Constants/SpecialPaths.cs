/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

using System.IO;

namespace pie.Constants
{
    public class SpecialPaths { 
        public static readonly string AppData = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData).ToString(), "Pie");
        public static readonly string Config = Path.Combine(AppData, "config");
        public static readonly string Themes = Path.Combine(Config, "themes");
        public static readonly string Languages = Path.Combine(Config, "languages");
        public static readonly string CrashReport = Path.Combine(AppData, "crashreport.txt");
    }
}
