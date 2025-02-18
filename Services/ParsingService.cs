/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

using System;
using System.Drawing;
using System.IO;

namespace pie.Services
{
    public class ParsingService
    {
        public string GetFolderName(string path)
        {
            if (path == null)
            {
                throw new NullReferenceException();
            }

            int length = path.Length;
            int indexOfSlash = -1;
            for (int i = length-1; i>0; i--)
            {
                if (path[i] == '\\')
                {
                    indexOfSlash = i;
                    break;
                }
            }

            if (indexOfSlash == -1)
            {
                return path;
            }
            else
            {
                return path.Substring(0, indexOfSlash+1);
            }
        }

        public string GetFileName(string path)
        {
            if (path == null)
            {
                throw new NullReferenceException();
            }

            int length = path.Length;
            int indexOfSlash = -1;

            for (int i =0;i<length;i++)
            {
                if (path[i] == '\\')
                {
                    indexOfSlash=i;
                }
            }

            if (indexOfSlash==-1)
            {
                return path;
            }
            else
            {
                return path.Substring(indexOfSlash+1);
            }
        }

        public string RemoveFileExtension(string path)
        {
            if (path == null)
            {
                throw new NullReferenceException();
            }

            int length = path.Length;
            int indexOfDot = -1;

            for (int i = length-1; i>0; i--)
            {
                if (path[i] == '.')
                {
                    indexOfDot = i;
                    break;
                }
            }

            if (indexOfDot == -1)
            {
                return path;
            }
            else
            {
                return path.Substring(0, indexOfDot);
            }
        }

        public string GetFileExtension(string fileName)
        {
            return fileName.Substring(fileName.LastIndexOf('.')+1);
        }

        public Color IntToColor(int rgb)
        {
            return Color.FromArgb(255, (byte)(rgb >> 16), (byte)(rgb >> 8), (byte)rgb);
        }

        public string GetContentFromFile(string path)
        {
            return File.ReadAllText(path);
        }

        public string GoBackInFilePath(string path)
        {
            if (path[path.Length - 1] == '\\')
            {
                path = path.Substring(0, path.Length - 1);
            }

            int lastSlashIndex = path.LastIndexOf('\\');
            string result = path.Substring(0, lastSlashIndex+1);


            return result;
        }
    }
}
