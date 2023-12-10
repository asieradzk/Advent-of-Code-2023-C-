using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _10.Node;

public static class _10
{
    public static string mySolution(string input)
    {
        var myMaze = new Maze(input);

        var startingNode = myMaze.startingNode;


        List<Node> explorations = new();
        var startingPos = (startingNode.x, startingNode.y);


        #region addStartingExplorations
        // Check North of Start
        if (IsWithinBounds(startingPos.x, startingPos.y + 1, myMaze)
            && myMaze.maze[startingPos.x, startingPos.y + 1].CanEnterFrom(startingPos.x, startingPos.y))
        {
            explorations.Add(myMaze.maze[startingPos.x, startingPos.y + 1].updateOrigin(startingPos.x, startingPos.y));
        }

        // Check South of Start
        if (IsWithinBounds(startingPos.x, startingPos.y - 1, myMaze)
            && myMaze.maze[startingPos.x, startingPos.y - 1].CanEnterFrom(startingPos.x, startingPos.y))
        {
            explorations.Add(myMaze.maze[startingPos.x, startingPos.y - 1].updateOrigin(startingPos.x, startingPos.y));
        }

        // Check East of Start
        if (IsWithinBounds(startingPos.x + 1, startingPos.y, myMaze)
            && myMaze.maze[startingPos.x + 1, startingPos.y].CanEnterFrom(startingPos.x, startingPos.y))
        {
            explorations.Add(myMaze.maze[startingPos.x + 1, startingPos.y].updateOrigin(startingPos.x, startingPos.y));
        }

        // Check West of Start
        if (IsWithinBounds(startingPos.x - 1, startingPos.y, myMaze)
            && myMaze.maze[startingPos.x - 1, startingPos.y].CanEnterFrom(startingPos.x, startingPos.y))
        {
            explorations.Add(myMaze.maze[startingPos.x - 1, startingPos.y].updateOrigin(startingPos.x, startingPos.y));
        }
        #endregion


        var counter = 1;
        while(!isFinished(explorations))
        {
            List<Node> explorationsReplacement = new();

            foreach(var exploration in explorations)
            {
                //check if next node is valid
                var (nextX, nextY) = exploration.GetPossibleNextNodeInfo();
                bool isAble = IsWithinBounds(nextX, nextY, myMaze)
                    && myMaze.maze[nextX, nextY].CanEnterFrom(exploration.x, exploration.y);

                if(isAble)
                {
                    explorationsReplacement.Add(myMaze.maze[nextX, nextY].updateOrigin(exploration.x, exploration.y));
                }

            }
            counter++;
            explorations = explorationsReplacement;

            
        }


        return counter.ToString();
    }

    public static string mySolution2(string input)
    {
        var myMaze = new Maze(input);

        var startingNode = myMaze.startingNode;


        List<Node> explorations = new();
        var startingPos = (startingNode.x, startingNode.y);


        #region addStartingExplorations
        // Check North of Start
        if (IsWithinBounds(startingPos.x, startingPos.y + 1, myMaze)
            && myMaze.maze[startingPos.x, startingPos.y + 1].CanEnterFrom(startingPos.x, startingPos.y))
        {
            explorations.Add(myMaze.maze[startingPos.x, startingPos.y + 1].updateOrigin(startingPos.x, startingPos.y));
        }

        // Check South of Start
        if (IsWithinBounds(startingPos.x, startingPos.y - 1, myMaze)
            && myMaze.maze[startingPos.x, startingPos.y - 1].CanEnterFrom(startingPos.x, startingPos.y))
        {
            explorations.Add(myMaze.maze[startingPos.x, startingPos.y - 1].updateOrigin(startingPos.x, startingPos.y));
        }

        // Check East of Start
        if (IsWithinBounds(startingPos.x + 1, startingPos.y, myMaze)
            && myMaze.maze[startingPos.x + 1, startingPos.y].CanEnterFrom(startingPos.x, startingPos.y))
        {
            explorations.Add(myMaze.maze[startingPos.x + 1, startingPos.y].updateOrigin(startingPos.x, startingPos.y));
        }

        // Check West of Start
        if (IsWithinBounds(startingPos.x - 1, startingPos.y, myMaze)
            && myMaze.maze[startingPos.x - 1, startingPos.y].CanEnterFrom(startingPos.x, startingPos.y))
        {
            explorations.Add(myMaze.maze[startingPos.x - 1, startingPos.y].updateOrigin(startingPos.x, startingPos.y));
        }
        #endregion


        var counter = 1;
        List<Node> path = new();
        path.Add(startingNode);

        while (!isFinished(explorations))
        {
            List<Node> explorationsReplacement = new();
            foreach (var exploration in explorations)
            {
                //check if next node is valid
                var (nextX, nextY) = exploration.GetPossibleNextNodeInfo();
                bool isAble = IsWithinBounds(nextX, nextY, myMaze)
                    && myMaze.maze[nextX, nextY].CanEnterFrom(exploration.x, exploration.y);

                if (isAble)
                {
                    explorationsReplacement.Add(myMaze.maze[nextX, nextY].updateOrigin(exploration.x, exploration.y));
                    path.Add(myMaze.maze[nextX, nextY]);
                }

            }
            counter++;
            explorations = explorationsReplacement;
        }
        
        path.Remove(path.Last());

        myMaze.FloodFillFromEdges( path);
        var enclosedTiles = myMaze.CountEnclosedTiles(path);






        return enclosedTiles.ToString();
    }

