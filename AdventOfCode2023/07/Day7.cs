using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day7 : Day
    {
        public Day7(string folder) : base(folder)
        {
        }

        private class Hand : IComparable<Hand>
        {
            private enum HandRank
            {
                HighCard = 1,
                OnePair = 2,
                TwoPair = 3,
                ThreeOfAKind = 4,
                FullHouse = 5,
                FourOfAKind = 6,
                FiveOfAKind = 7
            }

            // A, K, Q, J, T, 9, 8, 7, 6, 5, 4, 3, or 2
            public Dictionary<char, int> CardValues { get; private set; }

            public List<int> Cards { get; private set; }
            public int Bid { get; private set; }
            public bool JokerRule { get; private set; }

            public Hand(Dictionary<char, int> cardValues, string cards, string bid) 
                : this(cardValues, cards, bid, false)
            {
            }

            public Hand(Dictionary<char, int> cardValues, string cards, string bid, bool jokerRule)
            {
                JokerRule = jokerRule;
                CardValues = cardValues;
                Cards = new List<int>();
                foreach (char card in cards)
                    Cards.Add(CardValues[card]);
                Bid = int.Parse(bid);
            }

            private bool IsFiveOfAKind(Hand hand)
            {
                return IsFiveOfAKind(hand, false);
            }

            private bool IsFiveOfAKind(Hand hand, bool blockJoker)
            {
                if (JokerRule && !blockJoker)
                {
                    if (IsFourOfAKind(hand, true))
                    {
                        var rankGroups = hand.Cards.GroupBy(x => x);
                        if (rankGroups.Any(group => group.Contains(CardValues['J'])))
                        {
                            return true;
                        }
                    }
                    if (IsFullHouse(hand, true))
                    {
                        var rankGroups = hand.Cards.GroupBy(x => x);
                        if (rankGroups.Any(group => group.Contains(CardValues['J'])))
                        {
                            return true;
                        }
                    }
                }

                return hand.Cards.Distinct().Count() == 1;
            }

            private bool IsFourOfAKind(Hand hand)
            {
                return IsFourOfAKind(hand, false);
            }

            private bool IsFourOfAKind(Hand hand, bool blockJoker)
            {
                var rankGroups = hand.Cards.GroupBy(x => x);

                if (JokerRule && !blockJoker)
                {
                    if (IsThreeOfAKind(hand, true) && rankGroups.Any(group => group.Contains(CardValues['J'])))
                    {
                        return true;
                    }
                    if (IsTwoPair(hand, true))
                    {
                        if (rankGroups.Any(group => group.Count() == 2 && group.Contains(CardValues['J']))) 
                        {
                            return true;
                        }
                    }
                }

                return rankGroups.Any(group => group.Count() == 4);
            }

            private bool IsFullHouse(Hand hand)
            {
                return IsFullHouse(hand, false);
            }

            private bool IsFullHouse(Hand hand, bool blockJoker)
            {
                var rankGroups = hand.Cards.GroupBy(x => x);

                if (JokerRule && !blockJoker)
                {
                    if (IsTwoPair(hand, true))
                    {
                        if (rankGroups.Any(group => group.Count() == 1 && group.Contains(CardValues['J'])))
                        {
                            return true;
                        }
                    }
                }

                return rankGroups.Any(group => group.Count() == 3) && rankGroups.Any(group => group.Count() == 2);
            }

            private bool IsThreeOfAKind(Hand hand)
            {
                return IsThreeOfAKind(hand, false);
            }

            private bool IsThreeOfAKind(Hand hand, bool blockJoker)
            {
                var rankGroups = hand.Cards.GroupBy(x => x);

                if (JokerRule && !blockJoker)
                {
                    if (IsOnePair(hand, true))
                    {
                        if (rankGroups.Any(group => group.Contains(CardValues['J'])))
                        {
                            return true;
                        }
                    }
                }

                return rankGroups.Any(group => group.Count() == 3);
            }

            private bool IsTwoPair(Hand hand)
            {
                return IsTwoPair(hand, false);
            }

            private bool IsTwoPair(Hand hand, bool blockJoker)
            {
                var rankGroups = hand.Cards.GroupBy(x => x);
                return rankGroups.Count(group => group.Count() == 2) == 2;
            }

            private bool IsOnePair(Hand hand)
            {
                return IsOnePair(hand, false);
            }

            private bool IsOnePair(Hand hand, bool blockJoker)
            {
                var rankGroups = hand.Cards.GroupBy(x => x);

                if (JokerRule && !blockJoker)
                {
                    if (rankGroups.Any(group => group.Contains(CardValues['J'])))
                    {
                        return true;
                    }
                }

                return rankGroups.Any(group => group.Count() == 2);
            }

            public void PrintHandTypeRank()
            {
                string card = "";
                foreach (int cardVal in Cards)
                {
                    card += CardValues.FirstOrDefault(x => x.Value == cardVal).Key;
                }

                Dictionary<int, string> handRank = new Dictionary<int, string>()
                {
                    { 1, "High card" },
                    { 2, "One pair" },
                    { 3, "Two pair" },
                    { 4, "Three of a kind" },
                    { 5, "Full house" },
                    { 6, "Four of a kind" },
                    { 7, "Five of a kind" }
                };

                Console.WriteLine($"[{card}] = {handRank[GetHandTypeRank(this)]}");
            }

            private int GetHandTypeRank(Hand hand)
            {
                if (IsFiveOfAKind(hand)) return (int)HandRank.FiveOfAKind;
                if (IsFourOfAKind(hand)) return (int)HandRank.FourOfAKind;
                if (IsFullHouse(hand)) return (int)HandRank.FullHouse;
                if (IsThreeOfAKind(hand)) return (int)HandRank.ThreeOfAKind;
                if (IsTwoPair(hand)) return (int)HandRank.TwoPair;
                if (IsOnePair(hand)) return (int)HandRank.OnePair;

                return (int)HandRank.HighCard;
            }

            public int CompareTo(Hand? other)
            {
                if (other == null) return -1;

                int handRank1 = GetHandTypeRank(this);
                int handRank2 = GetHandTypeRank(other);

                if (handRank1 > handRank2) return 1;
                if (handRank2 > handRank1) return -1;

                for (int i = 0; i < 5; i++) {
                    if (this.Cards[i] > other.Cards[i]) return 1;
                    if (other.Cards[i] > this.Cards[i]) return -1;
                }

                return 0;
            }
        }

        protected override string Part1(List<string> rows)
        {
            Dictionary<char, int> cardValues = new Dictionary<char, int>()
            {
                { '2', 2 },
                { '3', 3 },
                { '4', 4 },
                { '5', 5 },
                { '6', 6 },
                { '7', 7 },
                { '8', 8 },
                { '9', 9 },
                { 'T', 10 },
                { 'J', 11 },
                { 'Q', 12 },
                { 'K', 13 },
                { 'A', 14 }
            };

            List<Hand> hands = new List<Hand>();

            foreach (string row in rows)
            {
                string[] rowSplit = row.Split(' ');
                hands.Add(new Hand(cardValues, rowSplit[0], rowSplit[1]));
            }

            hands.Sort();

            int acc = 0;
            for (int i = 0; i < hands.Count; i++)
            {
                acc += hands[i].Bid * (i + 1);
            }

            return acc.ToString();
        }

        protected override string Part2(List<string> rows)
        {
            Dictionary<char, int> cardValues = new Dictionary<char, int>()
            {
                { 'J', 1 },
                { '2', 2 },
                { '3', 3 },
                { '4', 4 },
                { '5', 5 },
                { '6', 6 },
                { '7', 7 },
                { '8', 8 },
                { '9', 9 },
                { 'T', 10 },
                { 'Q', 11 },
                { 'K', 12 },
                { 'A', 13 }
            };

            List<Hand> hands = new List<Hand>();

            foreach (string row in rows)
            {
                string[] rowSplit = row.Split(' ');
                Hand hand = new Hand(cardValues, rowSplit[0], rowSplit[1], true);
                hands.Add(hand);
                //hand.PrintHandTypeRank();
            }

            hands.Sort();

            int acc = 0;
            for (int i = 0; i < hands.Count; i++)
            {
                acc += hands[i].Bid * (i + 1);
            }

            return acc.ToString();
        }
    }
}
