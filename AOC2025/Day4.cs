//AoC Day 4

using System.Security.Cryptography.X509Certificates;

public static class Day4
{
    private static List<string> GetRollLayout(string path)
    {
        using StreamReader file = new(path);
        List<string> rollLayout = [];
        string? line;
        while((line = file.ReadLine()) != null)
        {
            rollLayout.Add(line);
        }
        return rollLayout;
    }
    public static void Part1(string path="../../../Data/day4.txt")
    {
        List<string> layout = GetRollLayout(path);
         
        //get dimensions
        int width = layout[0].Length;
        int height = layout.Count;

        int accessibleRoleCount = 0;

        //check corners (they are always available)
        if(layout[0][0] == '@'){accessibleRoleCount++;}
        if(layout[0][width - 1] == '@'){accessibleRoleCount++;}
        if(layout[height - 1][0] == '@'){accessibleRoleCount++;}
        if(layout[height - 1][width - 1] == '@'){accessibleRoleCount++;}

        //check roll slots along edges (except corners)
        //top edge
        for(int i = 1; i < width - 1; i++)
        {
            //x@x
            //xxx'
            int nearbyRollCount = 0;
            if(layout[0][i] != '@'){continue;}
            if(layout[0][i - 1] == '@'){nearbyRollCount++;}
            if(layout[0][i + 1] == '@'){nearbyRollCount++;}
            if(layout[0 + 1][i - 1] == '@'){nearbyRollCount++;}
            if(layout[0 + 1][i] == '@'){nearbyRollCount++;}
            if(layout[0 + 1][i + 1] == '@'){nearbyRollCount++;}
            if(nearbyRollCount < 4){accessibleRoleCount++;}
        }
        //bottom edge
        for(int j = 1; j < width - 1; j++)
        {
            //xxx
            //x@x'
            int nearbyRollCount = 0;
            if(layout[height - 1][j] != '@'){continue;}
            if(layout[height - 2][j - 1] == '@'){nearbyRollCount++;}
            if(layout[height - 2][j] == '@'){nearbyRollCount++;}
            if(layout[height - 2][j + 1] == '@'){nearbyRollCount++;}
            if(layout[height - 1][j - 1] == '@'){nearbyRollCount++;}
            if(layout[height - 1][j + 1] == '@'){nearbyRollCount++;}
            if(nearbyRollCount < 4){accessibleRoleCount++;}
        }
        //left edge
        for(int k = 1; k < height - 1; k++)
        {
            //xx
            //@x
            //xx
            int nearbyRollCount = 0;
            if(layout[k][0] != '@'){continue;}
            if(layout[k - 1][0] == '@'){nearbyRollCount++;}
            if(layout[k - 1][1] == '@'){nearbyRollCount++;}
            if(layout[k][1] == '@'){nearbyRollCount++;}
            if(layout[k + 1][0] == '@'){nearbyRollCount++;}
            if(layout[k + 1][1] == '@'){nearbyRollCount++;}
            if(nearbyRollCount < 4){accessibleRoleCount++;}
        }
        //right edge
        for(int k = 1; k < height - 1; k++)
        {
            //xx
            //x@
            //xx
            int nearbyRollCount = 0;
            if(layout[k][width - 1] != '@'){continue;}
            if(layout[k - 1][width - 1] == '@'){nearbyRollCount++;}
            if(layout[k - 1][width - 2] == '@'){nearbyRollCount++;}
            if(layout[k][width - 2] == '@'){nearbyRollCount++;}
            if(layout[k + 1][width - 1] == '@'){nearbyRollCount++;}
            if(layout[k + 1][width - 2] == '@'){nearbyRollCount++;}
            if(nearbyRollCount < 4){accessibleRoleCount++;}
        }

        //iterate remaining roll slots within the layout
        for(int row = 1; row < height - 1; row++)
        {
            for(int col = 1; col < width - 1; col++)
            {
                //ehhh it aint pretty
                int nearbyRollCount = 0;
                if(layout[row][col] != '@'){continue;}
                if(layout[row - 1][col - 1] == '@'){nearbyRollCount++;}
                if(layout[row - 1][col] == '@'){nearbyRollCount++;}
                if(layout[row - 1][col + 1] == '@'){nearbyRollCount++;}
                if(layout[row][col - 1] == '@'){nearbyRollCount++;}
                if(layout[row][col + 1] == '@'){nearbyRollCount++;}
                if(layout[row + 1][col - 1] == '@'){nearbyRollCount++;}
                if(layout[row + 1][col] == '@'){nearbyRollCount++;}
                if(layout[row + 1][col + 1] == '@'){nearbyRollCount++;}
                if(nearbyRollCount < 4){accessibleRoleCount++;}
            }
        }
        Console.WriteLine($"Accessible Rolls: {accessibleRoleCount}");
    }

    public static void Part1Test()
    {
        foreach (string path in Directory.GetFiles("../../../TestData/day4"))
        {
            Console.WriteLine($"Test Case: {path}");
            Day4.Part1(path);
        }
    }
}