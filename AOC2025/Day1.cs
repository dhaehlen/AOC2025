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
    
    public static void Part2()
    {
        int password = 0;
        int currentValue = 50;
        int previousCentury = 0;

        try
        {
            using StreamReader file = new("../../../Data/day1.txt");
            string? line;
            while ((line = file.ReadLine()) is not null)
            {
                Console.WriteLine($"Current Value: {currentValue}");

                //Parse Line: We expect [L|R]###..
                char rotationDirection = line[0];
                bool success = int.TryParse(line[1..], out int rotationDistance);

                if (!success) { throw new ArgumentException($"Could Not Parse Rotation Distance in {line}"); }

                if (rotationDirection == 'L')
                {
                    //L Subtract
                    Console.WriteLine($"Subtract: {rotationDistance}");
                    currentValue -= rotationDistance;
                }
                else if (rotationDirection == 'R')
                {
                    //R Add
                    Console.WriteLine($"Add: {rotationDistance}");
                    currentValue += rotationDistance;
                }
                else
                {
                    throw new ArgumentException($"Invalid Rotation Direction in {line}");
                }

                //Count Century Crossings (equivilant to crossing 0)
                //> Have to watch out for negative values here
                // -100------------0-----------100-----------200
                //   |  Century -1 | Century 0  |  Century 1  |
                // Century [0..99] = 0
                // Century [-1..-100] = -1
                int currentCentury = currentValue < 0 ? (currentValue + 1) / 100 - 1 : currentValue / 100;

                if (currentCentury != previousCentury)
                {
                    Console.WriteLine($"Century Boundary Crossed: {previousCentury} -> {currentCentury}");
                    password += Math.Abs(currentCentury - previousCentury);
                    Console.WriteLine($"Password Incremented: {password}");
                    previousCentury = currentCentury;
                }
                else if (currentCentury == previousCentury && currentValue % 100 == 0)
                {
                    password++;
                    Console.WriteLine($"Landed on Current Century Boundary Password Incremented: {password}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        Console.WriteLine($"The password is: {password}");
    }
}