const long TotalDiskSpace = 70_000_000;
const long RequiredFreeDiskSpace = 30_000_000;

using var fileStream = File.OpenRead(@"C:\Repos\AdventofCode2022\Day7\input.txt");
using var streamReader = new StreamReader(fileStream);

var currentDir = new Stack<string>();
var lastCommandWasLs = false;
var directorySizes = new Dictionary<string, long>() { { "/", 0 } };
do
{
    var line = await streamReader.ReadLineAsync();
    if (line == null) break;

    if (line.StartsWith("$ cd"))
    {
        ProcessCd(line[5..], ref currentDir);
        lastCommandWasLs = false;
    }
    else if (line.StartsWith("$ ls"))
    {
        lastCommandWasLs = true;
    }
    else if (lastCommandWasLs && !line.StartsWith("dir"))
    {
        var fileMetadata = line.Split(' ', 2);
        var fileSize = long.Parse(fileMetadata[0]);

        directorySizes["/"] += fileSize;
        AggregateDirectoryStack(currentDir, directory =>
        {
            var directoryName = $"/{string.Join('/', directory)}";
            if (!directorySizes.TryAdd(directoryName, fileSize))
            {
                directorySizes[directoryName] += fileSize;
            }
        });
    }
} while (true);

var totalSize = directorySizes.Sum(f => f.Value <= 100_000 ? f.Value : 0);
Console.WriteLine($"TotalSize: {totalSize}");

var diskSpaceToFree = RequiredFreeDiskSpace - (TotalDiskSpace - directorySizes["/"]);
var smallestSufficientDirectory = directorySizes.OrderBy(f => f.Value).First(f => f.Value >= diskSpaceToFree);
Console.WriteLine($"SmallestSufficientDirectory: {smallestSufficientDirectory}");

static void ProcessCd(string command, ref Stack<string> currentDir)
{
    switch (command)
    {
        case "/":
            currentDir = new Stack<string>();
            break;
        case "..":
            currentDir.Pop();
            break;
        default:
            currentDir.Push(command);
            break;
    }
}

void AggregateDirectoryStack(Stack<string> directories, Action<IEnumerable<string>> func)
{
    AggregateDirectoryEnumerable(directories.Reverse(), func);
}

void AggregateDirectoryEnumerable(IEnumerable<string> directories, Action<IEnumerable<string>> func)
{
    if (directories.Any())
    {
        func(directories);
        AggregateDirectoryEnumerable(directories.SkipLast(1), func);
    }
}