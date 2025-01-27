using System.Linq;

public class DuplicateLines {
    public string Category() {
        return "Line";
    }
    
    public string Description() {
        return "Duplicates every line.";
    }
    
    public string Format(string text) {
        string[] lines = text.Split('\n');
        return string.Join("\n", lines.Select(line => string.Format("{0}\n{0}", line)));
    }
}