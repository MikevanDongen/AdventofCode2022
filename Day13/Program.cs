using Day13;
using System.Text.Json.Nodes;

Console.WriteLine("Part 1:");
{
    using var fileStream = File.OpenRead(@"C:\Repos\AdventofCode2022\Day13\input.txt");
    using var streamReader = new StreamReader(fileStream);

    var sumOfIndicesInRightOrder = 0;
    var index = 0;
    do
    {
        index++;
        var left = JsonNode.Parse((await streamReader.ReadLineAsync())!)!;
        var right = JsonNode.Parse((await streamReader.ReadLineAsync())!)!;

        if (PacketComparer.CompareNodes(left, right) <= 0)
        {
            sumOfIndicesInRightOrder += index;
        }
    } while ((await streamReader.ReadLineAsync()) != null);

    Console.WriteLine($"SumOfIndicesInRightOrder: {sumOfIndicesInRightOrder}");
}
Console.WriteLine();
Console.WriteLine("Part 2:");
{
    using var fileStream = File.OpenRead(@"C:\Repos\AdventofCode2022\Day13\input.txt");
    using var streamReader = new StreamReader(fileStream);

    var divider1 = new JsonArray(new JsonArray(2));
    var divider2 = new JsonArray(new JsonArray(6));
    var packets = new List<JsonNode> { divider1, divider2 };

    do
    {
        packets.Add(JsonNode.Parse((await streamReader.ReadLineAsync())!)!);
        packets.Add(JsonNode.Parse((await streamReader.ReadLineAsync())!)!);
    } while ((await streamReader.ReadLineAsync()) != null);

    var orderedPackets = packets.Order(new PacketComparer()).ToList();
    var dividerPosition1 = orderedPackets.IndexOf(divider1) + 1;
    var dividerPosition2 = orderedPackets.IndexOf(divider2) + 1;

    Console.WriteLine($"Decoder key: {dividerPosition1 * dividerPosition2}");
}
Console.WriteLine();
