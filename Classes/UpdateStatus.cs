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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace pie.Classes
{
    public class UpdateStatus
    {
        public bool NeedsUpdate { get; set; }
        public string Version { get; set; }

        public UpdateStatus(bool needsUpdate)
        {
            NeedsUpdate = needsUpdate;
        }

        public UpdateStatus(bool needsUpdate, string version)
        {
            NeedsUpdate = needsUpdate;
            Version = version;
        }
    }
}
