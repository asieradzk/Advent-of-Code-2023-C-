using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class _9
{
    public static string mySolution(string input)
    {
        var lines = input.Split('\n');
        List<Sequence> sequences = new();   

        foreach (var line in lines)
        {
            var newSequence = new Sequence(line);
            sequences.Add(newSequence);
        }


        //sum all the extrapolated numbers
        var sum = sequences.Sum(x => x.nextNumber);
        return sum.ToString();
    }
    public static string mySolution2(string input)
    {
        var lines = input.Split('\n');
        List<Sequence> sequences = new();

        foreach (var line in lines)
        {
            var newSequence = new Sequence(line);
            sequences.Add(newSequence);
        }


        //sum all the extrapolated numbers
        var sum = sequences.Sum(x => x.previousNumber);
        return sum.ToString();
    }

    public class Sequence
    {
        public List<int> originalNumbers = new();
        public int nextNumber;
        public int previousNumber;


        public Sequence(string line)
        {
            var numbers = line.Split(' ').Select(x => int.Parse(x)).ToList();
            originalNumbers = numbers;
            Extrapolate();
        }

        public List<List<int>> extrapolationScaffolds = new();

        public void Extrapolate()
        {
            List<int> currentExtrapolationScaffold = originalNumbers;
            extrapolationScaffolds.Add(currentExtrapolationScaffold);

            //while all elements are not equal to 0 and num of elements more than 1
            while(!currentExtrapolationScaffold.All(x => x == 0))
            {
                List<int> newExtrapolationScaffold = new();
                //starting from first number add the difference between next number and current number
                for (int i = 0; i < currentExtrapolationScaffold.Count - 1; i++)
                {
                    newExtrapolationScaffold.Add(currentExtrapolationScaffold[i + 1] - currentExtrapolationScaffold[i]);
                }
                extrapolationScaffolds.Add(newExtrapolationScaffold);
                currentExtrapolationScaffold = newExtrapolationScaffold;
            }


            //starting from last row
            int current = 0;
            //iterate from last row to first row
            for (int i = extrapolationScaffolds.Count - 2; i >= 0; i--)
            {
                var lastElement = extrapolationScaffolds[i].Last();
                current = lastElement + current;
            }   

            nextNumber = current;

            //starting from last row
            int current2 = 0;
            //iterate from last row to first row
            for (int i = extrapolationScaffolds.Count - 2; i >= 0; i--)
            {
                var lastElement = extrapolationScaffolds[i].First();
                current2 = lastElement - current2;
            }
            previousNumber = current2;
        }


    }
}