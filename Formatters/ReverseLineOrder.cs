/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

using System.Linq;
using System.Collections.Generic;

public class ReverseLineOrder {
    public string Category() {
        return "Sort";
    }
    
    public string Description() {
        return "Reverses line order.";
    }
    
    public string Format(string text) {
        if (text.Equals(""))
        {
            return "";
        }

        string result = "";

        List<string> lines = text.Split('\n').ToList();

        lines.Reverse();

        for (int i = 0; i < lines.Count; i++)
        {
            if (i == lines.Count - 1)
            {
                result += lines[i];
            }
            else
            {
                result += lines[i] + "\n";
            }
        }

        if (result[result.Length - 1] == '\r')
        {
            return result.Substring(0, result.Length - 1);
        }
        else
        {
            return result;
        }
    }
}