using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolMaze
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int rows = new Random().Next(10, 25);
            int cols = new Random().Next(20, 50);
            bool[,] maze = GenerateMaze(rows, cols);
            int[] exitPos = GenerateExitPosition(rows, cols);
            int playerRow = 0;
            int playerCol = 0;

            while (true)
            {
                Console.Clear();
                StringBuilder mazeBuilder = DrawMaze(maze, playerRow, playerCol, exitPos);
                Console.Write(mazeBuilder.ToString());

                if (playerRow == exitPos[0] && playerCol == exitPos[1])
                {
                    Console.WriteLine("Congratulations, you won!");
                    Console.WriteLine("Generating new maze...");
                    rows = new Random().Next(5, 20);
                    cols = new Random().Next(10, 30);
                    maze = GenerateMaze(rows, cols);
                    exitPos = GenerateExitPosition(rows, cols);
                    playerRow = 0;
                    playerCol = 0;
                    continue;
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (playerRow > 0 && !maze[playerRow - 1, playerCol])
                            playerRow--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (playerRow < rows - 1 && !maze[playerRow + 1, playerCol])
                            playerRow++;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (playerCol > 0 && !maze[playerRow, playerCol - 1])
                            playerCol--;
                        break;
                    case ConsoleKey.RightArrow:
                        if (playerCol < cols - 1 && !maze[playerRow, playerCol + 1])
                            playerCol++;
                        break;
                    case ConsoleKey.R:
                        Console.WriteLine("Generating new maze...");
                        rows = new Random().Next(10, 25);
                        cols = new Random().Next(20, 50);
                        maze = GenerateMaze(rows, cols);
                        exitPos = GenerateExitPosition(rows,cols);
                        playerRow = 0;
                        playerCol = 0;
                        break;
                }
            }
        }

        static bool[,] GenerateMaze(int rows, int cols)
        {
            bool[,] maze = new bool[rows, cols];
            Random random = new Random();

            for (int row = 0; row < rows; row++)
            {
                maze[row, 0] = true;
                maze[row, cols - 1] = true;
            }

            for (int col = 0; col < cols; col++)
            {
                maze[0, col] = true;
                maze[rows - 1, col] = true;
            }

            for (int row = 2; row < rows - 2; row += 2)
            {
                for (int col = 2; col < cols - 2; col += 2)
                {
                    if (random.NextDouble() < 0.5)
                        maze[row, col + 2] = true;
                    else
                        maze[row + 2, col] = true;
                }
            }

            maze[0, 1] = false;
            maze[rows - 1, cols - 2] = false;

            Console.WriteLine("Move with arrows, regenerate maze R");
            return maze;
        }
        static int[] GenerateExitPosition(int rows, int cols)
        {
            Random random = new Random();
            int[] exitPos = new int[2];
            do
            {
                exitPos[0] = random.Next(rows);
                exitPos[1] = random.Next(cols);
            } while (exitPos[0] == 0 || exitPos[0] == rows - 1 || exitPos[1] == 0 || exitPos[1] == cols - 1);
            return exitPos;
        }
        static StringBuilder DrawMaze(bool[,] maze, int playerRow, int playerCol, int[] exitPos)
        {
            StringBuilder sb = new StringBuilder();
            for (int row = 0; row < maze.GetLength(0); row++)
            {
                for (int col = 0; col < maze.GetLength(1); col++)
                {
                    if (row == playerRow && col == playerCol)
                    {
                        sb.Append("P");
                    }
                    else if (row == exitPos[0] && col == exitPos[1])
                    {
                        sb.Append("E");
                    }
                    else if (maze[row, col])
                    {
                        sb.Append("#");
                    }
                    else
                    {
                        sb.Append(".");
                    }
                }
                sb.AppendLine();
            }
            return sb;
        }
    }
}