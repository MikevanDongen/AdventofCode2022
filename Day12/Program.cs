using Day12;

using var fileStream = File.OpenRead(@"C:\Repos\AdventofCode2022\Day12\input.txt");
using var streamReader = new StreamReader(fileStream);

var map = new List<Position[]>();
do
{
    var line = await streamReader.ReadLineAsync();
    if (line == null) break;

    map.Add(line.Select((c, index) => new Position(map, x: index, y: map.Count, character: c)).ToArray());
} while (true);

var endPosition = map.SelectMany(row => row.Where(p => p.IsEnd)).Single();

Console.WriteLine("Part 1:");
{
    var startPosition = map.SelectMany(row => row.Where(p => p.IsStart)).Single();
    var route = startPosition.CalculateRoute(endPosition)!;
    Console.WriteLine($"Steps: {route.Size}");
}
Console.WriteLine();
Console.WriteLine("Part 2:");
{
    var fewestSteps = int.MaxValue;
    var startPositions = map.SelectMany(row => row.Where(p => p.Elevation == 0));
    foreach (var startPosition in startPositions)
    {
        var route = startPosition.CalculateRoute(endPosition);
        if (route != null)
        {
            fewestSteps = Math.Min(fewestSteps, route.Size);
        }
    }
    Console.WriteLine($"Fewest steps: {fewestSteps}");
}
