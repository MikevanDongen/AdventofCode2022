using System.Text.Json.Nodes;
namespace Day13;

internal class PacketComparer : IComparer<JsonNode>
{
    public int Compare(JsonNode? x, JsonNode? y) => CompareNodes(x!, y!);

    public static int CompareNodes(JsonNode left, JsonNode right)
    {
        return (left, right) switch
        {
            (JsonArray, JsonArray) => CompareArrays(left.AsArray(), right.AsArray()),
            (JsonArray, JsonValue) => CompareArrays(left.AsArray(), new JsonArray(right.GetValue<int>())),
            (JsonValue, JsonArray) => CompareArrays(new JsonArray(left.GetValue<int>()), right.AsArray()),
            (JsonValue, JsonValue) => left.GetValue<int>().CompareTo(right.GetValue<int>()),
            _ => throw new NotImplementedException()
        };
    }

    private static int CompareArrays(JsonArray left, JsonArray right)
    {
        int i;
        for (i = 0; i < left.Count; i++)
        {
            if (i >= right.Count)
            {
                return 1;
            }

            var comparison = CompareNodes(left[i]!, right[i]!);
            if (comparison != 0)
            {
                return comparison;
            }
        }

        return i < right.Count ? -1 : 0;
    }
}