using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

public class CapitalizeFirstCharacterFromEveryLine {
    public string format(string text) {
        string[] lines = text.Split('\n');
        return string.Join("\n", lines.Select(line => line.Length > 0 ? line.Substring(0, 1).ToUpper() + line.Substring(1) : line));
    }
}