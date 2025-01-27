using System.Linq;

public class CapitalizeFirstCharacterFromEveryLine {
    public string Category() {
        return "Character";
    }
    
    public string Description() {
        return "Capitalizes first character from every new line.";
    }
    
    public string Format(string text) {
        string[] lines = text.Split('\n');
        return string.Join("\n", lines.Select(line => line.Length > 0 ? line.Substring(0, 1).ToUpper() + line.Substring(1) : line));
    }
}