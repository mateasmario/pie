using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

public class TrimLines {
    public string format(string text) {
        string[] words = text.Split('\n', ' ');

        return string.Join("", words.Select(word => word.Length > 0 ? 
                        word.Substring(0, 1).ToUpper() + word.Substring(1) +
                        ((word.Substring(word.Length-1) == "\r") ? "" : " ") : ""));
    }
}