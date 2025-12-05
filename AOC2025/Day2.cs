//Day 2 
using System.ComponentModel.DataAnnotations;
using System.Globalization;

static class Day2
{
    public static void Part1(string path = "../../../Data/day2.txt")
    {
        //go through every number in every range start and end inclusive
        //find mid point (this also means that odd length numbers cannot match the criteria)
        //go through each set of chars and as soon as they don't match exit
        nint answer = 0;
        using StreamReader file = new(path);
        string contents = file.ReadToEnd();

        string[] ranges = contents.Split(',');

        foreach(string range in ranges)
        {
            _ = nint.TryParse(range.Split('-')[0].Trim(), out nint rangeStart);
            _ = nint.TryParse(range.Split('-')[1].Trim(), out nint rangeEnd);

            for(nint i = rangeStart; i <= rangeEnd; i++)
            {
                string id = i.ToString();
                if(id.Length % 2 != 0){continue;}
                int midString = id.Length / 2;
                
                bool isMatch = true;

                int p1 = 0;
                for(int p2 = midString; p2 < id.Length; p2++)
                {
                    if(id[p1] != id[p2]){ isMatch = false; break;}
                    p1++;
                }

                if (isMatch)
                {
                    answer += i;
                }
            }            
        }
        Console.WriteLine($"Day 2 answer: {answer}");
    }
}