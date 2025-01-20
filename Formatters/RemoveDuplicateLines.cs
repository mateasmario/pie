using System.Linq;

public class RemoveDuplicateLines {
    public string format(string text) {
            text = text + "\r";

            string[] lines = text.Split('\n').Distinct().ToArray();

            string result = string.Join("\n", lines);

            return result.Substring(0, result.Length - 1);
    }
}