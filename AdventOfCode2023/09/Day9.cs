using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day9 : Day
    {
        public Day9(string folder) : base(folder)
        {
        }

        private (long prev, long next) Extrapolate(List<List<long>> histories)
        {
            for (int i = histories.Count - 2; i >= 0; i--)
            {
                histories[i].Insert(0, histories[i][0] - histories[i + 1][0]);
                histories[i].Add(histories[i][histories[i].Count - 1] + histories[i + 1][histories[i + 1].Count - 1]);
            }

            return (histories[0][0], histories[0][histories[0].Count - 1]);
        }

        private (long prev, long next) GetNextNumber(List<long> numbers)
        {
            List<List<long>> histories = new List<List<long>>()
            {
                numbers
            };

            bool areZero;
            int count = 0;
            do
            {
                areZero = false;
                List<long> history = new List<long>();

                for (int i = 1; i < histories[count].Count; i++)
                {
                    long num = histories[count][i] - histories[count][i - 1];
                    
                    history.Add(num);
                }
                areZero = history.All(x => x == 0);
                histories.Add(history);

                count++;
            } while (!areZero);

            return Extrapolate(histories);
        }

        protected override string Part1(List<string> rows)
        {
            long acc = 0;
            foreach (string row in rows)
            {
                (long prev, long next) next = GetNextNumber(row.Split(' ').Select(long.Parse).ToList());
                acc += next.next;
            }

            return acc.ToString();
        }

        protected override string Part2(List<string> rows)
        {
            long acc = 0;
            foreach (string row in rows)
            {
                (long prev, long next) next = GetNextNumber(row.Split(' ').Select(long.Parse).ToList());
                acc += next.prev;
            }

            return acc.ToString();
        }
    }
}
