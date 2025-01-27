using System.Linq;

public class ConvertTextToUppercase {
    public string Category() {
        return "Character";
    }
    
    public string Description() {
        return "Converts every character to uppercase.";
    }
    
    public string Format(string text) {
        string[] lines = text.Split('\n');
        return string.Join("\n", lines.Select(line => line.ToUpper()));
    }
}