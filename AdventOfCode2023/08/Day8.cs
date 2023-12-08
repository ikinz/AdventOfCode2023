using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace AdventOfCode2023
{
    internal class Day8 : Day
    {
        public Day8(string folder) : base(folder)
        {
        }

        private struct Jump
        {
            public string Left;
            public string Right;

            public Jump(string left, string right)
            {
                Left = left;
                Right = right;
            }
        }

        private Dictionary<string, Jump> GetNodes(List<string> rows)
        {
            Dictionary<string, Jump> nodes = new Dictionary<string, Jump>();
            for (int i = 2; i < rows.Count; i++)
            {
                string fixedRow = rows[i].Replace(" ", "").Replace("(", "").Replace(")", "");
                string[] titleSplit = fixedRow.Split('=');
                string title = titleSplit[0];
                string[] jumpSplit = titleSplit[1].Split(",");
                string left = jumpSplit[0];
                string right = jumpSplit[1];

                nodes.Add(title, new Jump(left, right));
            }
            return nodes;
        }

        protected override string Part1(List<string> rows)
        {
            string instructions = rows[0];

            Dictionary<string, Jump> nodes = GetNodes(rows);

            string currentPos = "AAA";
            int jmpCount = 0;
            bool done = false;
            while (!done)
            {
                foreach (char jump in instructions)
                {
                    if (currentPos.Equals("ZZZ"))
                    {
                        done = true;
                        break;
                    }

                    jmpCount++;

                    if (jump.Equals('L'))
                    {
                        currentPos = nodes[currentPos].Left;
                    }
                    else if (jump.Equals('R'))
                    {
                        currentPos = nodes[currentPos].Right;
                    }
                }
            }

            return jmpCount.ToString();
        }

        private struct Node
        {
            public string Position;
            public bool Done;

            public Node(string position, bool done)
            {
                Position = position;
                Done = done;
            }
        }

        private long LCM(IEnumerable<long> nums)
        {
            long lcm = nums.First();

            foreach (long num in nums.Skip(1))
            {
                lcm = (lcm * num) / GCD(lcm, num);
            }

            return lcm;
        }

        private long GCD(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        protected override string Part2(List<string> rows)
        {
            string instructions = rows[0];

            Dictionary<string, Jump> nodes = GetNodes(rows);

            List<string> currentPos = nodes.Keys.Where(x => x.EndsWith('A')).ToList();
            var paths = currentPos.Select((x, i) => (x, i)).ToDictionary(t => t.i, _ => (found: false, steps: 0L));
            int inst = 0;
            int jumps = 0;

            while (paths.Values.Any(x => !x.found))
            {
                jumps++;

                currentPos = currentPos.Select(x => instructions[inst] == 'L' ? nodes[x].Left : nodes[x].Right).ToList();

                var endSel = currentPos.Select((x, i) => (isEnd: x.EndsWith('Z'), i))
                    .Where(t => t.isEnd);

                foreach (var x in endSel)
                {
                    paths[x.i] = (true, jumps);
                }

                inst = (inst + 1) % instructions.Length;
            }

            return LCM(paths.Select(x => x.Value.steps)).ToString();
        }
    }
}
