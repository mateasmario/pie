using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
