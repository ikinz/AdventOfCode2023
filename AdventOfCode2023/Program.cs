using AdventOfCode2023;

Dictionary<int, Day> days = new Dictionary<int, Day>()
{
    { 1, new Day1() }, // Done
    { 2, new Day2() }
};

try
{
    if (args.Length < 1)
        throw new ArgumentException("Too few arguments!");

    if (!int.TryParse(args[0], out int day))
        throw new ArgumentException("Argument not a number!");

    if (!days.ContainsKey(day) && day != 0)
        throw new ArgumentException("Day not implemented!");

    if (day == 0)
    {
        for (int i = 1; i <= days.Count; i++)
        {
            PrintDay(days, i);
        }
    }
    else
    {
        PrintDay(days, day);
    }
    //Console.ReadKey();
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

static void PrintDay(Dictionary<int, Day> days, int day)
{
    Console.WriteLine($"Day {day}");
    Console.WriteLine($"------------");
    Console.WriteLine($"Part1: {days[day].Part1()}");
    Console.WriteLine($"Part2: {days[day].Part2()}");
    Console.WriteLine($"=================================");
}