/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

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

                        archiveEntry.ExtractToFile(Path.GetFullPath(archiveEntry.FullName));
                    }
                }

                Console.WriteLine("piesync: All files synced");
            }
        }
    }
}
