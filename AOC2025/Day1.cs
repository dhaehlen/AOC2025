// DAY 1 Part 1 & 2
static class Day1
{
    public static void Part1()
    {
        int password = 0;
        int currentValue = 50;

        try
        {
            using StreamReader file = new("../../../Data/day1.txt");
            string? line;
            while ((line = file.ReadLine()) is not null)
            {
                //Parse Line: We expect [L|R]###..
                char rotationDirection = line[0];
                bool success = int.TryParse(line[1..], out int rotationDistance);

                if (!success) { throw new ArgumentException($"Could Not Parse Rotation Distance in {line}"); }


                if (rotationDirection == 'L')
                {
                    //L Subtract
                    currentValue -= rotationDistance;
                }
                else if (rotationDirection == 'R')
                {
                    //R Add
                    currentValue += rotationDistance;
                }
                else
                {
                    throw new ArgumentException($"Invalid Rotation Direction in {line}");
                }

                //Normalize
                currentValue %= 100;

                Console.WriteLine($"Current Value: {currentValue}");

                if (currentValue == 0)
                {
                    password++;
                    Console.WriteLine($"Password Incremented: {password}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        Console.WriteLine($"The password is: {password}");
    }
    
    public static int Part2(string path = "../../../Data/day1.txt")
    {
        int password = 0;
        int currentPosition = 50;
        bool atZeroLast = false;

        using StreamReader file = new(path);
        string? line;
        while ((line = file.ReadLine()) is not null)
        {
            //Test files have the answer marked with #
            if(line[0]=='#'){continue;}

            //Parse Line: We expect [L|R]###..
            char rotationDirection = line[0];
            bool success = int.TryParse(line[1..], out int rotationDistance);

            if (!success) { throw new ArgumentException($"Could Not Parse Rotation Distance in {line}"); }

            int lastPosition = currentPosition;

            Console.WriteLine($"At {lastPosition}; Rotate {line}");

            //Any rotation greater than +-99 will cross zero at least once
            password += rotationDistance / 100;
            int remainingDistance = rotationDistance % 100;

            if(remainingDistance == 0)
            {
                Console.WriteLine($"Current Position: {currentPosition}; Password at: {password}"); 
                continue; 
            }

            //atZeroLast avoids double counting in cases where we
            //are at zero and do multiples of full rotations.
            if (atZeroLast)
            {
                if(rotationDirection == 'L'){ currentPosition = 100 - remainingDistance;}
                if(rotationDirection == 'R'){ currentPosition = 0 + remainingDistance;}
                Console.WriteLine($"Current Position: {currentPosition}; Password at: {password}");
                atZeroLast = false;
                continue;
            }

            if (rotationDirection == 'L')
            {
                //L Subtract
                currentPosition -= remainingDistance;
            }
            else if (rotationDirection == 'R')
            {
                //R Add
                currentPosition += remainingDistance;
            }
            else
            {
                throw new ArgumentException($"Invalid Rotation Direction in {line}");
            }

            if (currentPosition == 0 || currentPosition == 100)
            {
                password++;
                currentPosition = 0;
                atZeroLast = true;
            }
            else if (currentPosition < 0)
            {
                password++;
                currentPosition += 100;
                atZeroLast = false;
            }
            else if (currentPosition > 99)
            {
                password++;
                currentPosition -= 100;
                atZeroLast = false;
            }
            else if (currentPosition > 0 && currentPosition < 100)
            {
                atZeroLast = false;
            }
            Console.WriteLine($"Current Position: {currentPosition}; Password at: {password}");
        }
        Console.WriteLine($"The password is: {password}");
        return password;
    }

    public static void Part2Test()
    {
        try
        {            
            foreach (string path in Directory.GetFiles("../../../TestData/day1"))
            {
                using StreamReader testFile = new(path);
                _ = int.TryParse(testFile.ReadLine()![1..], out int answer);
                int result = Part2(path);
                Console.WriteLine($"{path}: Expected {answer}, Got {result}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}