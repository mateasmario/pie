/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

using System.Linq;

public class SwitchLowercaseWithUppercase {
    public string Category() {
        return "Character";
    }
    
    public string Description() {
        return "Switches lowercase with uppercase characters.";
    }
    
    public string Format(string text) {
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