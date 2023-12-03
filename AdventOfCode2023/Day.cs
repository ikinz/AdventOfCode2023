using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public enum Part
    {
        One,
        Two
    }

    public abstract class Day
    {
        const string INPUT = "input.txt";
        const string SAMPLE1 = "sample1.txt";
        const string SAMPLE2 = "sample2.txt";

        public string Folder { get; private set; }
        public bool Sample { get; set; } = false;

        private bool ReadAllLines(Part part, out string[]? lines)
        {
            string file;
            lines = null;

            if (Sample)
            {
                if (part == Part.One)
                {
                    file = $"{Folder}{SAMPLE1}";
                }
                else
                {
                    file = $"{Folder}{SAMPLE2}";
                }
            }
            else
            {
                file = $"{Folder}{INPUT}";
            }

            if (!File.Exists(file))
                return false;

            lines = File.ReadAllLines(file);

            return true;
        }

        protected abstract string Part1(List<string> rows);

        protected abstract string Part2(List<string> rows);

        public string Run(Part part)
        {
            if (!ReadAllLines(part, out string[]? lines))
                return "<no input>";

            if (lines == null || lines.Length < 1)
                return "<no input>";

            if (part == Part.One)
            {
                return Part1(new List<string>(lines));
            }
            else
            {
                return Part2(new List<string>(lines));
            }
        }

        public Day(string folder) => Folder = folder;
    }
}
