/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

using System.Linq;

public class RemoveDuplicateLines {
    public string Category() {
        return "Line";
    }
    
    public string Description() {
        return "Removes all duplicate lines.";
    }
    
    public string Format(string text) {
            text = text + "\r";

            string[] lines = text.Split('\n').Distinct().ToArray();

            string result = string.Join("\n", lines);

            return result.Substring(0, result.Length - 1);
    }
}