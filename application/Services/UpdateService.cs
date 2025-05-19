/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Threading.Tasks;

using pie.Classes;

namespace pie.Services
{
    public class UpdateService
    {
        public UpdateStatus GetUpdateStatus()
        {
            // Send GET Request
            ReleaseInfo releaseInfo = Task.Run(async () => await GetAsync("https://api.github.com/repos/mateasmario/pie/releases/latest")).Result;
            string remoteVersion = releaseInfo.Version;
            Version assemblyVersion = Assembly.GetEntryAssembly().GetName().Version;

            if (!assemblyVersion.ToString().Equals(remoteVersion.ToString()) 
                && (!assemblyVersion.ToString().StartsWith(remoteVersion)))
            {
                return new UpdateStatus(true, remoteVersion);
            }

            return new UpdateStatus(false);
        }

        private async Task<ReleaseInfo> GetAsync(string uri)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd("request");
                return await httpClient.GetFromJsonAsync<ReleaseInfo>(uri);
            } catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}
