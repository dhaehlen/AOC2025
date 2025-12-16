//AoC Day 4

using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;

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

    private static (List<(int row, int col)> coords, int count) CheckCorners(List<string> layout)
    {
        int accessibleRoleCount = 0;
        List<(int row, int col)> removedRollCoords = [];
        int width = layout[0].Length;
        int height = layout.Count;
        if(layout[0][0] == '@')
        {
            accessibleRoleCount++;
            removedRollCoords.Add((0, 0));
        }
        if(layout[0][width - 1] == '@')
        {
            accessibleRoleCount++; 
            removedRollCoords.Add((0, width - 1));
        }
        if(layout[height - 1][0] == '@')
        {
            accessibleRoleCount++; 
            removedRollCoords.Add((height - 1, 0));
        }
        if(layout[height - 1][width - 1] == '@')
        {
            accessibleRoleCount++; 
            removedRollCoords.Add((height - 1, width - 1));
        }
        return (removedRollCoords, accessibleRoleCount);
    }
    private static (List<(int row,int col)> coords, int count) CheckTopEdge(List<string> layout)
    {
        int width = layout[0].Length;
        List<(int row, int col)> removedRollCoords = [];
        int accessibleRoleCount = 0;
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
            if(nearbyRollCount < 4)
            {
                removedRollCoords.Add((0, i));
                accessibleRoleCount++;
            }
        }
         return (removedRollCoords, accessibleRoleCount);
    }

    private static (List<(int row,int col)> coords, int count) CheckBottomEdge(List<string> layout)
    {
        int height = layout.Count;
        int width = layout[0].Length;
        List<(int row, int col)> removedRollCoords = [];
        int accessibleRoleCount = 0;
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
            if(nearbyRollCount < 4)
            {
                removedRollCoords.Add((height - 1, j));
                accessibleRoleCount++;
            }
        }
         return (removedRollCoords, accessibleRoleCount);
    }

    private static (List<(int row,int col)> coords, int count) CheckLeftEdge(List<string> layout)
    {
        List<(int row, int col)> removedRollCoords = [];
        int accessibleRoleCount = 0;
        int height = layout.Count;
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
            if(nearbyRollCount < 4)
            {
                removedRollCoords.Add((k, 0));
                accessibleRoleCount++;
            }
        }
        return (removedRollCoords, accessibleRoleCount);
    }

    private static (List<(int row, int col)> coords, int count) CheckRightEdge(List<string> layout)
    {
        List<(int row, int col)> removedRollCoords = [];
        int accessibleRoleCount = 0;
        int height = layout.Count;
        int width = layout[0].Length;
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
            if(nearbyRollCount < 4)
            {
                removedRollCoords.Add((k, width - 1));
                accessibleRoleCount++;
            }
        }
        return (removedRollCoords, accessibleRoleCount);
    }

    private static (List<(int row, int col)> coords, int count) CheckCenter(List<string> layout)
    {
        List<(int row, int col)> removedRollCoords = [];
        int accessibleRoleCount = 0; 
        int height = layout.Count;
        int width = layout[0].Length;
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
                if(nearbyRollCount < 4)
                {
                    removedRollCoords.Add((row, col));
                    accessibleRoleCount++;
                }
            }
        }
         return (removedRollCoords, accessibleRoleCount);
    }
    public static void Part1(string path="../../../Data/day4.txt")
    {
        List<string> layout = GetRollLayout(path);
         
        //get dimensions
        int width = layout[0].Length;
        int height = layout.Count;

        int accessibleRoleCount = 0;

        //check corners (they are always available)
        accessibleRoleCount += CheckCorners(layout).count;

        //check roll slots along edges (except corners)
        accessibleRoleCount += CheckTopEdge(layout).count;

        //bottom edge
        accessibleRoleCount += CheckBottomEdge(layout).count;

        //left edge
        accessibleRoleCount += CheckLeftEdge(layout).count;

        //right edge
        accessibleRoleCount += CheckRightEdge(layout).count;

        //iterate remaining roll slots within the layout
        accessibleRoleCount += CheckCenter(layout).count;
       
        Console.WriteLine($"Accessible Rolls: {accessibleRoleCount}");
    }

    public static void Part1Test()
    {
        foreach (string path in Directory.GetFiles("../../../TestData/day4"))
        {
            Console.Write($"Test Case: {path}: ");
            Day4.Part1(path);
        }
    }

    public static void Part2(string path="../../../Data/day4.txt")
    {
        List<string> layout = GetRollLayout(path);
        //get dimensions
        int width = layout[0].Length;
        int height = layout.Count;
        
        List<(int row, int col)> rollsToRemove = [];
        int accessibleRoleCount = 0;

        var (coords, count) = CheckCorners(layout);
        rollsToRemove.AddRange(coords);
        accessibleRoleCount += count;
        
        var (_, _) = CheckTopEdge(layout);
        rollsToRemove.AddRange(coords);
        accessibleRoleCount += count;

        var (_, _) = CheckBottomEdge(layout);
        rollsToRemove.AddRange(coords);
        accessibleRoleCount += count;

        var (_, _) = CheckLeftEdge(layout);
        rollsToRemove.AddRange(coords);
        accessibleRoleCount += count;

        var (_, _) = CheckRightEdge(layout);
        rollsToRemove.AddRange(coords);
        accessibleRoleCount += count;

        (List<(int row, int col)> coords, int count) result;
        while((result = CheckCenter(layout)).count != 0)
        {
            rollsToRemove.AddRange(result.coords);
            accessibleRoleCount += result.count;

            //remove rolls
            //Note: better would be if we had a list of cols per row
            foreach((int row, int col) in rollsToRemove)
            {
                char[] temp = layout[row].ToCharArray();
                temp[col] = '.';
                layout[row] = temp.ToString()!;
            }
        }
    }
}