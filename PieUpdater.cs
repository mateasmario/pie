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
    public class PieUpdater
    {
        static void Main()
        {
            try
            {
                using (var client = new WebClient())
                {
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    Console.WriteLine("[PieUpdater] Downloading latest release from GitHub...");
                    client.DownloadFile("https://github.com/mateasmario/pie/releases/latest/download/Release.zip", "Release.zip");
                    Console.WriteLine("[PieUpdater] Download successful");

                    ExtractArchive("Release.zip");

                    File.Delete("Release.zip");
                }
            } catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.WriteLine("[PieUpdater] Update successful. Pie will now start.");

            Thread.Sleep(2000);

            Process.Start("pie.exe");
        }

        private static void ExtractArchive(string inputPath)
        {
            using (ZipArchive archive = ZipFile.OpenRead(inputPath))
            {
                Console.WriteLine("[PieUpdater] Deleting old files...");

                foreach(ZipArchiveEntry archiveEntry in archive.Entries)
                {
                    Console.WriteLine("[PieUpdater] Deleting " + archiveEntry.FullName + "...");
                    if (File.Exists(archiveEntry.FullName))
                    {
                        File.Delete(archiveEntry.FullName);
                    }
                    if (Directory.Exists(archiveEntry.FullName))
                    {
                        Directory.Delete(archiveEntry.FullName, true);
                    }
                }

                Console.WriteLine("[PieUpdater] Old files deleted successfully");

                Console.WriteLine("[PieUpdater] Extracting files from downloaded archive...");
                archive.ExtractToDirectory(".");
                Console.WriteLine("[PieUpdater] Files extracted successfully");
            }
        }
    }
}
