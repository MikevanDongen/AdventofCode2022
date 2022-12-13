using System.Diagnostics;
namespace Day11;

[DebuggerDisplay("Divisible by {DivisibleBy}?")]
internal record Test(ulong DivisibleBy, int MonkeyTrue, int MonkeyFalse)
{
    public int Execute(Item item)
    {
        return item.WorryLevel % DivisibleBy == 0 ? MonkeyTrue : MonkeyFalse;
    }

    public static ulong CalculateCommonDenominator(IEnumerable<Test> tests)
    {
        return tests.Aggregate(1UL, (result, test) => result * test.DivisibleBy);
    }
}
