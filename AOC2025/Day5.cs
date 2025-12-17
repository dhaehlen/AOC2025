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
        ranges.Clear();
        IDs.Clear();
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

    //Again enumerating the IDs feels like a bad idea becuase of the sheer
    //number of them. Alternatively we can subtrace the max from the min 
    //(being careful of the inclusive aspect) and add all the ranges. We will
    //need to account for overlapping ranges.
    public static void Part2(string path="../../../Data/day5.txt")
    {
        ranges.Clear();
        IDs.Clear();
        ParseFile(path);

        ranges.Sort((r1, r2) => r1.min.CompareTo(r2.min));

        nint totalIDs = 0;
        nint currentMin = ranges[0].min;
        nint currentMax = ranges[0].max;
        for(int i = 0; i < ranges.Count; i++)
        {
            if(ranges[i].min > currentMax)
            {
                totalIDs += currentMax - currentMin + 1; //because inclusivity of range
                currentMin = ranges[i].min;
                currentMax = ranges[i].max; 
            }
            else
            {
                currentMax = ranges[i].max;
            }
        }
        totalIDs += currentMax - currentMin + 1;
        Console.WriteLine($"Total Fresh IDs: {totalIDs}");
    }
}