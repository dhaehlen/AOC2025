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
}