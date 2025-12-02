// DAY 1 Part 1

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
        else{
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