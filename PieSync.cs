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
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading;

namespace pie
{
    public class PieSync
    {
        static void Main()
        {
            try
            {
                using (var client = new WebClient())
                {
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    Console.WriteLine("piesync: Downloading latest release from GitHub...");
                    client.DownloadFile("https://github.com/mateasmario/pie/releases/latest/download/Release.zip", "Release.zip");
                    Console.WriteLine("piesync: Download successful");

                    ExtractArchive("Release.zip");

                    File.Delete("Release.zip");
                }
            } catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.WriteLine("piesync: Update successful. Pie will now start.");

            Thread.Sleep(2000);

            Process.Start("pie.exe");
        }

        private static void ExtractArchive(string inputPath)
        {
            using (ZipArchive archive = ZipFile.OpenRead(inputPath))
            {
                foreach(ZipArchiveEntry archiveEntry in archive.Entries)
                {
                    Console.WriteLine("piesync: Syncing " + archiveEntry.FullName + "...");

                    if (archiveEntry.FullName.EndsWith("/") || archiveEntry.FullName.EndsWith("\\"))
                    {
                        if (Directory.Exists(archiveEntry.FullName))
                        {
                            Directory.Delete(archiveEntry.FullName.Substring(0, archiveEntry.FullName.Length - 1), true);
                        }

                        Directory.CreateDirectory(archiveEntry.FullName.Substring(0, archiveEntry.FullName.Length - 1));
                    }
                    else if (archiveEntry.Name != "piesync.exe")
                    {
                        if (File.Exists(archiveEntry.FullName) && archiveEntry.Name != "piesync.exe")
                        {
                            File.Delete(archiveEntry.FullName);
                        }

                        archiveEntry.ExtractToFile(archiveEntry.FullName);
                    }
                }

                Console.WriteLine("piesync: All files synced");
            }
        }
    }
}
