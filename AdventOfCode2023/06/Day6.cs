using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day6 : Day
    {
        public Day6(string folder) : base(folder)
        {
        }

        private class Race
        {
            public long RaceTime { get; private set; }
            public long Distance { get; private set; }

            public Race(long raceTime, long distance)
            {
                RaceTime = raceTime;
                Distance = distance;
            }

            private long GetMinHold()
            {
                for (long i = 0; i < RaceTime; i++)
                {
                    if ((RaceTime - i) * i > Distance)
                        return i;
                }
                return 0;
            }

            private long GetMaxHold()
            {
                for (long i = RaceTime; i > 0; i--)
                {
                    if ((RaceTime - i) * i > Distance)
                        return i;
                }
                return 0;
            }

            public long GetNumHolds()
            {
                long min = GetMinHold();
                long max = GetMaxHold();
                return max - min + 1;
            }
        }

        private List<Race> GetRaceCollection(List<string> rows)
        {
            string[] times;
            string[] distances;

            times = rows[0].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            distances = rows[1].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

            List<Race> raceCollection = new List<Race>();
            for (int i = 0; i < times.Length; i++)
                raceCollection.Add(new Race(long.Parse(times[i]), long.Parse(distances[i])));
            return raceCollection;
        }

        protected override string Part1(List<string> rows)
        {
            List<Race> races = GetRaceCollection(rows);

            long acc = 0;
            foreach (Race race in races)
            {
                long holds = race.GetNumHolds();
                if (holds > 0)
                {
                    if (acc == 0) acc++;
                    acc *= holds;
                }
            }

            return acc.ToString();
        }

        protected override string Part2(List<string> rows)
        {
            List<Race> races = GetRaceCollection(rows);
            
            string time = "";
            string distance = "";
            foreach (Race race in races)
            {
                time += race.RaceTime.ToString();
                distance += race.Distance.ToString();
            }

            return new Race(long.Parse(time), long.Parse(distance)).GetNumHolds().ToString();
        }
    }
}
