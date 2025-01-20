using System.Linq;

public class SwitchLowercaseWithUppercase {
    public string format(string text) {
        string result = "";

        for(int i = 0; i < text.Length; i++)
        {
            if (char.IsLetter(text[i]))
            {
                if (text[i] == char.ToLower(text[i]))
                {
                    result += char.ToUpper(text[i]);
                }
                else
                {
                    result += char.ToLower(text[i]);
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