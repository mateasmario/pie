using System.Linq;

public class DuplicateLines {
    public string format(string text) {
        string[] lines = text.Split('\n');
        return string.Join("\n", lines.Select(line => string.Format("{0}\n{0}", line))); 
    }
}