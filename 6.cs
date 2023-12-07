using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

public static class _6
{
    /*
    public static string mySolution(string input)
    {
        List<Race> myRaces = new();

        string[] lines = input.Trim().Split('\n');

        // Extract times and distances from the lines
        string[] times = lines[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1..];
        string[] distances = lines[1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1..];


        for (int i = 0; i < times.Length; i++)
        {
            int time = int.Parse(times[i]);
            int distance = int.Parse(distances[i]);
            myRaces.Add(new Race(time, distance));
        }

        //result is equal to all of the race.WaysToWin multiplied together

        long result = 1;
        //use linq
        foreach (Race race in myRaces)
        {
            result *= race.WaysToWin;
        }

        return result.ToString();

    
   }
    */

    public static string mySolution2(string input)
    {
        List<Race> myRaces = new();

        string[] lines = input.Trim().Split('\n');

        // Extract times and distances from the lines
        string[] times = lines[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1..];
        string[] distances = lines[1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1..];

        //concat all the times and distances into a single string
        var timeString = string.Join("", times);
        Console.WriteLine(timeString);
        var time = Int128.Parse(timeString);
        var distance = Int128.Parse(string.Join("", distances));

        var race = new Race(time, distance);

        return race.WaysToWin.ToString();   



    }

    public class Race
    {
        public BigInteger Time { get; set; }
        public BigInteger Distance { get; set; }

        //returns strategies which have "true" key
        public BigInteger WaysToWin => Strategies.Count(x => x.Value);

        public Dictionary<BigInteger, bool> Strategies = new();

        public Race(BigInteger time, BigInteger distance)
        {
            Time = time;
            Distance = distance;

            for (BigInteger i = 0; i < Time; i++)
            {
                var travelDistance = DistanceTraveled(i);
                Strategies.Add(i, travelDistance >= Distance);               
            }
        }

        private BigInteger DistanceTraveled(BigInteger windUpTime, BigInteger travelTime)
        {
            return windUpTime * travelTime;
        }

        private BigInteger DistanceTraveled(BigInteger windUpTime)
        {
            var travelTime = Time - windUpTime;
            return DistanceTraveled(windUpTime, travelTime);
        }

    }
}
