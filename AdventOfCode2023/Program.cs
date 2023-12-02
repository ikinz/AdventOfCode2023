using AdventOfCode2023;

Dictionary<int, Day> days = new Dictionary<int, Day>()
{
    { 1, new Day1(@"01\") }, // Done
    { 2, new Day2(@"02\") }, // Done
    { 3, new Day3(@"03\") }
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
        for (int i = 1; i <= days.Count; i++)
        {
            PrintDay(days, i, sample);
        }
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
}

static void PrintDay(Dictionary<int, Day> days, int day, bool sample)
{
    days[day].Sample = sample;
    string sampleFlavor = sample ? " (TEST)" : "";

    Console.WriteLine($"Day {day}{sampleFlavor}");
    Console.WriteLine($"------------");
    Console.WriteLine($"Part1: {days[day].Run(Part.One)}");
    Console.WriteLine($"Part2: {days[day].Run(Part.Two)}");
    Console.WriteLine($"=================================");
}