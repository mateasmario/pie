using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pie.Services
{
    public class FormattingService
    {
        public static string DuplicateLines(string text)
        {
            string[] lines = text.Split('\n');

            return lines.Aggregate((curr, next) => {
                if (!curr.Contains("\n"))
                {
                    return curr + "\n" + curr + "\n" + next + "\n" + next;
                }
                else
                {
                    return curr + "\n" + next + "\n" + next;
                }
            });
        }

        public static string AddEmptyRowBetweenEachLine(string text)
        {
            string[] lines = text.Split('\n');

            return lines.Aggregate((curr, next) => {
                return curr + "\n\n" + next;
            });
        }

        public static string CapitalizeFirstCharacterFromEveryLine(string text)
        {
            string[] lines = text.Split('\n');

            return lines.Aggregate((curr, next) => {
                return curr.Substring(0, 1).ToUpper() + curr.Substring(1) + "\n" + (next.Length >= 1 ? (next.Substring(0, 1).ToUpper() + next.Substring(1)) : next);
            });
        }

        public static string RemoveEmptyLines(string text)
        {
            string[] lines = text.Split('\n');

            string result = lines.Aggregate((curr, next) => {
                return (((curr.Length == 1 && curr[0] == '\r') || (curr.Length == 0)) ? "" : curr) + (((next.Length == 1 && next[0] == '\r') || (next.Length == 0)) ? "" : "\n" + next);
            });


            if (result[result.Length-1] == '\r')
            {
                return result.Substring(0, result.Length - 1);
            }
            else
            {
                return result;
            }
        }

        public static string RemoveWhitespaceLines(string text)
        {
            string[] lines = text.Split('\n');

            string result = lines.Aggregate((curr, next) => {
                return (String.IsNullOrWhiteSpace(curr) ? "" : curr) + (String.IsNullOrWhiteSpace(next) ? "" : "\n" + next);
            });

            if (result[result.Length - 1] == '\r')
            {
                return result.Substring(0, result.Length - 1);
            }
            else
            {
                return result;
            }
        }

        public static string RemoveDuplicateLines(string text)
        {
            text = text + "\r";

            string[] lines = text.Split('\n').Distinct().ToArray();

            string result = lines.Aggregate((curr, next) => {
                return curr + "\n" + next;
            });

            return result.Substring(0, result.Length - 1);
        }

        public static string RemoveConsecutiveDuplicates(string text)
        {
            text = text + "\r";

            List<string> results = new List<string>();

            string[] lines = text.Split('\n');

            foreach (var element in lines)
            {
                if (results.Count == 0 || !results.Last().Equals(element))
                    results.Add(element);
            }

            string result = results.Aggregate((curr, next) => {
                return curr + "\n" + next;
            });

            return result.Substring(0, result.Length - 1);
        }

        public static string TrimLines(string text)
        {
            string[] lines = text.Split('\n');

            return lines.Aggregate((curr, next) => {
                return curr.Trim() + "\n" + next.Trim();
            });
        }

        public static string CapitalizeEveryWord(string text)
        {
            string[] lines = text.Split('\n', ' ');

            if (lines.Length >= 1)
            {
                lines[0] = lines[0].ToUpper();
            }

            bool isNewLine = false;

            return lines.Aggregate((curr, next) => {
                string res;

                if (curr[curr.Length-1] == '\r')
                {
                    isNewLine = true;
                }

                if (isNewLine)
                {
                    res = curr + next.Substring(0, 1).ToUpper() + next.Substring(1);
                }
                else
                {
                    res = curr + " " + next.Substring(0, 1).ToUpper() + next.Substring(1);
                }

                if (next[next.Length - 1] == '\r')
                {
                    isNewLine = true;
                }
                else
                {
                    isNewLine = false;
                }

                return res;
            });
        }

        public static string ConvertTextToUppercase(string text)
        {
            string[] lines = text.Split('\n');

            return lines.Aggregate((curr, next) => {
                return curr.ToUpper() + "\n" + next.ToUpper();
            });
        }

        public static string ConvertTextToLowercase(string text)
        {
            string[] lines = text.Split('\n');

            return lines.Aggregate((curr, next) => {
                return curr.ToLower() + "\n" + next.ToLower();
            });
        }

        public static string SwitchLowercaseWithUppercase(string text)
        {
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

        public static string RemoveAllWhitespaces(string text)
        {
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

        public static string RemoveAllConsecutiveWhitespaces(string text)
        {
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

        public static string SortLines(string text)
        {
            return SortLines(text, true);
        }

        public static string SortLines (string text, bool ascending)
        {
            string result = "";

            List<string> lines = text.Split('\n').ToList();

            lines.Sort();

            if (!ascending)
            {
                lines.Reverse();
            }

            for(int i = 0; i<lines.Count; i++)
            {
                if (i == lines.Count - 1)
                {
                    result += lines[i];
                }
                else
                {
                    result += lines[i] + "\n";
                }
            }

            if (result[result.Length - 1] == '\r')
            {
                return result.Substring(0, result.Length - 1);
            }
            else
            {
                return result;
            }

            return result;
        }

        public static string ReverseLineOrder(string text)
        {
            string result = "";

            List<string> lines = text.Split('\n').ToList();

            lines.Reverse();

            for (int i = 0; i < lines.Count; i++)
            {
                if (i == lines.Count - 1)
                {
                    result += lines[i];
                }
                else
                {
                    result += lines[i] + "\n";
                }
            }

            if (result[result.Length - 1] == '\r')
            {
                return result.Substring(0, result.Length - 1);
            }
            else
            {
                return result;
            }

            return result;
        }
    }
}
