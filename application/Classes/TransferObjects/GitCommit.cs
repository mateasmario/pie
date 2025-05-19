/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

namespace pie.Classes
{
    internal class GitCommit
    {
        public string Id { get; set; }
        public string Author { get; set; }
        public string Date { get; set; }
        public string Message { get; set; }
    }
}
