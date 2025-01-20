using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

public class AddEmptyRowBetweenEachLine {
    public string format(string text) {
        string[] lines = text.Split('\n');
        return string.Join("\n", lines.Select(line => string.Format("{0}\n", line)));
    }
}