using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public static class _1
{

    public static int mySolution(string input)
    {
        var lines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

        List<int> values = new List<int>();
        foreach (var line in lines)
        {
            values.Add(getLineValue2(line));
        }
        return values.Sum();



    }

    static int getLineValue(string line)
    {
        string[] words = line.Select(c => c.ToString()).ToArray();
        int? num1 = null;
        int? num2 = null;

        for (int i = 0; i < words.Length; i++)
        {
            int val;
            if(num1 == null)
            {
                var s = int.TryParse(words[i], out val);
                if(s)
                {
                    num1 = val;
                }
            }
            else
            {
                var s = int.TryParse(words[i], out val);
                if (s)
                {
                    num2 = val;
                }

            }
        }
       
        string retrunVal = "";
        retrunVal += num1.ToString();
        if(num2 != null)
        {
            retrunVal += num2.ToString();
        }
        else
        {
            retrunVal += num1.ToString();
        }
        var myVal = int.Parse(retrunVal);

        Console.WriteLine(myVal);
        return myVal;
    }


    static int getLineValue2(string line)
    {

        var words = SplitIntoWordsAndDigits(line);

        int? num1 = null;
        int? num2 = null;

        for (int i = 0; i < words.Length; i++)
        {
            int val;
            if (num1 == null)
            {
                var s = expandedTryParse(words[i], out val);
                if (s)
                {
                    num1 = val;
                }
            }
            else
            {
                var s = expandedTryParse(words[i], out val);
                if (s)
                {
                    num2 = val;
                }

            }
        }

        {
            string retrunVal = "";
            retrunVal += num1.ToString();
            if (num2 != null)
            {
                retrunVal += num2.ToString();
            }
            else
            {
                retrunVal += num1.ToString();
            }
            var myVal = int.Parse(retrunVal);

            Console.WriteLine(myVal);
            return myVal;
        }
    }
    static bool expandedTryParse(string word, out int value)
    {
        var s = int.TryParse(word, out value);
        if(s)
        {
            return true;
        }

        if(word == "one")
        {
            value = 1;
            return true;
        }
        if (word == "two")
        {
            value = 2;
            return true;
        }
        if (word == "three")
        {
            value = 3;
            return true;
        }
        if (word == "four")
        {
            value = 4;
            return true;
        }
        if (word == "five")
        {
            value = 5;
            return true;
        }
        if (word == "six")
        {
            value = 6;
            return true;
        }
        if (word == "seven")
        {
            value = 7;
            return true;
        }
        if (word == "eight")
        {
            value = 8;
            return true;
        }
        if (word == "nine")
        {
            value = 9;
            return true;
        }

        return false;
    }

    static string[] SplitIntoWordsAndDigits(string input)
    {
        var knownWords = new Dictionary<string, string>
        {
            {"1", "one"}, {"2", "two"}, {"3", "three"}, {"4", "four"},
            {"5", "five"}, {"6", "six"}, {"7", "seven"}, {"8", "eight"}, {"9", "nine"}
        };

        // Find all occurrences of digit-words or single digits
        var occurrences = new Dictionary<int, string>();
        foreach (var pair in knownWords)
        {
            int index = input.IndexOf(pair.Value);
            while (index != -1)
            {
                occurrences[index] = pair.Key;
                index = input.IndexOf(pair.Value, index + 1);
            }

            index = input.IndexOf(pair.Key);
            while (index != -1)
            {
                occurrences[index] = pair.Key;
                index = input.IndexOf(pair.Key, index + 1);
            }
        }

        // Sort by the position of occurrence
        var sortedOccurrences = occurrences.OrderBy(kvp => kvp.Key);

        return sortedOccurrences.Select(kvp => kvp.Value).ToArray();
    }
}
