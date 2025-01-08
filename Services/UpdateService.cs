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
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Threading.Tasks;

using pie.Classes;

namespace pie.Services
{
    public class UpdateService
    {
        public static UpdateStatus GetUpdateStatus()
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

        private static async Task<ReleaseInfo> GetAsync(string uri)
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
