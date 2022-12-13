using System.Diagnostics;
namespace Day11;

[DebuggerDisplay("{_items.Count} items, {InspectedItems} inspected items")]
internal class Monkey
{
    private readonly List<Item> _items;
    private readonly Operation _operation;

    public Test Test { get; }
    public int InspectedItems { get; private set; }

    private Monkey(List<Item> items, Operation operation, Test test)
    {
        _items = items;
        _operation = operation;
        Test = test;
    }

    public IEnumerable<(int Monkey, Item Item)> InspectItems(ulong? reliefValue, ulong? commonDenominator)
    {
        foreach (var item in _items.ToList())
        {
            _items.Remove(item);
            Inspect(item, reliefValue, commonDenominator);

            var nextMonkey = Test.Execute(item);
            yield return (nextMonkey, item);
        }
    }

    public void CatchItem(Item item)
    {
        _items.Add(item);
    }

    private void Inspect(Item item, ulong? reliefValue, ulong? commonDenominator)
    {
        item.Inspect(_operation, reliefValue, commonDenominator);
        InspectedItems++;
    }

    public static async Task<Monkey> CreateAsync(StreamReader streamReader)
    {
        await streamReader.ReadLineAsync();

        var startingItems = await ReadStartingItemsAsync(streamReader);
        var operation = await ReadOperationAsync(streamReader);
        var test = await ReadTestAsync(streamReader);

        return new Monkey(startingItems, operation, test);
    }

    private static async Task<List<Item>> ReadStartingItemsAsync(StreamReader streamReader)
    {
        var line = await streamReader.ReadLineAsync();
        return line!
            .Split(':', 2)[1]
            .Split(',', StringSplitOptions.TrimEntries)
            .Select(item => new Item(ulong.Parse(item)))
            .ToList();
    }

    private static async Task<Operation> ReadOperationAsync(StreamReader streamReader)
    {
        var line = await streamReader.ReadLineAsync();
        var operation = line!.Split("new = old ")[1];
        return operation[0] switch
        {
            '+' => new Operation(OperationType.Add, ulong.Parse(operation[2..])),
            '*' when (operation[2..] == "old") => new Operation(OperationType.Square),
            '*' => new Operation(OperationType.Multiply, ulong.Parse(operation[2..])),
            _ => throw new NotImplementedException(),
        };
    }

    private static async Task<Test> ReadTestAsync(StreamReader streamReader)
    {
        var line = await streamReader.ReadLineAsync();
        var divisibleBy = ulong.Parse(line!.Split("divisible by ")[1]);

        line = await streamReader.ReadLineAsync();
        var monkeyTrue = int.Parse(line!.Split("monkey ")[1]);

        line = await streamReader.ReadLineAsync();
        var monkeyFalse = int.Parse(line!.Split("monkey ")[1]);

        return new Test(divisibleBy, monkeyTrue, monkeyFalse);
    }
}
