//AoC Day 5


public static class Day5
{
    private static readonly List<(nint min, nint max)> ranges = [];
    private static readonly List<nint> IDs = [];
    private static void ParseFile(string path)
    {
        using StreamReader file = new(path);
        string line = file.ReadLine()!;
        while(line is not null  && line != "")
        {
            string[] range = line.Split("-");
            _ = nint.TryParse(range[0].Trim(), out nint min);
            _ = nint.TryParse(range[1].Trim(), out nint max);
            ranges.Add((min, max));
            line = file.ReadLine()!;
        }
        while(line is not null)
        {
            _ = nint.TryParse(line.Trim(), out nint Id);
            IDs.Add(Id);
            line = file.ReadLine()!;
        }
        return;
    }

    //Initially I was going to create a hashset with every unspoiled
    //ID and then look up each products ID. However, I have realized that
    //this hashset could potentially become ginormous.

    //Rather than find out, I think we'll sort the ranges and then compare
    //each id to the first and last values to determine if its in the range.
    public static void Part1(string path="../../../Data/day5.txt")
    {
        ParseFile(path);
        //sorting doesn't provide much so skip
        //ranges.Sort((r1, r2) => r1.min.CompareTo(r2.min));

        nint countOfFreshProducts = 0;

        foreach(nint id in IDs)
        {
            int i = 0;
            while(i < ranges.Count)
            {
                if(id >= ranges[i].min && id <= ranges[i].max)
                {
                    countOfFreshProducts++;
                    Console.WriteLine($"{id} in range {ranges[i]}");
                    break;
                }
                i++;
            }
            //Console.WriteLine($"{id} not in any range");
        }
        Console.WriteLine($"Fresh Products: {countOfFreshProducts}");
    }
}