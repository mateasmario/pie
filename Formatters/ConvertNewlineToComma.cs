using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

public class ConvertNewlineToComma {
    public string format(string text) {
        string[] lines = text.Split('\n');

        for (int i = 0; i<lines.Length; i++)
        {
            if (lines[i].Length-1 >= 0 && lines[i][lines[i].Length-1] == '\r')
            {
                lines[i] = lines[i].Substring(0, lines[i].Length-1);
            }
        }

        return string.Join(",", lines.Select(line => line.ToLower()));
    }
}