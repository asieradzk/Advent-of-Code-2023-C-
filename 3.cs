using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class _3
{
    public static string mySolution(string input)
    {
        var grid = Grid.GridFromInput(input);


        var output = grid.ValueOfParts(grid.myNumbers);

        return output.ToString();
    }

    public static string mySolution2(string input)
    {
        var grid = Grid.GridFromInput(input);

        //return the sum of my gears
        var output = grid.myGears.Sum(g => g.Value);
        return output.ToString();
    }

    public class Grid
    {
        //exapmle input:
        //467..114..
        //...*......
        //..35..633.
        //......#...



        public int myWidth;
        public int myHeight;

        public GridCell[,] myGrid;
        public List<GridCell> mySymbols;
        public List<Number> myNumbers;
        public List<SymbolPart2> myGears;

        public static Grid GridFromInput (string gridAsString)
        {
            var result = new Grid();


            //width is the number of characters before the first newline
            result.myWidth = gridAsString.IndexOf(Environment.NewLine);

            //height is the number of newlines
            var lineCount = gridAsString.Split(new[] { Environment.NewLine }, StringSplitOptions.None).Length;
            result.myHeight = lineCount;


            result.myGrid = new GridCell[result.myWidth, result.myHeight];

            var lines = gridAsString.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            for (int y = 0; y < result.myHeight; y++)
            {
                var line = lines[y];
                var cells = getGridCellsFromline(line);

                //set y coordiante of each cell
                foreach (var cell in cells)
                {
                    cell.myY = y;
                }

                for (int x = 0; x < result.myWidth; x++)
                {
                    result.myGrid[x, y] = cells[x];
                }


            }

            //find all the symbols in the gird
            result.mySymbols = new List<GridCell>();
            for (int y = 0; y < result.myHeight; y++)
            {
                for (int x = 0; x < result.myWidth; x++)
                {
                    var cell = result.myGrid[x, y];
                    if (cell.myType == CelLType.symbol)
                    {
                        result.mySymbols.Add(cell);
                    }
                }
            }

            result.myNumbers = result.FindNumbersInGrid();

            foreach (var number in result.myNumbers)
            {
                number.isAdjacentToSymbol = IsNumberAdjacentToSymbol(number, result.mySymbols);
            }

            result.myGears = FindGears();


            return result;

            bool IsNumberAdjacentToSymbol(Number number, List<GridCell> symbols)
            {
                foreach (var symbol in symbols)
                {
                    if (IsAdjacent(number, symbol))
                    {
                        return true;
                    }
                }
                return false;
            }

            bool IsAdjacent(Number number, GridCell symbol)
            {
                int minX = number.myXSpan.Min();
                int maxX = number.myXSpan.Max();

                if ((symbol.myX >= minX - 1 && symbol.myX <= maxX + 1) &&
                    (symbol.myY >= number.myY - 1 && symbol.myY <= number.myY + 1))
                {
                    return true;
                }

                return false;
            }

            List<SymbolPart2> FindGears()
            {
                var gears = new List<SymbolPart2>();

                foreach (var symbol in result.mySymbols)
                {
                    var adjacentNumbers = GetAdjacentNumbers(symbol, result.myNumbers);


                    if (adjacentNumbers.Count == 2)
                    {
                        int product = adjacentNumbers[0].myValue * adjacentNumbers[1].myValue;
                        if (true)
                        {
                            gears.Add(new SymbolPart2
                            {
                                num1Value = adjacentNumbers[0].myValue,
                                num2Value = adjacentNumbers[1].myValue,
                                Value = product
                            });
                        }
                    }
                }

                return gears;
            }

            List<Number> GetAdjacentNumbers(GridCell symbol, List<Number> numbers)
            {
                var adjacentNumbers = new List<Number>();

                foreach (var number in numbers)
                {
                    if (IsAdjacent(number, symbol))
                    {
                        adjacentNumbers.Add(number);
                    }
                }

                return adjacentNumbers;
            }



        }


        public int ValueOfParts(List<Number> numbers)
        {
            int sum = 0;
            foreach (var number in numbers)
            {
                if (number.isAdjacentToSymbol)
                {
                    sum += number.myValue;
                }
            }
            return sum;
        }




        public static List<GridCell> getGridCellsFromline(string line)
        {
            var result = new List<GridCell>();
            var numberAccumulator = string.Empty;
            int x = 0; // Initialize x coordinate

            foreach (var c in line)
            {
                if (char.IsDigit(c))
                {
                    numberAccumulator += c;
                }
                else
                {
                    if (numberAccumulator != string.Empty)
                    {
                        int numberValue = int.Parse(numberAccumulator);
                        foreach (var digit in numberAccumulator)
                        {
                            result.Add(new GridCell(x, 0, numberValue, digit, CelLType.number));
                            x++;
                        }
                        numberAccumulator = string.Empty;
                    }

                    var cellType = c == '.' ? CelLType.separator : CelLType.symbol;
                    result.Add(new GridCell(x, 0, 0, c, cellType));
                    x++;
                }
            }

            // Add any remaining number at the end of the line
            if (numberAccumulator != string.Empty)
            {
                int numberValue = int.Parse(numberAccumulator);
                foreach (var digit in numberAccumulator)
                {
                    result.Add(new GridCell(x, 0, numberValue, digit, CelLType.number));
                    x++;
                }
            }

            return result;

        }

        public List<Number> FindNumbersInGrid()
        {
            var numbers = new List<Number>();

            for (int y = 0; y < myHeight; y++)
            {
                List<int> myXs = new List<int>();
                string numberValue = "";

                for (int x = 0; x < myWidth; x++)
                {
                    GridCell cell = myGrid[x, y];
                    if (cell.myType == CelLType.number)
                    {
                        myXs.Add(x);
                        numberValue += cell.myChar.ToString(); // Assuming myChar holds the digit
                    }
                    else if (numberValue != "")
                    {
                        numbers.Add(new Number(y, myXs, int.Parse(numberValue)));
                        myXs = new List<int>();
                        numberValue = "";
                    }
                }

                // Check if the last cell of the row is part of a number
                if (numberValue != "")
                {
                    numbers.Add(new Number(y, myXs, int.Parse(numberValue)));
                }
            }

            return numbers;
        }





    }

    //symbol Part 2 is any symbol that is * symbol that is adjacneto to two numbers
    public class SymbolPart2
    {
        public int num1Value;
        public int num2Value;

        public int Value;
    }

    public class Number
    {
        public int myY;
        public List<int> myXSpan;
        public int myValue;
        public bool isAdjacentToSymbol;

        public Number(int y, List<int> xSpan, int value)
        {
            myY = y;
            myXSpan = xSpan;
            myValue = value;
        }
    }

    public class GridCell
    {
        public int myY;
        public int myX;
        public int myValue;
        public CelLType myType;
        public char myChar;

        public GridCell(int x, int y, int value, char character, CelLType type)
        {
            myX = x;
            myY = y;
            myValue = value;
            myChar = character;
            myType = type;
        }

    }

    public enum CelLType
    {
        separator,
        number,
        symbol,
    }
}