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
            List<string> rows = new List<string>(File.ReadAllLines("01\\01.txt"));
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

        private struct Occurance
        {
            public int Index;
            public string Number;
        }

        static List<Occurance> FindAllOccurrences(string mainString, string[] searchStrings)
        {
            List<Occurance> occurrences = new List<Occurance>();

            foreach (string searchString in searchStrings)
            {
                int index = -1;
                do
                {
                    index = mainString.IndexOf(searchString, index + 1);
                    if (index != -1)
                    {
                        occurrences.Add(new Occurance()
                        {
                            Index = index,
                            Number = searchString
                        });
                    }
                } while (index != -1);
            }

            // Sort the list of occurrences
            occurrences = occurrences.OrderBy(x => x.Index).ToList();

            return occurrences;
        }

        public string Part2()
        {
            Dictionary<string, string> ConvertingTable = new Dictionary<string, string>()
            {
                { "0", "0" },
                { "1", "1" },
                { "2", "2" },
                { "3", "3" },
                { "4", "4" },
                { "5", "5" },
                { "6", "6" },
                { "7", "7" },
                { "8", "8" },
                { "9", "9" },
                { "zero", "0" },
                { "one", "1" },
                { "two", "2" },
                { "three", "3" },
                { "four", "4" },
                { "five", "5" },
                { "six", "6" },
                { "seven", "7" },
                { "eight", "8" },
                { "nine", "9" }
            };

            List<string> rows = new List<string>(File.ReadAllLines("01\\01.txt"));
            List<int> numbers = new List<int>();

            foreach (string row in rows)
            {
                // Get list of occurances sorted by index
                List<Occurance> occuranceInRow = FindAllOccurrences(row, ConvertingTable.Keys.ToArray());
                // Get First and last numbers
                string num1 = ConvertingTable[occuranceInRow[0].Number];
                string num2 = ConvertingTable[occuranceInRow[occuranceInRow.Count - 1].Number];
                // Add the two numbers to form the final number, and add to set of numbers to add
                numbers.Add(int.Parse(num1 + num2));
            }

            // Add numbers & return
            int result = 0;
            foreach (int num in numbers)
            {
                result += num;
            }
            return result.ToString();
        }
    }
}
