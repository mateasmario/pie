/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

using System.Linq;

public class ConvertNewlineToSpace {
    public string Category() {
        return "Line";
    }
    
    public string Description() {
        return "Replaces every new line with a space.";
    }
    
    public string Format(string text) {
        string[] lines = text.Split('\n');

        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].Length - 1 >= 0 && lines[i][lines[i].Length - 1] == '\r')
            {
                lines[i] = lines[i].Substring(0, lines[i].Length - 1);
            }
        }

        return string.Join(" ", lines.Select(line => line.ToLower()));
    }
}