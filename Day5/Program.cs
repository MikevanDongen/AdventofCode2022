Console.WriteLine("Part 1");
{
    using var fileStream = File.OpenRead(@"C:\Repos\AdventofCode2022\Day5\input.txt");
    using var streamReader = new StreamReader(fileStream);

    var line = (await streamReader.ReadLineAsync())!;
    var numberOfStacks = line.Length / 4 + 1;
    var initialCrates = new List<List<string>>();
    for (int stackIndex = 0; stackIndex < numberOfStacks; stackIndex++)
    {
        initialCrates.Add(new List<string>());
    }

    do
    {
        if (line == string.Empty) break;

        if (line[0] == '[')
        {
            for (int stackIndex = 0; stackIndex < numberOfStacks; stackIndex++)
            {
                var value = line.Substring(stackIndex * 4 + 1, 1);
                if (value != " ")
                {
                    initialCrates[stackIndex].Add(value);
                }
            }
        }

        line = (await streamReader.ReadLineAsync())!;
    } while (true);

    var stacks = new Stack<string>[numberOfStacks];
    for (int stackIndex = 0; stackIndex < stacks.Length; stackIndex++)
    {
        stacks[stackIndex] = new Stack<string>(initialCrates[stackIndex].Reverse<string>());
    }

    do
    {
        line = await streamReader.ReadLineAsync();
        if (line == null) break;

        var splitLine = line.Split(' ');
        var count = int.Parse(splitLine[1]);
        var source = int.Parse(splitLine[3]) - 1;
        var destination = int.Parse(splitLine[5]) - 1;

        for (int i = 0; i < count; i++)
        {
            var crate = stacks[source].Pop();
            stacks[destination].Push(crate);
        }
    } while (true);

    Console.Write("Result: ");
    for (int stackIndex = 0; stackIndex < stacks.Length; stackIndex++)
    {
        if (stacks[stackIndex].TryPeek(out var crate))
        {
            Console.Write(crate);
        }
        else
        {
            Console.Write(' ');
        }
    }
    Console.WriteLine();
}

Console.WriteLine();
Console.WriteLine("Part 2");
{
    using var fileStream = File.OpenRead(@"C:\Repos\AdventofCode2022\Day5\input.txt");
    using var streamReader = new StreamReader(fileStream);

    var line = (await streamReader.ReadLineAsync())!;
    var numberOfStacks = line.Length / 4 + 1;
    var initialCrates = new List<List<string>>();
    for (int stackIndex = 0; stackIndex < numberOfStacks; stackIndex++)
    {
        initialCrates.Add(new List<string>());
    }

    do
    {
        if (line == string.Empty) break;

        if (line[0] == '[')
        {
            for (int stackIndex = 0; stackIndex < numberOfStacks; stackIndex++)
            {
                var value = line.Substring(stackIndex * 4 + 1, 1);
                if (value != " ")
                {
                    initialCrates[stackIndex].Add(value);
                }
            }
        }

        line = (await streamReader.ReadLineAsync())!;
    } while (true);

    var stacks = new List<string>[numberOfStacks];
    for (int stackIndex = 0; stackIndex < stacks.Length; stackIndex++)
    {
        stacks[stackIndex] = initialCrates[stackIndex].Reverse<string>().ToList();
    }

    do
    {
        line = await streamReader.ReadLineAsync();
        if (line == null) break;

        var splitLine = line.Split(' ');
        var count = int.Parse(splitLine[1]);
        var source = int.Parse(splitLine[3]) - 1;
        var destination = int.Parse(splitLine[5]) - 1;

        var movedStacks = stacks[source].TakeLast(count).ToList();
        stacks[source].RemoveRange(stacks[source].Count - count, count);
        movedStacks.ForEach(crate => stacks[destination].Add(crate));
    } while (true);

    Console.Write("Result: ");
    for (int stackIndex = 0; stackIndex < stacks.Length; stackIndex++)
    {
        var crate = stacks[stackIndex].LastOrDefault() ?? " ";
        Console.Write(crate);
    }
    Console.WriteLine();
}

Console.WriteLine();