    private static bool isFinished(List<Node> explorations)
    {
        return explorations.GroupBy(node => new { node.x, node.y }).Any(g => g.Count() > 1);
    }


    private static bool IsWithinBounds(int x, int y, Maze maze)
    {
        return x >= 0 && y >= 0 && x < maze.maze.GetLength(0) && y < maze.maze.GetLength(1);
    }

    public class Maze
    {
        public Node[,] maze;
        public Node startingNode;

        public Node GetNode(int x, int y)
        {
            return maze[x, y];
        }

        public Maze(string input)
        {
            string[] lines = input.Replace("\r\n", "\n").Split('\n');
            int height = lines.Length;
            int width = lines[0].Length;

            maze = new Node[width, height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    char nodeSymbol = lines[y][x];
                    maze[x, y] = new Node(x, y, nodeSymbol);

                    if (nodeSymbol == 'S')
                    {
                        startingNode = maze[x, y];
                    }
                }
            }
        }


        public void FloodFill(int x, int y, List<Node> circuit)
        {
            if (x < 0 || x >= maze.GetLength(0) || y < 0 || y >= maze.GetLength(1))
                return;

            Node node = maze[x, y];

            // Skip if the node is already visited or part of the circuit
            if (node.Visited || circuit.Contains(node))
                return;

            node.Visited = true;

            // Standard flood fill in four directions
            FloodFill(x + 1, y, circuit);
            FloodFill(x - 1, y, circuit);
            FloodFill(x, y + 1, circuit);
            FloodFill(x, y - 1, circuit);

            // Check for squeezable paths
            if (CanSqueeze(x, y, circuit))
            {
                // Propagate flood fill to diagonally adjacent tiles
                FloodFill(x + 1, y + 1, circuit);
                FloodFill(x - 1, y - 1, circuit);
                FloodFill(x + 1, y - 1, circuit);
                FloodFill(x - 1, y + 1, circuit);
            }
        }
        private bool CanSqueeze(int x, int y, List<Node> circuit)
        {
            // Check if the current position is at the edge of the maze where squeezing isn't possible
            if (x <= 0 || x >= maze.GetLength(0) - 1 || y <= 0 || y >= maze.GetLength(1) - 1)
                return false;

            // Check for right angle pipe configurations
            bool pipeAbove = IsPipe(maze[x, y - 1]);
            bool pipeBelow = IsPipe(maze[x, y + 1]);
            bool pipeLeft = IsPipe(maze[x - 1, y]);
            bool pipeRight = IsPipe(maze[x + 1, y]);

            // Squeezing is possible in the diagonal gaps created by right angle configurations
            return ((pipeAbove || pipeBelow) && (pipeLeft || pipeRight)) && !circuit.Contains(maze[x, y]);
        }

        private bool IsPipe(Node node)
        {
            return node.nodeType != NodeType.Ground && node.nodeType != NodeType.Start;
        }



        public void FloodFillFromEdges(List<Node> circuit)
        {
            int width = maze.GetLength(0);
            int height = maze.GetLength(1);

            // Perform flood fill from all edges
            for (int x = 0; x < width; x++)
            {
                FloodFill(x, 0, circuit); // Top edge
                FloodFill(x, height - 1, circuit); // Bottom edge
            }
            for (int y = 0; y < height; y++)
            {
                FloodFill(0, y, circuit); // Left edge
                FloodFill(width - 1, y, circuit); // Right edge
            }
        }

        private bool CanSqueezeDiagonally(int x, int y, List<Node> circuit)
        {
            // Check bounds
            if (x <= 0 || x >= maze.GetLength(0) - 1 || y <= 0 || y >= maze.GetLength(1) - 1)
                return false;

            // Check for right angle pipe configurations
            bool pipeAbove = circuit.Contains(maze[x, y - 1]);
            bool pipeBelow = circuit.Contains(maze[x, y + 1]);
            bool pipeLeft = circuit.Contains(maze[x - 1, y]);
            bool pipeRight = circuit.Contains(maze[x + 1, y]);

            // Squeezing is possible if there are pipes in any of the right angle configurations
            return (pipeAbove && pipeLeft) || (pipeAbove && pipeRight) ||
                   (pipeBelow && pipeLeft) || (pipeBelow && pipeRight);
        }



