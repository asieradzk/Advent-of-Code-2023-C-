using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class _7
{
    public static string mySolution(string input)
    {
        var lines = input.Trim().Split('\n');
        List<Hand> hands = new();

        foreach (var line in lines)
        {
            hands.Add(new Hand(line));
        }

        var handsSorted = hands.OrderBy(h => h.handType).ThenByDescending(h => h.cardsValue);



        var pot = 0;
        int counter = 1;
        foreach (var hand in handsSorted)
        {
            Console.WriteLine($"Hand: {hand.mycards} Bid: {hand.myBid} Type: {hand.handType} Value: {hand.cardsValue}");
            pot += hand.myBid * counter;
            counter++;
        }

        return pot.ToString();
    }



    public class Hand
    {
        public int handType;
        public int myBid;
        public string cardsValue;
        public string mycards;

        public Hand(string line)
        {
            var cards = line.Split(' ')[0];
            var bid = line.Split(' ')[1];

            myBid = int.Parse(bid);
            cardsValue = cardsToValue(cards);
           
            mycards = cards;
            handType = WildCardSearch(mycards);

        }


        string cardsToValue(string cards)
        {
            //translate each letter to value from translation table
            var chars = cards.ToCharArray();
            var values = new List<char>();
            foreach (var c in chars)
            {
                values.Add(translationTable[c]);
            }

            //return the values as a string
            return string.Join("", values);

        }

        static int GetHandType(string cards)
        {
            var cardCounts = cards.GroupBy(c => c)
                                  .Select(group => group.Count())
                                  .OrderByDescending(count => count)
                                  .ToList();

            switch (cardCounts.Count)
            {
                case 1: return 7; // 5 of a kind
                case 2: return cardCounts[0] == 4 ? 6 : 5; // 4 of a kind or Full house
                case 3: return cardCounts[0] == 3 ? 4 : 3; // 3 of a kind or Two pairs
                case 4: return 2; // 2 of a kind
                default: return 1; // All different cards
            }


        }

        static int WildCardSearch(string cards)
        {
            if (!cards.Contains('J'))
            {
                return GetHandType(cards);
            }

            char[] cardArray = cards.ToCharArray();
            List<int> wildcardIndices = new List<int>();
            for (int i = 0; i < cardArray.Length; i++)
            {
                if (cardArray[i] == 'J')
                {
                    wildcardIndices.Add(i);
                    cardArray[i] = '2'; // Initialize wildcards with the first possible value
                }
            }

            int bestHandType = 0;

            bool finished = false;
            while (!finished)
            {
                int handType = GetHandType(new string(cardArray));
                bestHandType = Math.Max(bestHandType, handType);

                //finished if all wildcards are A
                List<char> wildcards = new();
                foreach (var index in wildcardIndices)
                {
                    wildcards.Add(cardArray[index]);
                }


                if (wildcards.All(c => c == 'A'))
                {
                    finished = true;
                    break;
                }

                //increment first index if its A set it to 2 and increment next index 

                foreach (var index in wildcardIndices)
                {
                    if (cardArray[index] != 'A')
                    {
                        cardArray[index] = possibleChars[possibleChars.IndexOf(cardArray[index]) + 1];
                        break;
                    }
                    else
                    {
                        cardArray[index] = '2';
                        continue;
                    }
                }

                Console.WriteLine(new string(cardArray));
            }

            return bestHandType;
        }



        static List<char> possibleChars = new() { '2', '3', '4', '5', '6', '7', '8', '9', 'T', /*'J',*/ 'Q', 'K', 'A' };




        Dictionary<char, char> translationTable = new()
            {
                {'J', 'Z'},
                {'2', 'M'},
                {'3', 'L'},
                {'4', 'K'},
                {'5', 'J'},
                {'6', 'I'},
                {'7', 'H'},
                {'8', 'G'},
                {'9', 'F'},
                {'T', 'E'},
                {'Q', 'C'},
                {'K', 'B'},
                {'A', 'A'},
            };
    }
}
