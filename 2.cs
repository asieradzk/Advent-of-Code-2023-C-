using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class _2
{
    public static string mySolution(string input)
    {
        List<Game> myGames = new();
        var lines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var line in lines)
        {
            myGames.Add(Game.GameFromLine(line));
        }

        // Using LINQ to filter out games where any roll has more than 12 R, 13 G, and 14 B
        var validGames = myGames.Where(game =>
            game.R.All(count => count <= 12) &&
            game.G.All(count => count <= 13) &&
            game.B.All(count => count <= 14));

        // Summing the IDs of the valid games
        int sumOfIds = validGames.Sum(game => game.ID);

        return sumOfIds.ToString();
    } 


    public static string mySolution2(string input)
    {
        List<Game> myGames = new();
        var lines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var line in lines)
        {
            myGames.Add(Game.GameFromLine(line));
        }

        // Create a list of the multiplication products for each game
        List<int> multiplicationProducts = myGames.Select(game =>
            (game.R.Any() ? game.R.Max() : 1) *
            (game.G.Any() ? game.G.Max() : 1) *
            (game.B.Any() ? game.B.Max() : 1)).ToList();

        // Sum the multiplication products
        int sumOfProducts = multiplicationProducts.Sum();

        return sumOfProducts.ToString();
    }


    internal class Game
    {
        public static Game GameFromLine(string line)
        {
            // Example input:
            // Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green

            var result = new Game
            {
                R = new List<int>(),
                G = new List<int>(),
                B = new List<int>()
            };

            // Split off game ID
            var parts = line.Split(':');
            var gameID = parts[0];
            result.ID = int.Parse(gameID.Split(' ')[1]);

            // Split off the rolls with ";"
            var rolls = parts[1].Split(';');

            foreach (var roll in rolls)
            {
                // Split each roll into color and count pairs
                var colorCounts = roll.Split(',');
                foreach (var colorCount in colorCounts)
                {
                    var trimmedColorCount = colorCount.Trim();
                    if (string.IsNullOrEmpty(trimmedColorCount))
                        continue;

                    // Split each pair into count and color
                    var partsed = trimmedColorCount.Split(' ');
                    int count = int.Parse(partsed[0]);
                    string color = partsed[1].ToLower();

                    // Add count to respective color list
                    switch (color)
                    {
                        case "red":
                            result.R.Add(count);
                            break;
                        case "green":
                            result.G.Add(count);
                            break;
                        case "blue":
                            result.B.Add(count);
                            break;
                    }
                }
            }

            return result;
        }

        public List<int> R { get; set; }
        public List<int> G { get; set; }
        public List<int> B { get; set; }
        public int ID { get; set; }
    }

}
