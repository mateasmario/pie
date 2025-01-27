using System.Linq;

public class RemoveEmptyLines {
    public string Category() {
        return "Line";
    }
    
    public string Description() {
        return "Removes all empty lines.";
    }
    
    public string Format(string text) {
        string[] lines = text.Split('\n');

        string result = string.Join("", lines.Select(line => ((line.Length == 1 && line[0] == '\r') || (line.Length == 0)) ? "" : line));


        if (result.Length-1 >= 0 && result[result.Length-1] == '\r')
        {
            return result.Substring(0, result.Length - 1);
        }
        else
        {
            return result;
        }
    }
}