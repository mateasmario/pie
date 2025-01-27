using System.Linq;

public class AddEmptyRowBetweenEachLine {
    public string Category() {
        return "Line";
    }
    
    public string Description() {
        return "Adds an empty row between each line.";
    }

    public string Format(string text) {
        string[] lines = text.Split('\n');
        return string.Join("\n", lines.Select(line => string.Format("{0}\n", line)));
    }
}