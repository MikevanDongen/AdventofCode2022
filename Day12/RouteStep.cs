namespace Day12;

internal class RouteStep
{
    public RouteStep(RouteStep? previous, Position position)
    {
        Previous = previous;
        Position = position;
        Size = previous == null ? 0 : previous.Size + 1;
    }

    public RouteStep? Previous { get; }
    public Position Position { get; }
    public int Size { get; }

    public bool DidNotPreviouslyPassPosition(Position position)
    {
        return Position != position && (Previous == null || Previous.DidNotPreviouslyPassPosition(position));
    }
}
