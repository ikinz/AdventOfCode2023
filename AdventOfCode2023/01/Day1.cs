using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day1 : Day
    {
        public string Part1()
        {
            List<string> rows = new List<string>(File.ReadAllLines(@"01\input.txt"));
            List<int> numbers = new List<int>();

            foreach (string row in rows)
            {
                List<string> numbersInRow = new List<string>();
                foreach (char c in row)
                {
                    if (Char.IsDigit(c))
                        numbersInRow.Add(c.ToString());
                }
                numbers.Add(int.Parse(numbersInRow[0] + numbersInRow[numbersInRow.Count - 1]));
            }

            int result = 0;
            foreach (int num in numbers)
            {
                result += num;
            }
            return result.ToString();
        }

        private struct Occurrence
        {
            public int Index;
            public string NumberKey;
        }

        private List<Occurrence> FindAllOccurrences(string mainString, string[] searchStrings)
        {
            List<Occurrence> occurrences = new List<Occurrence>();

            // For every key
            foreach (string searchString in searchStrings)
            {
                // Match until no more matches
                int index = -1;
                do
                {
                    index = mainString.IndexOf(searchString, index + 1);
                    if (index != -1)
                    {
                        // Save key and it's index in the string
                        occurrences.Add(new Occurrence()
                        {
                            Index = index,
                            NumberKey = searchString
                        });
                    }
                } while (index != -1);
            }

            // Sort by index
            occurrences = occurrences.OrderBy(x => x.Index).ToList();

            return occurrences;
        }

        public string Part2()
        {
            // Map used both as a key-set when searching, and as a simple way to convert a match to an integer
            Dictionary<string, int> convertingTable = new Dictionary<string, int>()
            {
                { "0", 0 },
                { "1", 1 },
                { "2", 2 },
                { "3", 3 },
                { "4", 4 },
                { "5", 5 },
                { "6", 6 },
                { "7", 7 },
                { "8", 8 },
                { "9", 9 },
                { "zero", 0 },
                { "one", 1 },
                { "two", 2 },
                { "three", 3 },
                { "four", 4 },
                { "five", 5 },
                { "six", 6 },
                { "seven", 7 },
                { "eight", 8 },
                { "nine", 9 }
            };

            // Read the file
            List<string> rows = new List<string>(File.ReadAllLines(@"01\input.txt"));
            int result = 0;

            foreach (string row in rows)
            {
                // Get list of occurrences sorted by index
                List<Occurrence> occurrencesInRow = FindAllOccurrences(row, convertingTable.Keys.ToArray());
                // Get First and last numbers
                int num1 = convertingTable[occurrencesInRow[0].NumberKey];
                int num2 = convertingTable[occurrencesInRow[occurrencesInRow.Count - 1].NumberKey];
                // Add the numbers together and add to result
                result += ((num1 * 10) + num2);
            }

            return result.ToString();
        }
    }
}
