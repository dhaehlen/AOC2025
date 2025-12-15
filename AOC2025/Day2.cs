//Day 2 

static class Day2
{
    private static string[] GetRangesFromFile(string path)
    {
        using StreamReader file = new(path);
        string contents = file.ReadToEnd();

        return contents.Split(',');
    }

    private static IEnumerable<nint> RangeIterator(string range)
    {
        _ = nint.TryParse(range.Split('-')[0].Trim(), out nint rangeStart);
        _ = nint.TryParse(range.Split('-')[1].Trim(), out nint rangeEnd);

        for(nint i = rangeStart; i <= rangeEnd; i++)
        {
            yield return i;
        }
    }

    public static void Part1(string path = "../../../Data/day2.txt")
    {
        //go through every number in every range start and end inclusive
        //find mid point (this also means that odd length numbers cannot match the criteria)
        //go through each set of chars and as soon as they don't match exit
        nint answer = 0;
        string[] ranges = GetRangesFromFile(path);

        foreach(string range in ranges)
        {
            foreach (nint i in RangeIterator(range))
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

    private static bool Part2ValidateID(nint candidateID)
    {
        string candidate = candidateID.ToString();
        int len = candidate.Length;
        bool keyIsInvalid = false;
        int groupCount = 2;

        //keep dividing id into more and more groups, starting
        //with two groups (if possible).
        while(groupCount <= len && !keyIsInvalid)
        {
            //if we can't divide the ID into equal sized groups
            //of n digits then we can skip this grouping
            if(len % groupCount == 0)
            {
                int groupSize = len / groupCount;
                string pattern = candidate[..groupSize];

                //compare each group to the pattern (the first group)
                bool allGroupsMatch = true;
                for(int m = 1; m < groupCount; m++)
                {
                    int groupIndex = groupSize * m;
                    for(int n=0; n < groupSize; n++)
                    {
                        //this group does not match the first
                        //either this key is valid or we need to check
                        //smaller groups.
                        if(pattern[n] != candidate[n + groupIndex])
                        {
                            allGroupsMatch = false;
                            break;
                        } 
                    }
                    if(!allGroupsMatch){break;}
                }
                if(allGroupsMatch){keyIsInvalid=true;}
            }
            groupCount++;
        }
        return keyIsInvalid;
    }

    public static void Part2(string path = "../../../Data/day2.txt")
    {
        string[] ranges = GetRangesFromFile(path);
        nint password = 0;
        foreach(string range in ranges)
        {
            foreach (nint i in RangeIterator(range))
            {
                bool isNotValidKey = Part2ValidateID(i);
                if (isNotValidKey)
                {
                    password += i;
                }
            }            
        }
        Console.WriteLine($"Day 2 Part 2 Password: {password}");
    }
}