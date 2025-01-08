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
using System.IO.Compression;
using System.Net;

namespace pie
{
    public class PieUpdater
    {
        static void Main()
        {
            using (var client = new WebClient())
            {
                Console.WriteLine("Downloading latest release from GitHub...");
                client.DownloadFile("https://github.com/mateasmario/pie/releases/latest/download/Release.zip", "Release.zip");
                ZipArchive archive = ZipFile.OpenRead("Release.zip");
                foreach(ZipArchiveEntry entry in archive.Entries)
                {
                    Console.WriteLine("Extracting " + entry.FullName + "...");
                    if (entry.Name != "updater.exe")
                    {
                        entry.ExtractToFile(entry.FullName);
                    }
                }
            }
        }
    }
}