        public int CountEnclosedTiles(List<Node> circuit)
        {
            int count = 0;

            // Debugging loop to print out the maze with enclosed tiles
            for (int y = 0; y < maze.GetLength(1); y++)
            {
                for (int x = 0; x < maze.GetLength(0); x++)
                {
                    Node node = maze[x, y];
                    if (!node.Visited && !circuit.Contains(node))
                        Console.Write('X');
                    else
                        Console.Write(' ');
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            // Count enclosed tiles
            for (int x = 0; x < maze.GetLength(0); x++)
            {
                for (int y = 0; y < maze.GetLength(1); y++)
                {
                    Node node = maze[x, y];
                    if (!node.Visited && !circuit.Contains(node))
                        count++;
                }
            }
            return count;
        }



        public void ResetVisited()
        {
            for (int x = 0; x < maze.GetLength(0); x++)
            {
                for (int y = 0; y < maze.GetLength(1); y++)
                {
                    maze[x, y].Visited = false;
                }
            }
        }

    }

    public class Node
    {
        public int x;
        public int y;
        public NodeType nodeType;
        int originX;
        int originY;
        public bool Visited { get; set; } = false;


        public Node(int x, int y, char nodeSymbol)
        {
            this.x = x;
            this.y = y;
            nodeType = ConvertSymbolToNodeType(nodeSymbol);
        }

        public Node updateOrigin(int x, int y)
        {
            originX = x;
            originY = y;
            return this;
        }



        private NodeType ConvertSymbolToNodeType(char symbol)
        {
            switch (symbol)
            {
                case '|':
                    return NodeType.Vertical;
                case '-':
                    return NodeType.Horizontal;
                case 'L':
                    return NodeType.RightUp;
                case 'J':
                    return NodeType.LeftUp;
                case '7':
                    return NodeType.LeftDown;
                case 'F':
                    return NodeType.DownRight;
                case '.':
                    return NodeType.Ground;
                case 'S':
                    return NodeType.Start;
                default:
                    return NodeType.Ground;

                    
            }
        }

        public (int, int) GetNextNodeCoordinates(Node previousNode)
        {
            int dx = this.x - previousNode.x;
            int dy = this.y - previousNode.y;

            switch (this.nodeType)
            {
                case NodeType.Vertical:
                    return dy > 0 ? (this.x, this.y + 1) : (this.x, this.y - 1);
                case NodeType.Horizontal:
                    return dx > 0 ? (this.x + 1, this.y) : (this.x - 1, this.y);
                case NodeType.RightUp:
                    return dx == 0 ? (this.x + 1, this.y) : (this.x, this.y - 1);
                case NodeType.LeftUp:
                    return dx == 0 ? (this.x - 1, this.y) : (this.x, this.y - 1);
                case NodeType.DownRight:
                    return dx == 0 ? (this.x + 1, this.y) : (this.x, this.y + 1);
                case NodeType.LeftDown:
                    return dx == 0 ? (this.x - 1, this.y) : (this.x, this.y + 1);
                // Ground and Start types might need special handling
                case NodeType.Ground:
                    throw new InvalidOperationException("Invalid node type for movement");
                case NodeType.Start:
                default:
                    throw new InvalidOperationException("Invalid node type for movement");
            }
        }

        public bool CanEnterFrom(int x, int y)
        {
            int dx = this.x - x; // Difference in x-coordinate
            int dy = this.y - y; // Difference in y-coordinate

            switch (this.nodeType)
            {
                case NodeType.Vertical:
                    // Can enter from North or South
                    return dy != 0 && dx == 0;

                case NodeType.Horizontal:
                    // Can enter from East or West
                    return dx != 0 && dy == 0;

                case NodeType.RightUp:
                    return (dx == -1 && dy == 0) || (dx == 0 && dy == 1);


                case NodeType.LeftUp:
                    return (dx == 1 && dy == 0) || (dx == 0 && dy == 1);

                case NodeType.DownRight:
                    return (dx == -1 && dy == 0) || (dx == 0 && dy == -1);

                case NodeType.LeftDown:
                    return (dx == 1 && dy == 0) || (dx == 0 && dy == -1);


                case NodeType.Ground:
                    // Cannot enter ground nodes
                    return false;

                case NodeType.Start:
                    // Start node can be entered (special case)
                    return true;

                default:
                    throw new InvalidOperationException("Invalid node type for entry check");
            }
        }


        public (int, int) GetPossibleNextNodeInfo()
        {
            // Calculate the next node's coordinates based on the current node type and its origin
            var (nextX, nextY) = GetNextNodeCoordinates(new Node(originX, originY, ' ')); // ' ' is a placeholder symbol

            return (nextX, nextY);
        }

        public enum NodeType
        {
            Vertical,
            Horizontal,
            RightUp,
            LeftUp,
            DownRight,
            LeftDown,
            Ground,
            Start,
        }
    }


}
