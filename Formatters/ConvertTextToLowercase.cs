/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

using System.Linq;

public class ConvertTextToLowercase {
    public string Category() {
        return "Character";
    }
    
    public string Description() {
        return "Converts every character to lowercase.";
    }
    
    public string Format(string text) {
        string[] lines = text.Split('\n');
        if (lines.Length == 1)
        {
            return lines[0].ToLower();
        }
        return string.Join("\n", lines.Select(line => line.ToLower()));
    }
}