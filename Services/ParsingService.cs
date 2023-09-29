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
using System.Drawing;
using System.IO;

namespace pie.Services
{
    public class ParsingService
    {
        public static string GetFolderName(string path)
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

        public static string GetFileName(string path)
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

        public static string RemoveFileExtension(string path)
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

        public static string GetFileExtension(string fileName)
        {
            return fileName.Substring(fileName.LastIndexOf('.')+1);
        }

        public static Color IntToColor(int rgb)
        {
            return Color.FromArgb(255, (byte)(rgb >> 16), (byte)(rgb >> 8), (byte)rgb);
        }

        public static string GetContentFromFile(string path)
        {
            return File.ReadAllText(path);
        }

        public static string GoBackInFilePath(string path)
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
