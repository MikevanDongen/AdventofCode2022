using System.Diagnostics;
namespace Day11;

[DebuggerDisplay("WorryLevel: {WorryLevel}")]
internal class Item
{
    public Item(ulong worryLevel)
    {
        WorryLevel = worryLevel;
    }

    public ulong WorryLevel { get; private set; }

    public void Inspect(Operation operation, ulong? reliefValue, ulong? commonDenominator)
    {
        ulong newWorryLevel = operation.Execute(WorryLevel);
        if (reliefValue.HasValue)
        {
            newWorryLevel /= reliefValue.Value;
        }
        else if (commonDenominator.HasValue)
        {
            newWorryLevel %= commonDenominator.Value;
        }

        WorryLevel = newWorryLevel;
    }
}
