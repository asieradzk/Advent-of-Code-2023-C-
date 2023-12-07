using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

public static class _5
{
    public static string mySolution(string input)
    {
        var myMaps = MyMaps(input);
        var mySeeds = MySeeds(input);
        List<long> lowestEndMaps = new();
        foreach (var seed in mySeeds)
        {
            Console.WriteLine($"Seed: {seed}");
            long num = seed;
            foreach (var map in myMaps)
            {
                num = map.FindLowestMap(num);
                Console.WriteLine($"Map: {num}");
            }
            lowestEndMaps.Add(num);          
        }

        return lowestEndMaps.Min().ToString();
    }
    public static string mySolution2(string input)
    {
        var myMaps = MyMaps(input);
        var mySeeds = MySeeds2(input);
        ConcurrentBag<long> lowestEndMaps = new();

        Parallel.ForEach(mySeeds, seed =>
        {
            long num = seed;
            foreach (var map in myMaps)
            {
                num = map.FindLowestMap(num);
            }
            lowestEndMaps.Add(num);
        });

        return lowestEndMaps.Min().ToString();
    }

    public static string mySolution3(string input)
    {
        var myMaps = MyMaps(input);
        var mySeeds = MySeeds3(input);
        ConcurrentBag<(long, long)> lowestEndMaps = new();

        Parallel.ForEach(mySeeds, seed =>
        {
            Console.WriteLine($"Seed: {seed}");
            List<(long, long)> num = new();
            num.Add(seed);


            foreach (var map in myMaps)
            {
                num = map.ConvertSeedRanges(num);
            }

            foreach (var item in num)
            {
                lowestEndMaps.Add(item);
            }
        });


        foreach (var seed in mySeeds)
        {
            
        }

        //find the lowest item1 in lowestEndMaps
        //remove item 0 and negative
        lowestEndMaps = new(lowestEndMaps.Where(x => x.Item1 > 0));


        var lowest = lowestEndMaps.Min(x => x.Item1);
        var lowestItem = lowestEndMaps.Where(x => x.Item1 == lowest).First();

        return lowestItem.Item1.ToString();

    }

    private static List<long> MySeeds(string input)
    {
        //take first line
        var line = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None)[0];
        return line.Split(' ')
                    .Where(s => long.TryParse(s, out _))
                    .Select(s => long.Parse(s))
                    .ToList();
    }

    private static List<long> MySeeds2(string input)
    {
        var nums = MySeeds(input);
        var result = new List<long>();
        for (var i = 0; i < nums.Count; i = i+ 2)
        {
            for (var j = 0; j < nums[i + 1]; j++)
            {
                result.Add(nums[i] + j);
            }
        }
        return result;
    }

    //Non-brute force solution
    //this works on test case but cant find why it doesnt work on real case
    private static List<(long, long)> MySeeds3(string input)
    {
        var nums = MySeeds(input);
        var result = new List<(long, long)>();

        for (var i = 0; i < nums.Count; i = i + 2)
        {
            result.Add((nums[i], nums[i + 1]));
        }
        return result;
    }

    private static List<MapProvider> MyMaps(string input)
    {
        //split longo lines
        var lines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

        //iterate all lines
        List<MapProvider> maps = new();
        List<string> linesCache = new();
        foreach (var line in lines.Skip(2))
        {
            if (line.Contains("map:"))
            {
                linesCache = new();
                continue;
            }

            //if line is empty
            if (string.IsNullOrWhiteSpace(line))
            {
                maps.Add(MapProvider.CreateFromLines(linesCache.ToArray()));
                continue;
            }

            //else add to cache
            linesCache.Add(line);


        }
        maps.Add(MapProvider.CreateFromLines(linesCache.ToArray()));

        return maps;
    }


    public class MapProvider
    {
        private readonly List<Converter> myConverters;

        private MapProvider(IEnumerable<Converter> converters)
        {
            myConverters = converters.ToList();
        }

        public static MapProvider CreateFromLines(string[] lines)
        {
            return new MapProvider(lines.Select(Converter.CreateFromLine));
        }

        public List<(long, long)> ConvertSeedRanges(List<(long, long)> seedRanges)
        {
            var result = new List<(long, long)>();

            foreach (var converter in myConverters)
            {
                result.AddRange(converter.Convert(seedRanges));
            }
            
            if(result.Count == 0)
            {
                result.AddRange(seedRanges);
            }
            
            //remove negative & 0
            result = new(result.Where(x => x.Item1 > 0));


            //find items with same item1 and remove everything but the one with the lowesr item2
            var duplicates = result.GroupBy(x => x.Item1)
                                   .Where(g => g.Count() > 1)
                                   .SelectMany(g => g.OrderByDescending(x => x.Item2).Skip(1))
                                   .ToList();




            return duplicates;
        }

        public long FindLowestMap(long input)
        {
           
            var result = input;

            foreach (var converter in myConverters)
            {
                if (converter.isInRange(result))
                {
                    result = converter.Convert(result);
                    return result;
                }
            }

            return result;
        }
        class Converter
        {
            long firstInput;
            long secondInput;
            long range;

            public bool isInRange(long input)
            {
                //returns false if input is less than firstInput or greater than firstInput + range
                if (input < secondInput || input >= secondInput + range)
                {
                    return false;
                }              
                return true;
            }

            public static Converter CreateFromLine(string line)
            {
                List<long> numbers = new List<long>();
                string[] parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string part in parts)
                {
                    numbers.Add(Int64.Parse(part));
                }

                //if not 3 numbers throw
                if (numbers.Count != 3)
                {
                    throw new Exception("Invalid input");
                }

                return new Converter(numbers[0], numbers[1], numbers[2]);
            }

            Converter(long firstInput, long secondInput, long range)
            {
                this.firstInput = firstInput;
                this.secondInput = secondInput;
                this.range = range;
            }

            public long Convert(long input)
            {
                var diff = firstInput - secondInput;

                var result = input + diff;

                return result;
            }

            //previous conversion takes in input and maps it according to mapping rules
            //now we need to split according to rules and return that many ranges
            public List<(long, long)> Convert(List<(long, long)> inputAndRange)
            {
                List<(long, long)> result = new();

                foreach (var item in inputAndRange)
                {
                    result.AddRange(ConvertInputRange(item));
                }
                return result;
            }   


            List<(long, long)> ConvertInputRange((long, long) inputAndRange)
            {
                List<(long, long)> result = new();
                var input = inputAndRange.Item1;
                var myRange = inputAndRange.Item2;

                


                long startOfRange = input;
                long endOfRange = input + myRange;

                long startOfSecondRange = secondInput;
                long endOfSecondRange = secondInput + range;

                // Calculate split1 and split2 directly
                long split1 = Math.Max(0, Math.Min(endOfRange, startOfSecondRange) - startOfRange);
                long split2 = Math.Max(0, Math.Min(endOfRange, endOfSecondRange) - Math.Max(startOfRange, startOfSecondRange));




                if (input + myRange < secondInput || input >= secondInput + range)
                {

                   result.Add(inputAndRange);
                    return result;
                }

                if (split1 > 0)
                {
                    result.Add((input, split1));
                }

                if (split2 > 0)
                {
                    result.Add((input + split1 + (firstInput - secondInput), split2));
                }
                if (split1 + split2 < myRange)
                {
                    result.Add((input + split2 + split1, myRange - split2 - split1));
                }



                return result;

                

                
            }

        }
    }

}
