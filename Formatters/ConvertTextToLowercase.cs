using System.Linq;

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