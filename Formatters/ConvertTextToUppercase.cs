using System.Linq;

public class ConvertTextToUppercase {
    public string format(string text) {
        string[] lines = text.Split('\n');
        return string.Join("\n", lines.Select(line => line.ToUpper()));
    }
}