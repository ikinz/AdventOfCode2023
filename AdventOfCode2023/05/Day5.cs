using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day5 : Day
    {
        public Day5(string folder) : base(folder)
        {
        }

        private class Map
        {
            public long DestinationStart { get; set; }
            public long SourceStart { get; set; }
            public long Range { get; set; }

            public bool IsInRange(long source)
            {
                long rangeDiff = source - SourceStart;
                return rangeDiff >= 0 && rangeDiff < Range;
            }
        }

        Dictionary<string, List<Map>> mapDict;
        List<long> seeds;

        private void ParseRows(List<string> rows)
        {
            foreach (string seed in rows[0].Split(' ').Skip(1))
            {
                seeds.Add(long.Parse(seed));
            }

            string key = "";

            for (int i = 2; i < rows.Count; i++)
            {
                if (rows[i].Contains(':'))
                {
                    key = rows[i].Split(' ')[0];
                    mapDict.Add(key, new List<Map>());
                }
                else if (!string.IsNullOrEmpty(rows[i]))
                {
                    string[] nums = rows[i].Split(' ');
                    mapDict[key].Add(new Map()
                    {
                        DestinationStart = long.Parse(nums[0]),
                        SourceStart = long.Parse(nums[1]),
                        Range = long.Parse(nums[2])
                    });
                }
                else
                {
                    key = "";
                }
            }
        }

        private long GetLocation(long seed)
        {
            long nextPos = seed;

            foreach (string mapKey in mapDict.Keys)
            {
                foreach (Map map in mapDict[mapKey])
                {
                    if (map.IsInRange(nextPos))
                    {
                        nextPos = map.DestinationStart + (nextPos - map.SourceStart);
                        break;
                    }
                }
            }

            return nextPos;
        }

        protected override string Part1(List<string> rows)
        {
            mapDict = new Dictionary<string, List<Map>>();
            seeds = new List<long>();
            ParseRows(rows);

            long lowestLoc = long.MaxValue;
            foreach (long seed in seeds)
            {
                long loc = GetLocation(seed);
                if (loc < lowestLoc)
                    lowestLoc = loc;
            }

            return lowestLoc.ToString();
        }

        // Omega slow brute force
        protected override string Part2(List<string> rows)
        {
            mapDict = new Dictionary<string, List<Map>>();
            seeds = new List<long>();
            ParseRows(rows);

            long lowestLoc = long.MaxValue;
            for (int i = 0; i < seeds.Count; i += 2)
            {

                for (int j = 0; j < seeds[i + 1]; j++)
                {
                    long loc = GetLocation(seeds[i] + j);
                    if (loc < lowestLoc)
                        lowestLoc = loc;
                }
            }

            return lowestLoc.ToString();
        }
    }
}
