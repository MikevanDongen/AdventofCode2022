using System.Diagnostics;
namespace Day11;

[DebuggerDisplay("{Type} {Value}")]
internal record Operation(OperationType Type, ulong? Value = default)
{
    public ulong Execute(ulong worryLevel) => Type switch
    {
        OperationType.Add => worryLevel + Value!.Value,
        OperationType.Multiply => worryLevel * Value!.Value,
        OperationType.Square => worryLevel * worryLevel,
        _ => throw new NotImplementedException()
    };
}
