using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day4 : Day
    {
        public Day4(string folder) : base(folder)
        {
        }

        protected override string Part1(List<string> rows)
        {
            int acc = 0;

            foreach (string row in rows)
            {
                string[] metaSplit = row.Split(':');
                string[] cardSplit = metaSplit[1].Split('|');
                string[] winningCards = cardSplit[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < winningCards.Length; i++)
                    winningCards[i] = winningCards[i].Trim();
                string[] playerCards = cardSplit[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

                int matches = 0;
                foreach (string card in playerCards)
                {
                    if (winningCards.Contains(card.Trim()))
                    {
                        matches++;
                    }
                }

                int points = 0;
                if (matches > 0)
                {
                    if (points == 0) points++;
                    points <<= (matches - 1);
                }

                acc += points;
            }

            return acc.ToString();
        }

        private class Game
        {
            public int ID { get; private set; }
            public List<string> WinningCards { get; set; }
            public List<string> PlayerCards { get; set; }
            public int Points { get; private set; }

            public Game(int id, string[] winningCards, string[] playerCards)
            {
                ID = id;
                WinningCards = new List<string>(winningCards);
                PlayerCards = new List<string>(playerCards);

                Points = CalculatePoints();
            }

            private int CalculatePoints()
            {
                int matches = 0;
                foreach (string card in PlayerCards)
                {
                    if (WinningCards.Contains(card.Trim()))
                    {
                        matches++;
                    }
                }
                return matches;
            }
        }

        /// <summary>
        /// Recursivly step through the games
        /// </summary>
        /// <param name="allGames">List of all games</param>
        /// <param name="games">Subset of games to go through</param>
        /// <returns>Accumulator</returns>
        private int PlayGame(List<Game> allGames, List<Game> games)
        {
            int acc = 0;
            for (int i = 0; i < games.Count; i++)
            {
                acc += 1;
                if (games[i].Points > 0)
                {
                    acc += PlayGame(allGames, new List<Game>(allGames.Skip(games[i].ID).Take(games[i].Points)));
                }
            }
            return acc;
        }

        protected override string Part2(List<string> rows)
        {
            List<Game> games = new List<Game>();

            foreach (string row in rows)
            {
                // Split all the things
                string[] metaSplit = row.Split(':');
                int gameID = int.Parse(metaSplit[0].Split(' ', StringSplitOptions.RemoveEmptyEntries)[1]);
                string[] cardSplit = metaSplit[1].Split('|');
                string[] winningCards = cardSplit[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string[] playerCards = cardSplit[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

                // Add game to a list
                games.Add(new Game(gameID, winningCards, playerCards));
            }

            int acc = 0;
            // For each (original) game
            foreach (Game game in games)
            {
                // Accumulate
                acc += PlayGame(games, new List<Game>() { game });
            }

            return acc.ToString();
        }
    }
}
