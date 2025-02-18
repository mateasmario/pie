/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

using System.Linq;
using System.Collections.Generic;

public class RemoveConsecutiveDuplicates {
    public string Category() {
        return "Character";
    }
    
    public string Description() {
        return "Removes all consecutive duplicates.";
    }
    
    public string Format(string text) {
        text = text + "\r";

        List<string> results = new List<string>();

        string[] lines = text.Split('\n');

        foreach (var element in lines)
        {
            if (results.Count == 0 || !results.Last().Equals(element))
                results.Add(element);
        }

        string result = string.Join("\n", results);

        return result.Substring(0, result.Length - 1);
    }
}