using var fileStream = File.OpenRead(@"C:\Repos\AdventofCode2022\Day8\input.txt");
using var streamReader = new StreamReader(fileStream);

var map = new List<int[]>();
do
{
    var line = await streamReader.ReadLineAsync();
    if (line == null) break;

    map.Add(line.Select(c => c - '0').ToArray());
} while (true);

Console.WriteLine("Part 1");
{
    var visibleTrees = new HashSet<string>();

    LookAtTreelineX(true);
    LookAtTreelineX(false);
    LookAtTreelineY(true);
    LookAtTreelineY(false);

    Console.WriteLine(visibleTrees.Distinct().Count());

    void LookAtTreelineX(bool directionAscending)
    {
        for (int y = 0; y < map.Count; y++)
        {
            var highestTree = -1;
            for (int x = directionAscending ? 0 : map[y].Length - 1; x < map[y].Length && x > -1; x += directionAscending ? 1 : -1)
            {
                if (map[y][x] > highestTree)
                {
                    visibleTrees!.Add($"{y},{x}");
                    highestTree = map[y][x];
                }
            }
        }
    }

    void LookAtTreelineY(bool directionAscending)
    {
        for (int x = 0; x < map[0].Length; x++)
        {
            var highestTree = -1;
            for (int y = directionAscending ? 0 : map.Count - 1; y < map.Count && y > -1; y += directionAscending ? 1 : -1)
            {
                if (map[y][x] > highestTree)
                {
                    visibleTrees!.Add($"{y},{x}");
                    highestTree = map[y][x];
                }
            }
        }
    }
}

Console.WriteLine();
Console.WriteLine("Part 2");
{
    var treeValues = new Dictionary<string, int>();

    LookAtTreelineX(true);
    LookAtTreelineX(false);
    LookAtTreelineY(true);
    LookAtTreelineY(false);

    Console.WriteLine(treeValues.Values.Max());

    void LookAtTreelineX(bool directionAscending)
    {
        for (int y = 0; y < map.Count; y++)
        {
            var visibleTrees = InitializeVisibleTrees();
            for (int x = directionAscending ? 0 : map[y].Length - 1; x < map[y].Length && x > -1; x += directionAscending ? 1 : -1)
            {
                ProcessTree(y, x, ref visibleTrees);
            }
        }
    }

    void LookAtTreelineY(bool directionAscending)
    {
        for (int x = 0; x < map[0].Length; x++)
        {
            var visibleTrees = InitializeVisibleTrees();
            for (int y = directionAscending ? 0 : map.Count - 1; y < map.Count && y > -1; y += directionAscending ? 1 : -1)
            {
                ProcessTree(y, x, ref visibleTrees);
            }
        }
    }

    int[] InitializeVisibleTrees()
    {
        return Enumerable.Repeat(0, 10).ToArray();
    }

    void ProcessTree(int y, int x, ref int[] visibleTrees)
    {
        var treeSize = map[y][x];
        int value = visibleTrees[treeSize];

        var key = $"{y},{x}";
        if (!treeValues.TryAdd(key, value))
        {
            treeValues[key] *= value;
        }

        for (int i = 0; i < visibleTrees.Length; i++)
        {
            visibleTrees[i] = 1 + (i <= treeSize ? 0 : visibleTrees[i]);
        }
    }
}

Console.WriteLine();
