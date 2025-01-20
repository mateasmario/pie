using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

public class RemoveAllConsecutiveWhitespaces {
    public string format(string text) {
        string result = "";

        for (int i = 0; i < text.Length; i++)
        {
            if (char.IsWhiteSpace(text[i]))
            {
                if (result.Length == 0 || !char.IsWhiteSpace(result[result.Length - 1]) || text[i] == '\n' || text[i] == '\r')
                {
                    result += text[i];
                }
            }
            else
            {
                result += text[i];
            }
        }

        return result;
    }
}