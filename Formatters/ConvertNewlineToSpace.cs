using System.Linq;

public class ConvertNewlineToSpace {
    public string format(string text) {
        string[] lines = text.Split('\n');

        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].Length - 1 >= 0 && lines[i][lines[i].Length - 1] == '\r')
            {
                lines[i] = lines[i].Substring(0, lines[i].Length - 1);
            }
        }

        return string.Join(" ", lines.Select(line => line.ToLower()));
    }
}