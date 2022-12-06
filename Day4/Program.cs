Console.WriteLine("Part 1");
{
    var numberOfAssignmentPairs = 0;

    using var fileStream = File.OpenRead(@"C:\Repos\AdventofCode2022\Day4\input.txt");
    using var streamReader = new StreamReader(fileStream);

    do
    {
        var line = await streamReader.ReadLineAsync();
        if (line == null) break;

        var assignmentRanges = line.Split(',');
        var assignmentRangeElf1 = assignmentRanges[0].Split('-').Select(s => int.Parse(s)).ToList();
        var assignmentRangeElf2 = assignmentRanges[1].Split('-').Select(s => int.Parse(s)).ToList();

        var assignmentsElf1 = Enumerable.Range(assignmentRangeElf1[0], assignmentRangeElf1[1] - assignmentRangeElf1[0] + 1);
        var assignmentsElf2 = Enumerable.Range(assignmentRangeElf2[0], assignmentRangeElf2[1] - assignmentRangeElf2[0] + 1);

        var commonSections = assignmentsElf1.Intersect(assignmentsElf2);
        if (new[] { assignmentsElf1.Count(), assignmentsElf2.Count() }.Contains(commonSections.Count()))
        {
            numberOfAssignmentPairs++;
        }
    } while (true);

    Console.WriteLine($"NumberOfAssignmentPairs: {numberOfAssignmentPairs}");
}

Console.WriteLine();
Console.WriteLine("Part 2");
{
    var numberOfAssignmentPairs = 0;

    using var fileStream = File.OpenRead(@"C:\Repos\AdventofCode2022\Day4\input.txt");
    using var streamReader = new StreamReader(fileStream);

    do
    {
        var line = await streamReader.ReadLineAsync();
        if (line == null) break;

        var assignmentRanges = line.Split(',');
        var assignmentRangeElf1 = assignmentRanges[0].Split('-').Select(s => int.Parse(s)).ToList();
        var assignmentRangeElf2 = assignmentRanges[1].Split('-').Select(s => int.Parse(s)).ToList();

        var assignmentsElf1 = Enumerable.Range(assignmentRangeElf1[0], assignmentRangeElf1[1] - assignmentRangeElf1[0] + 1);
        var assignmentsElf2 = Enumerable.Range(assignmentRangeElf2[0], assignmentRangeElf2[1] - assignmentRangeElf2[0] + 1);

        var commonSections = assignmentsElf1.Intersect(assignmentsElf2);
        if (commonSections.Any())
        {
            numberOfAssignmentPairs++;
        }
    } while (true);

    Console.WriteLine($"NumberOfAssignmentPairs: {numberOfAssignmentPairs}");
}
