using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day3 : Day
    {
        public Day3(string folder) : base(folder)
        {
        }

        private bool IsSymbol(List<string> rows, int y, int x)
        {
            if (y < 0 || y >= rows.Count)
                return false;

            if (x < 0 || x >= rows[y].Length)
                return false;

            if (char.IsDigit(rows[y][x]))
                return false;

            if (rows[y][x] == '.')
                return false;

            return true;
        }

        private bool ConnectedSymbol(List<string> rows, int y, int x)
        {
            return IsSymbol(rows, y - 1, x - 1) || IsSymbol(rows, y - 1, x) || IsSymbol(rows, y - 1, x + 1)
                || IsSymbol(rows, y, x - 1) || IsSymbol(rows, y, x + 1)
                || IsSymbol(rows, y + 1, x - 1) || IsSymbol(rows, y + 1, x) || IsSymbol(rows, y + 1, x + 1);
        }

        protected override string Part1(List<string> rows)
        {
            int accumulator = 0;
            string currentNumber = "";
            bool connectedSymbol = false;

            // For every row
            for (int y = 0; y < rows.Count; y++)
            {
                // If the last row ended on a number, handle it
                if (connectedSymbol)
                    accumulator += int.Parse(currentNumber);
                currentNumber = "";
                connectedSymbol = false;

                // For every letter
                for (int x = 0;  x < rows[y].Length; x++)
                {
                    // If it's not a digit
                    if (rows[y][x] == '.' || IsSymbol(rows, y, x))
                    {
                        // Handle data from last number
                        if (connectedSymbol)
                            accumulator += int.Parse(currentNumber);
                        currentNumber = "";
                        connectedSymbol = false;
                    }
                    // If it's a digit
                    else if (char.IsDigit(rows[y][x]))
                    {
                        // Add the digit to the whole number
                        currentNumber += rows[y][x];
                        // If no connectedsymbol has been found already
                        if (!connectedSymbol)
                            connectedSymbol = ConnectedSymbol(rows, y, x);
                    }
                }
                
            }
            // If the last row of the set ended with a number, handle it
            if (connectedSymbol)
                accumulator += int.Parse(currentNumber);

            return accumulator.ToString();
        }

        private bool IsAsterisk(List<string> rows, int y, int x, out Point? point)
        {
            point = null;

            if (y < 0 || y >= rows.Count)
                return false;

            if (x < 0 || x >= rows[y].Length)
                return false;

            if (rows[y][x] == '*')
            {
                point = new Point(x, y);
                return true;
            }

            return false;
        }

        private Point? GetConnectedAsterisk(List<string> rows, int y, int x)
        {
            Point? point;
            bool connected = IsAsterisk(rows, y - 1, x - 1, out point) || IsAsterisk(rows, y - 1, x, out point) || IsAsterisk(rows, y - 1, x + 1, out point)
                || IsAsterisk(rows, y, x - 1, out point) || IsAsterisk(rows, y, x + 1, out point)
                || IsAsterisk(rows, y + 1, x - 1, out point) || IsAsterisk(rows, y + 1, x, out point) || IsAsterisk(rows, y + 1, x + 1, out point);

            if (connected)
                return point;
            return null;
        }

        protected override string Part2(List<string> rows)
        {
            Dictionary<string, List<int>> gears = new Dictionary<string, List<int>>();

            string currentNumber = "";
            Point? asterisk = null;

            // For every row
            for (int y = 0; y < rows.Count; y++)
            {
                // If the last row ended on a number, handle it
                if (asterisk != null)
                {
                    string xy = $"{asterisk.Value.X},{asterisk.Value.Y}";
                    if (!gears.ContainsKey(xy))
                        gears.Add(xy, new List<int>());
                    gears[xy].Add(int.Parse(currentNumber));
                }
                currentNumber = "";
                asterisk = null;

                // For every letter
                for (int x = 0; x < rows[y].Length; x++)
                {
                    // If it's a digit
                    if (char.IsDigit(rows[y][x]))
                    {
                        // Add digit to whole number
                        currentNumber += rows[y][x];
                        // If a connected gear has not already been found, try to find one
                        if (asterisk == null)
                            asterisk = GetConnectedAsterisk(rows, y, x);
                    }
                    // If it's not a digit
                    else
                    {
                        // Handle data from last number
                        if (asterisk != null)
                        {
                            string xy = $"{asterisk.Value.X},{asterisk.Value.Y}";
                            if (!gears.ContainsKey(xy))
                                gears.Add(xy, new List<int>());
                            gears[xy].Add(int.Parse(currentNumber));
                        }
                        currentNumber = "";
                        asterisk = null;
                    }
                }
            }

            // If the last row of the set ended with a number, handle it
            if (asterisk != null)
            {
                string xy = $"{asterisk.Value.X},{asterisk.Value.Y}";
                if (!gears.ContainsKey(xy))
                    gears.Add(xy, new List<int>());
                gears[xy].Add(int.Parse(currentNumber));
            }

            // Accumulate all gear ratios where it has exactly two parts
            int acc = 0;
            foreach (var gear in gears.Where(x => x.Value.Count == 2))
            {
                acc += (gear.Value[0] * gear.Value[1]);
            }

            return acc.ToString();
        }
    }
}
