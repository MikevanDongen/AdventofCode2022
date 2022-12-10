using var fileStream = File.OpenRead(@"C:\Repos\AdventofCode2022\Day10\input.txt");
using var streamReader = new StreamReader(fileStream);

var cycle = 0;
var register = 1;
var signalStrengths = new List<int>();
do
{
    var line = await streamReader.ReadLineAsync();
    if (line == null) break;

    var input = line.Split(' ');
    var command = input[0];
    var arguments = input[1..];

    switch (command)
    {
        case "noop":
            Execute(1, () => { });
            break;
        case "addx":
            Execute(2, () =>
            {
                register += int.Parse(arguments[0]);
            });
            break;
        default:
            throw new NotImplementedException();
    }

} while (true);

Console.WriteLine($"SignalStrength: {signalStrengths.Sum()}");

void Execute(int cyclesToComplete, Action action)
{
    for (int i = 0; i < cyclesToComplete; i++)
    {
        cycle++;

        if ((cycle - 20) % 40 == 0)
        {
            signalStrengths.Add(cycle * register);
        }

        var crtX = cycle % 40;
        Console.Write(new[] { register, register + 1, register + 2 }.Contains(crtX) ? '#' : '.');
        if (crtX == 0)
        {
            Console.WriteLine();
        }
    }

    action();
}
