using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static _8;

public static class _8
{
    public static int mySolution(string input)
    {
        // Splitting the input into lines
        string[] lines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

        // The first line contains navigation instructions
        string navigationInstructions = lines[0];

        // The rest of the lines contain node definitions
        string[] nodeDefinitions = lines.Skip(1).ToArray();

        // Create the map from node definitions
        var myMap = Map.CreateFromLines(nodeDefinitions);

        // Initialize the path with navigation instructions
        Path path = new Path(navigationInstructions);

        // Start navigating from the "AAA" node
        Node currentNode = myMap.nodes["AAA"];
        int stepCount = 0;

        // Navigate through the nodes according to the instructions
        while (currentNode.name != "ZZZ")
        {
            var instruction = path.GetInstruction();
            currentNode = currentNode.GetNextNode(instruction);
            stepCount++;

            // Check to prevent infinite loops
            if (stepCount > 100000) // You can adjust this limit as needed
            {
                throw new Exception("Too many steps, possibly an infinite loop.");
            }
        }

        return stepCount;
    }


    public static string mySolution3(string input)
    {
        string[] lines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

        // The first line contains navigation instructions
        string navigationInstructions = lines[0];

        // The rest of the lines contain node definitions
        string[] nodeDefinitions = lines.Skip(1).ToArray();

        // Create the map from node definitions
        var myMap = Map.CreateFromLines(nodeDefinitions);

        // Initialize the path with navigation instructions
        Path path = new Path(navigationInstructions);

        // List to store the lengths of cycles that end with 'Z'
        List<int> zCycleLengths = new List<int>();

        // Calculate cycle length for each node that ends with 'A'
        foreach (var nodePair in myMap.nodes)
        {
            if (nodePair.Key.EndsWith('A'))
            {
                int cycleLength = FindZCycleLength(nodePair.Value, path);
                zCycleLengths.Add(cycleLength);
            }
        }

        // Method to find the length of the cycle to reach a 'Z' ending node
        int FindZCycleLength(Node startNode, Path path)
        {
            Node currentNode = startNode;
            int count = 0;
            int currentInstructionIndex = 0;

            while (!currentNode.name.EndsWith('Z'))
            {
                var direction = path.instructions[currentInstructionIndex];
                currentNode = currentNode.GetNextNode(direction);
                currentInstructionIndex = (currentInstructionIndex + 1) % path.instructions.Count;
                count++;
            }

            return count;
        }
        /*
        int lcmOfCycleLengths = zCycleLengths.Aggregate(1, (a, b) => Lcm(a, b));

        int Lcm(int a, int b)
        {
            // Handle edge cases where either a or b is zero.
            if (a == 0 || b == 0)
                return 0;

            return Math.Abs(a * b) / Gcd(a, b);
        }

        int Gcd(int a, int b)
        {
            // Ensure non-negative values for GCD calculation
            a = Math.Abs(a);
            b = Math.Abs(b);

            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        */
        //USed online LCM calculator instead

        // Convert the list of cycle lengths to a string and add the LCM at the end
        string cycleLengthsString = string.Join(", ", zCycleLengths) + $", LCM: {1}";
        return cycleLengthsString;
    }


    static bool amIDone(List<Node> myNodes)
    {
        // Check if all nodes end with 'Z' using LINQ
        //return myNodes.All(node => node.name.EndsWith('Z'));
        var count = myNodes.Count(node => node.name.EndsWith('Z'));
        return count > 1;
    }


    public class Map
    {
        public Dictionary<string, Node> nodes = new();
        public static Map CreateFromLines(string[] lines)
        {
            var result = new Map();
            foreach (var line in lines)
            {
                result.AddOrEdit(line);
            }

            return result;
        }


        public void AddOrEdit(string line)
        {
            string pattern = @"\b\w{3}\b";
            MatchCollection matches = Regex.Matches(line, pattern);
            var myName = matches[0].Value;
            var thisNode = nodes.TryGetValue(myName, out Node myNode);
            var leftNode = nodes.TryGetValue(matches[1].Value, out Node leftDestinationNode);
            var rightNode = nodes.TryGetValue(matches[2].Value, out Node rightDestinationNode);

            if(!leftNode)
            {
                leftDestinationNode = new Node();
                leftDestinationNode.name = matches[1].Value;
                nodes.Add(leftDestinationNode.name, leftDestinationNode);
            }

            if (!rightNode)
            {
                rightDestinationNode = new Node();
                rightDestinationNode.name = matches[2].Value;
                if((matches[1].Value != matches[2].Value))
                {
                    nodes.Add(rightDestinationNode.name, rightDestinationNode);
                }
                
            }


            if (!thisNode)
            {
                myNode = new Node();
                myNode.name = myName;
                nodes.Add(myName, myNode);
            }

            if((matches[1].Value != matches[2].Value))
            {
                myNode.leftDestinationNode = leftDestinationNode;
                myNode.rightDestinationNode = rightDestinationNode;
            }else
            {
                myNode.leftDestinationNode = leftDestinationNode;
                myNode.rightDestinationNode = leftDestinationNode;
            }

        }
    }

    public class Node
    {
        public Node leftDestinationNode;
        public Node rightDestinationNode;
        public string name;

        public Node GetNextNode(instruction direction)
        {
            return direction == instruction.L ? leftDestinationNode : rightDestinationNode;
        }
    }

    public class Path
    {
        public List<instruction> instructions = new();

        int currentInstruction = 0;
        public instruction GetInstruction()
        {
            //returns instructions and loops around when it reaches the end
            var instruction = instructions[currentInstruction];
            currentInstruction++;
            if (currentInstruction == instructions.Count)
            {
                currentInstruction = 0;
            }
            return instruction;
        }

        public Path(string input)
        {
            foreach (var letter in input)
            {
                instructions.Add(letter switch
                {
                    'R' => instruction.R,
                    'L' => instruction.L,
                    _ => throw new Exception("invalid instruction")
                });
            }
        }
    }

    public enum instruction
    {
        R,
        L
    }


}