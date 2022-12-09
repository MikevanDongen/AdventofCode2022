Console.WriteLine("Part 1");
{
    using var fileStream = File.OpenRead(@"C:\Repos\AdventofCode2022\Day9\input.txt");
    using var streamReader = new StreamReader(fileStream);

    var tailPosition = (X: 0, Y: 0);
    var headPosition = (X: 0, Y: 0);
    var tailPositions = new HashSet<string>() { "0,0" };
    do
    {
        var line = await streamReader.ReadLineAsync();
        if (line == null) break;

        var newRelativeHeadPosition = GetNewRelativeHeadPosition(line);
        for (int i = int.Parse(line[2..]); i > 0; i--)
        {
            headPosition = MovePosition(headPosition, newRelativeHeadPosition);
            tailPosition = CalculateNewTailPosition(tailPosition, headPosition);
            tailPositions.Add($"{tailPosition.X},{tailPosition.Y}");
        }
    } while (true);

    Console.WriteLine(tailPositions.Count);
}

Console.WriteLine();
Console.WriteLine("Part 2");
{
    using var fileStream = File.OpenRead(@"C:\Repos\AdventofCode2022\Day9\input.txt");
    using var streamReader = new StreamReader(fileStream);

    var knotPositions = Enumerable.Repeat((X: 0, Y: 0), 10).ToArray();
    var tailPositions = new HashSet<string>() { "0,0" };
    do
    {
        var line = await streamReader.ReadLineAsync();
        if (line == null) break;

        var newRelativeHeadPosition = GetNewRelativeHeadPosition(line);
        for (int i = int.Parse(line[2..]); i > 0; i--)
        {
            knotPositions[0] = MovePosition(knotPositions[0], newRelativeHeadPosition);
            for (int k = 1; k < knotPositions.Length; k++)
            {
                knotPositions[k] = CalculateNewTailPosition(knotPositions[k], knotPositions[k - 1]);
            }

            tailPositions.Add($"{knotPositions[9].X},{knotPositions[9].Y}");
        }
    } while (true);

    Console.WriteLine(tailPositions.Count);
}

(int X, int Y) GetNewRelativeHeadPosition(string line)
{
    return line[0] switch
    {
        'U' => (0, 1),
        'D' => (0, -1),
        'L' => (-1, 0),
        'R' => (1, 0),
        _ => throw new NotImplementedException(),
    };
}

(int X, int Y) MovePosition((int X, int Y) absolute, (int X, int Y) relative)
{
    return (absolute.X + relative.X, absolute.Y + relative.Y);
}

(int X, int Y) CalculateNewTailPosition((int X, int Y) tailPosition, (int X, int Y) headPosition)
{
    var headPositionRelativeToTail = (X: headPosition.X - tailPosition.X, Y: headPosition.Y - tailPosition.Y);
    return headPositionRelativeToTail switch
    {
        (-1 or 0 or 1, -1 or 0 or 1) => tailPosition,
        (int x, int y) => (tailPosition.X + Normalize(x), tailPosition.Y + Normalize(y)),
    };
}

int Normalize(int z)
{
    if (z == 0) return 0;
    return z > 0 ? 1 : -1;
}
