Console.WriteLine("Part 1");
{
    var highest = new { NumberOfCalories = -1, ElfNumber = -1 };
    var currentElfNumber = 1;
    var currentNumberOfCalories = 0;

    using var fileStream = File.OpenRead(@"C:\Repos\AdventofCode2022\Day1\input.txt");
    using var streamReader = new StreamReader(fileStream);

    do
    {
        var line = await streamReader.ReadLineAsync();

        if (line == null)
        {
            break;
        }
        else if (line == string.Empty)
        {
            if (currentNumberOfCalories > highest.NumberOfCalories)
            {
                highest = new { NumberOfCalories = currentNumberOfCalories, ElfNumber = currentElfNumber };
            }

            currentElfNumber++;
            currentNumberOfCalories = 0;
            continue;
        }

        currentNumberOfCalories += int.Parse(line);
    } while (true);

    Console.WriteLine($"Highest: elf {highest.ElfNumber} has {highest.NumberOfCalories} calories.");
}

Console.WriteLine();
Console.WriteLine("Part 2");
{
    var highest = new List<(int NumberOfCalories, int ElfNumber)> { (-1, -1) };
    var currentElfNumber = 1;
    var currentNumberOfCalories = 0;

    using var fileStream = File.OpenRead(@"C:\Repos\AdventofCode2022\Day1\input.txt");
    using var streamReader = new StreamReader(fileStream);

    while (true)
    {
        var line = await streamReader.ReadLineAsync();

        if (line == null || line == string.Empty)
        {
            if (currentNumberOfCalories > highest.Min(h => h.NumberOfCalories))
            {
                highest = highest
                    .Append((NumberOfCalories: currentNumberOfCalories, ElfNumber: currentElfNumber))
                    .OrderByDescending(h => h.NumberOfCalories)
                    .Take(3)
                    .ToList();
            }

            currentElfNumber++;
            currentNumberOfCalories = 0;

            if (line == null) break;
            continue;
        }

        currentNumberOfCalories += int.Parse(line);
    }

    foreach (var (NumberOfCalories, ElfNumber) in highest)
    {
        Console.WriteLine($"Highest: elf {ElfNumber} has {NumberOfCalories} calories.");
    }

    Console.WriteLine($"Total: {highest.Sum(h => h.NumberOfCalories)} calories.");
}
