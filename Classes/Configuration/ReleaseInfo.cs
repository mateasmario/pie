﻿/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace pie.Classes
{
    public class ReleaseInfo
    {
        [JsonPropertyName("name")]
        public string Version { get; set; }
        [JsonPropertyName("tag_name")]
        public string TagName { get; set; }
    }
}
