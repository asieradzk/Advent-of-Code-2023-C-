using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class _4
{
    public static string mySolution(string input)
    {
        var lines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        List<Card> cards = new List<Card>();

        foreach (var line in lines)
        {
            cards.Add(Card.CardFromLine(line));
        }

        var output = cards.Sum(c => c.CardScore());

        return output.ToString();
    }

    public static string mySolution2(string input)
    {
        var lines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        Dictionary<int, Card> cards = new Dictionary<int, Card>();

        for (int i = 0; i < lines.Length; i++)
        {
            cards.Add(i + 1, Card.CardFromLine(lines[i]));
        }
        int counter = 0;
        for(int i = 0; i < lines.Length; i++)
        {
            var card = cards[i + 1];
            var copies = card.CardCopies();
            foreach (var copy in copies)
            {
                cards[copy].copies++;
            }
            counter += card.copies;
        }

        return counter.ToString();
    }



    public class  Card
    {
        List<int> numbers;
        List<int> winningNubers;
        public int myId;
        public int copies = 1;

        public List<int> CardCopies()
        {
            var result = new List<int>();
            int matches = 0;

            

            foreach (var number in numbers)
            {
                if (winningNubers.Contains(number))
                {
                    matches++;
                }
            }

            for (int j = 0; j < copies; j++)
            {
                for (int i = 0; i < matches; i++)
                {
                    result.Add(myId + i + 1);
                }
            }

            
            result.RemoveAll(i => i > 209);
            
            return result;
        }

        public int CardScore()
        {
            int score = 0;
            int matches = 0;
            foreach (var number in numbers)
            {
                if (winningNubers.Contains(number))
                {
                    matches++;
                }
            }

            if(matches > 0 )
            {
                score = 1;
                matches--;
            }

            for (int i = 0; i < matches; i++)
            {
                score *= 2;
            }

            return score;
        }

        public static Card CardFromLine(string line)
        {
            // | separator char
            var result = new Card();

            var id = line.Split(':')[0];
            var id2 = id.Split(' ').Select(s => s.Replace(" ", ""));
            var id3 = String.Join("", id2).Split('d')[1];
            result.myId = int.Parse(id3);

            var firstHalf = line.Split('|')[0].Split(':')[1].Substring(1);
            firstHalf = firstHalf.Substring(0, firstHalf.Length - 1);
            var secondHalf = line.Split('|')[1].Substring(1);

            var numbers = firstHalf.Split(' ').Select(s => s.Replace(" ", ""));
            result.numbers = new List<int>();
            foreach (var number in numbers)
            {
                if (number != "")
                {
                    result.numbers.Add(int.Parse(number));
                }
            }

                
            var winningNumbers = secondHalf.Split(' ').Select(s => s.Replace(" ", ""));
            result.winningNubers = new List<int>();
            foreach (var number in winningNumbers)
            {
                if (number != "")
                {
                    result.winningNubers.Add(int.Parse(number));
                }
            }

            Console.WriteLine(result.myId);
            return result;
        }
    }
}
