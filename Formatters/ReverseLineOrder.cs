using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

public class ReverseLineOrder {
    public string format(string text) {
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