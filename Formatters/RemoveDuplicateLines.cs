using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

public class RemoveWhitespaceLines {
    public string format(string text) {
            text = text + "\r";

            string[] lines = text.Split('\n').Distinct().ToArray();

            string result = string.Join("\n", lines);

            return result.Substring(0, result.Length - 1);
    }
}