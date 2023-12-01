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
            Console.WriteLine($"Day {i}");
            Console.WriteLine($"------------");
            Console.WriteLine($"Part1: {days[i].Part1()}");
            Console.WriteLine($"Part2: {days[i].Part2()}");
            Console.WriteLine($"=================================");
        }
    }
    else
    {
        Console.WriteLine($"Part1: {days[day].Part1()}");
        Console.WriteLine($"Part2: {days[day].Part2()}");
    }
    //Console.ReadKey();
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}