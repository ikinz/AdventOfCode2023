﻿using System;
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

        private (long, long) Extrapolate(List<List<long>> histories)
        {
            for (int i = histories.Count - 2; i >= 0; i--)
            {
                histories[i].Insert(0, histories[i].First() - histories[i + 1].First());
                histories[i].Add(histories[i].Last() + histories[i + 1].Last());
            }

            return (histories[0].First(), histories[0].Last());
        }

        private (long, long) GetNextNumber(List<long> numbers)
        {
            List<List<long>> histories = new List<List<long>>()
            {
                numbers
            };

            bool areZero;
            int count = 0;
            do
            {
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
