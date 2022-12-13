using Day11;

static async Task<long> ExecuteAsync(int numberOfRounds, ulong? reliefValue = default)
{
    using var fileStream = File.OpenRead(@"C:\Repos\AdventofCode2022\Day11\input.txt");
    using var streamReader = new StreamReader(fileStream);

    var monkeys = new List<Monkey>();
    do
    {
        var monkey = await Monkey.CreateAsync(streamReader);
        monkeys.Add(monkey);
    } while ((await streamReader.ReadLineAsync()) != null);

    ulong? commonDenominator = default;
    if (!reliefValue.HasValue) commonDenominator = Test.CalculateCommonDenominator(monkeys.Select(m => m.Test));

    for (int i = 0; i < numberOfRounds; i++)
    {
        foreach (var monkey in monkeys)
        {
            foreach (var thrownItem in monkey.InspectItems(reliefValue, commonDenominator))
            {
                monkeys[thrownItem.Monkey].CatchItem(thrownItem.Item);
            }
        }
    }

    const int numberOfMostActiveMonkeys = 2;
    return monkeys
        .OrderByDescending(m => m.InspectedItems)
        .Take(numberOfMostActiveMonkeys)
        .Aggregate(1L, (result, monkey) => result * monkey.InspectedItems);
}

Console.WriteLine($"MonkeyBusiness: {await ExecuteAsync(20, 3)}");
Console.WriteLine($"MonkeyBusiness: {await ExecuteAsync(10_000)}");
