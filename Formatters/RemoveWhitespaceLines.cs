using System.Linq;

public class RemoveWhitespaceLines {
    public string Category() {
        return "Line";
    }
    
    public string Description() {
        return "Removes all lines containing whitespaces.";
    }
    
    public string Format(string text) {
        string[] lines = text.Split('\n');

        string result = string.Join("", lines.Select(line => string.IsNullOrWhiteSpace(line) ? "" : line));

        if (result.Length-1 >= 0 && result[result.Length - 1] == '\r')
        {
            return result.Substring(0, result.Length - 1);
        }
        else
        {
            return result;
        }
    }
}