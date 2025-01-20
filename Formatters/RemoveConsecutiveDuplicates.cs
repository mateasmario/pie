using System.Linq;
using System.Collections.Generic;

public class RemoveConsecutiveDuplicates {
    public string format(string text) {
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