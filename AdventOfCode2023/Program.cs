using AdventOfCode2023;
using System.Diagnostics;

Dictionary<int, Day> days = new Dictionary<int, Day>()
{
    { 1, new Day1(@"01\") }, // Done
    { 2, new Day2(@"02\") }, // Done
    { 3, new Day3(@"03\") }, // Done
    { 4, new Day4(@"04\") }, // Done
    { 5, new Day5(@"05\") }, // Done
    { 6, new Day6(@"06\") }, // Done
    { 7, new Day7(@"07\") }, // Done
    { 8, new Day8(@"08\") }
};

try
{
    if (args.Length < 1)
        throw new ArgumentException("Too few arguments!");

    if (!int.TryParse(args[0], out int day))
        throw new ArgumentException("Argument not a number!");

    if (!days.ContainsKey(day) && day != 0)
        throw new ArgumentException("Day not implemented!");

    bool sample = false;
    if (args.Length > 1)
        sample = args[1].Equals("test");

    if (day == 0)
    {
        foreach (int key in days.Keys)
            PrintDay(days, key, sample);
    }
    else
    {
        PrintDay(days, day, sample);
    }
    //Console.ReadKey();
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
    Console.WriteLine(e.StackTrace);
}

static void PrintDay(Dictionary<int, Day> days, int day, bool sample)
{
    Stopwatch watch;

    days[day].Sample = sample;
    string sampleFlavor = sample ? " (TEST)" : "";

    Console.WriteLine($"Day {day}{sampleFlavor}");
    Console.WriteLine($"------------");
    watch = Stopwatch.StartNew();
    string res1 = days[day].Run(Part.One);
    watch.Stop();
    Console.WriteLine($"Part1: {res1} ({watch.ElapsedMilliseconds} ms)");
    watch = Stopwatch.StartNew();
    string res2 = days[day].Run(Part.Two);
    watch.Stop();
    Console.WriteLine($"Part2: {res2} ({watch.ElapsedMilliseconds} ms)");
    Console.WriteLine($"=================================");
}