/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

using System;
using System.IO;
using System.Linq;
using pie.Classes.Exceptions;

namespace pie.Services
{
    public class SecureFileService
    {
        private static string[] DISALLOWED_PATHS = {
            Environment.GetFolderPath(Environment.SpecialFolder.Windows),
            Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
            Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86),
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
            Environment.GetFolderPath(Environment.SpecialFolder.InternetCache),
            Environment.GetFolderPath(Environment.SpecialFolder.Favorites),
            Environment.GetFolderPath(Environment.SpecialFolder.AdminTools),
            Environment.GetFolderPath(Environment.SpecialFolder.CommonAdminTools),
            Environment.GetFolderPath(Environment.SpecialFolder.Cookies),
            Environment.GetFolderPath(Environment.SpecialFolder.History),
            Environment.GetFolderPath(Environment.SpecialFolder.System),
            Environment.GetFolderPath(Environment.SpecialFolder.SystemX86)
        };

        private ParsingService parsingService = new ParsingService();

        public void CreateFile(string path, string content)
        {
            CheckPath(path);
            CheckExtension(parsingService.GetFileExtension(path));

            FileStream fileStream = File.Create(path);
            fileStream.Close();

            File.WriteAllText(path, content);
        }

        public void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                CheckPath(path);
                Directory.CreateDirectory(path);
            }
        }

        public void CheckPath(string path)
        {
            if (DISALLOWED_PATHS.Any(disallowedPath => Path.GetFullPath(path).StartsWith(disallowedPath)))
            {
                throw new ForbiddenPathException("For security purposes, plugins are not allowed to create files and directories in special locations.");
            }
        }

        private void CheckExtension(string extension)
        {
            if (extension.Equals("exe") || extension.Equals("dll") || extension.Equals("bat") || extension.Equals("cmd"))
            {
                throw new ForbiddenFileTypeException("For security purposes, plugins are not allowed to create executable files (.exe), scripts (.cmd, .bat) and dynamic link libraries (.dll).");
            }
        }
    }
}
