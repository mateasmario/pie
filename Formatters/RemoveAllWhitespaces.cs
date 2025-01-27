using System.Linq;

public class RemoveAllWhitespaces {
    public string Category() {
        return "Character";
    }
    
    public string Description() {
        return "Removes all whitespaces.";
    }
    
    public string Format(string text) {
        string result = "";

        for (int i = 0; i < text.Length; i++)
        {
            if (char.IsWhiteSpace(text[i]))
            {
                if (text[i] == '\n' || text[i] == '\r')
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