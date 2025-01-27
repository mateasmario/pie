using System.Linq;

public class RemoveAllConsecutiveWhitespaces {
    public string Category() {
        return "Character";
    }
    
    public string Description() {
        return "Removes all duplicate whitespaces.";
    }

    public string Format(string text) {
        string result = "";

        for (int i = 0; i < text.Length; i++)
        {
            if (char.IsWhiteSpace(text[i]))
            {
                if (result.Length == 0 || !char.IsWhiteSpace(result[result.Length - 1]) || text[i] == '\n' || text[i] == '\r')
                {
                    result += text[i];
                }
            }
            else
            {
                result += text[i];
            }
        }

        return result;
    }
}