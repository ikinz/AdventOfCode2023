using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day2 : Day
    {
        public Day2(string folder) : base(folder)
        {
        }

        private class Game
        {
            public int ID { get; set; }
            public List<Dictionary<string, int>> Sets { get; set; }
            public Dictionary<string, int> MaxValues { get; set; }
            public bool ValidGame { get; private set; }
            public Dictionary<string, int> FewestCubes { get; set; }
            
            public Game(string row, Dictionary<string, int> maxValues)
            {
                ValidGame = true;
                MaxValues = maxValues;
                FewestCubes = new Dictionary<string, int>();
                Sets = new List<Dictionary<string, int>>();
                ID = ParseGame(row);
            }

            private void CheckAddFewestCubes(string key, int value)
            {
                // If value is in dict
                if (FewestCubes.ContainsKey(key))
                {
                    // If higher value
                    if (value > FewestCubes[key])
                    {
                        FewestCubes[key] = value;
                    }
                }
                else
                {
                    // If not already in dict it should be added
                    FewestCubes.Add(key, value);
                }
            }

            private int ParseGame(string game)
            {
                // Split game ID and Sets
                string[] gameSplit = game.Split(':');
                int id = int.Parse(gameSplit[0].Split(' ')[1]);

                // Split Sets
                string[] setSplit = gameSplit[1].Split(';');
                foreach (string set in setSplit)
                {
                    Dictionary<string, int> setDict = new Dictionary<string, int>();

                    // Split cubes in Set
                    string[] cubeSplit = set.Split(',');
                    foreach (string cube in cubeSplit)
                    {
                        // Split nr cubes and color
                        string[] finalValues = cube.Trim().Split(' ');
                        string key = finalValues[1];
                        int value = int.Parse(finalValues[0]);
                        // Check if the game should be marked invalid
                        if (value > MaxValues[key])
                        {
                            ValidGame = false;
                        }
                        // Checks the highest nr of cubes for part 2
                        CheckAddFewestCubes(key, value);
                        // Add color and number to the set dictionary
                        setDict.Add(key, value);
                    }

                    // Add set to game
                    Sets.Add(setDict);
                }

                return id;
            }
        }

        protected override string Part1(List<string> rows)
        {
            // Max values for each cube color
            Dictionary<string, int> MaxValues = new Dictionary<string, int>()
            {
                { "red", 12 },
                { "green", 13 },
                { "blue", 14 }
            };

            int result = 0;

            foreach (string row in rows)
            {
                // Load game
                Game game = new Game(row, MaxValues);
                if (game.ValidGame)
                    result += game.ID;
            }

            return result.ToString();
        }

        protected override string Part2(List<string> rows)
        {
            // Max values for each cube color
            Dictionary<string, int> MaxValues = new Dictionary<string, int>()
            {
                { "red", 12 },
                { "green", 13 },
                { "blue", 14 }
            };

            int result = 0;

            foreach (string row in rows)
            {
                // Load game
                Game game = new Game(row, MaxValues);
                // Get power of the fewest cube set
                int power = 1;
                foreach (var cubes in game.FewestCubes)
                {
                    power *= cubes.Value;
                }
                // Add to result
                result += power;
            }

            return result.ToString();
        }
    }
}
