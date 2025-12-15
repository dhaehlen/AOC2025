using System.IO.Compression;

public static class Day3
{
    public static void Part1(string path = "../../../Data/day3.txt")
    {   
        using StreamReader file = new(path);
        string? currentLine;
        nint joltsTotal = 0;
        while((currentLine = file.ReadLine()) is not null)
        {
            char[] batteryJolts = [currentLine[0], currentLine[1]];
            for(int i = 2; i < currentLine.Length; i++)
            {
                if(batteryJolts[0] < batteryJolts[1])
                {
                    batteryJolts[0] = batteryJolts[1];
                    batteryJolts[1] = currentLine[i];
                }
                else if (batteryJolts[1] < currentLine[i])
                {
                    batteryJolts[1] = currentLine[i];
                }
            }
            _ = int.TryParse(new string(batteryJolts), out int bankJolts);
            joltsTotal += bankJolts; 
            Console.WriteLine($"Bank: {currentLine} Jolts: {bankJolts} Total Jolts: {joltsTotal}");
        }
    }

    public static void Part2(string path = "../../../Data/day3.txt")
    {
        using StreamReader file = new(path);
        string? currentLine;
        nint joltsTotal = 0;
        while((currentLine = file.ReadLine()) is not null)
        {
            int bankLength = currentLine.Length;

            //initalize the joltage to the last 12 digits of the bank
            char[] batteries = new char[12];
            for(int j = 11; j >= 0; j--)
            {
                batteries[j] = currentLine[bankLength - 1 - 11 + j];
            }

            //some sort of bubble sort
            //compare the n - 13 digit, if its greater than n - 12 (the first in the batteries)
            //swap battery 0 with the n - 13 digit. Then check if battery 1 is less than the old
            //battery 0 and swap if true. Repeat to the end of the battery or until no swap took place
            for(int i = bankLength - 1 - 12; i >= 0; i--)
            {
                char nextJoltage = currentLine[i];
                int j = 0;
                while(j < batteries.Length && nextJoltage >= batteries[j])
                {
                    (nextJoltage, batteries[j]) = (batteries[j], nextJoltage);
                    j++;                    
                }
            }
            _ = nint.TryParse(new string(batteries), out nint bankJolts);
            joltsTotal += bankJolts; 
            Console.WriteLine($"Bank: {currentLine} Jolts: {bankJolts} Total Jolts: {joltsTotal}");
        }
    }
}