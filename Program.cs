using NUnit.Framework;
using System.Text.RegularExpressions;

namespace PathFinder_2
{
    internal class Program
    {
        public class Finder
        {
            private class Cell
            {
                public readonly int row;
                public readonly int col;
                public readonly int distance;

                public Cell(int row, int col, int distance)
                {
                    this.row = row;
                    this.col = col;
                    this.distance = distance;
                }
            }

            private static List<(int, int)> GetNeighbors(string[] grid, int row, int col)
            {
                List<(int, int)> neighbors = new();
                if (row > 0 && grid[row - 1][col] != 'W')
                {
                    neighbors.Add((row - 1, col));
                }
                if (row < grid.Length - 1 && grid[row + 1][col] != 'W')
                {
                    neighbors.Add((row + 1, col));
                }
                if (col > 0 && grid[row][col - 1] != 'W')
                {
                    neighbors.Add((row, col - 1));
                }
                if (col < grid.Length - 1 && grid[row][col + 1] != 'W')
                {
                    neighbors.Add((row, col + 1));
                }
                return neighbors;
            }

            public static int PathFinder(string maze)
            {
                string[] grid = Regex.Split(maze, @"(?:\r\n)|(?:\n)|(?:\r)");
                bool[,] visited = new bool[grid.Length, grid.Length];
                Queue<Cell> queue = new();
                queue.Enqueue(new Cell(0, 0, 0));
                while (queue.Count > 0)
                {
                    Cell node = queue.Dequeue();
                    if (node.row == grid.Length - 1 && node.col == grid.Length - 1)
                    {
                        return node.distance;
                    }
                    if (visited[node.row, node.col])
                    {
                        continue;
                    }
                    visited[node.row, node.col] = true;
                    List<(int, int)> neighbors = GetNeighbors(grid, node.row, node.col);
                    foreach ((int, int) neighbor in neighbors)
                    {
                        queue.Enqueue(new Cell(neighbor.Item1, neighbor.Item2, node.distance + 1));
                    }
                }
                return -1;
            }
        }
        static void Main(string[] args)
        {
            string a = ".W.\n" +
                   ".W.\n" +
                   "...",

               b = ".W.\n" +
                   ".W.\n" +
                   "W..",

               c = "......\n" +
                   "......\n" +
                   "......\n" +
                   "......\n" +
                   "......\n" +
                   "......",

               d = "......\n" +
                   "......\n" +
                   "......\n" +
                   "......\n" +
                   ".....W\n" +
                   "....W.";

            Assert.AreEqual(4, Finder.PathFinder(a));
            Assert.AreEqual(-1, Finder.PathFinder(b));
            Assert.AreEqual(10, Finder.PathFinder(c));
            Assert.AreEqual(-1, Finder.PathFinder(d));
        }
    }
}