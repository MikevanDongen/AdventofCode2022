Console.WriteLine("Part 1");
{
    var sumOfPriorities = 0;

    using var fileStream = File.OpenRead(@"C:\Repos\AdventofCode2022\Day3\input.txt");
    using var streamReader = new StreamReader(fileStream);

    do
    {
        var line = await streamReader.ReadLineAsync();
        if (line == null) break;

        var compartment1 = line.Substring(0, line.Length / 2).ToCharArray().Distinct();
        var compartment2 = line.Substring(line.Length / 2).ToCharArray().Distinct();
        var itemTypeInBothCompartments = compartment1.Intersect(compartment2).Single();
        var priority = itemTypeInBothCompartments - (itemTypeInBothCompartments < 97 ? 38 : 96);
        sumOfPriorities += priority;
    } while (true);

    Console.WriteLine($"SumOfPriorities: {sumOfPriorities}");
}

Console.WriteLine();
Console.WriteLine("Part 2");
{
    var sumOfPriorities = 0;

    using var fileStream = File.OpenRead(@"C:\Repos\AdventofCode2022\Day3\input.txt");
    using var streamReader = new StreamReader(fileStream);

    do
    {
        var items1 = (await streamReader.ReadLineAsync())?.ToCharArray();
        if (items1 == null) break;
        var items2 = (await streamReader.ReadLineAsync())!.ToCharArray();
        var items3 = (await streamReader.ReadLineAsync())!.ToCharArray();

        var itemTypeInAllRucksacks = items1.Intersect(items2).Intersect(items3).Single();
        var priority = itemTypeInAllRucksacks - (itemTypeInAllRucksacks < 97 ? 38 : 96);
        sumOfPriorities += priority;
    } while (true);

    Console.WriteLine($"SumOfPriorities: {sumOfPriorities}");
}
