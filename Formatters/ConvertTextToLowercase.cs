using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

public class ConvertTextToLowercase {
    public string format(string text) {
        string[] lines = text.Split('\n');
        if (lines.Length == 1)
        {
            return lines[0].ToLower();
        }
        return string.Join("\n", lines.Select(line => line.ToLower()));
    }
}