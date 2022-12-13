namespace Day12;

internal class Position
{
    private readonly Lazy<List<Position>> _neighbouringPositionsToConsider;

    public int Elevation { get; }
    public bool IsStart { get; }
    public bool IsEnd { get; }

    public Position(List<Position[]> map, int x, int y, char character)
    {
        if (character == 'S')
        {
            IsStart = true;
            Elevation = 0;
        }
        else if (character == 'E')
        {
            IsEnd = true;
            Elevation = 'z' - 'a';
        }
        else
        {
            Elevation = character - 'a';
        }

        _neighbouringPositionsToConsider = new(() =>
        {
            var mapWidth = map[0].Length;
            var mapHeight = map.Count;
            var list = new List<Position>();

            if (x > 0) list.Add(map[y][x - 1]);
            if (x < mapWidth - 1) list.Add(map[y][x + 1]);
            if (y > 0) list.Add(map[y - 1][x]);
            if (y < mapHeight - 1) list.Add(map[y + 1][x]);

            return list
                .Where(neighbouringPosition => neighbouringPosition.Elevation - 1 <= Elevation)
                .ToList();
        });
    }

    public RouteStep? CalculateRoute(Position endPosition)
    {
        var routeSteps = new List<RouteStep> { new RouteStep(null, this) };

        var routes = new Routes();
        routes.TryAddRouteStep(routeSteps[0]);

        do
        {
            List<RouteStep> nextRouteSteps = new List<RouteStep>();
            foreach (var routeStep in routeSteps)
            {
                nextRouteSteps.AddRange(routeStep.Position.CalculateRoute(routes, routeStep));
            }
            routeSteps = nextRouteSteps;
        } while (routeSteps.Count > 0);

        return routes.Get(endPosition);
    }

    private List<RouteStep> CalculateRoute(Routes routes, RouteStep current)
    {
        var goodRouteSteps = new List<RouteStep>();
        foreach (var neigbouringPosition in _neighbouringPositionsToConsider.Value)
        {
            var routeStep = new RouteStep(current, neigbouringPosition);
            if (routes.TryAddRouteStep(routeStep))
            {
                if (routeStep.Position.IsEnd) return new List<RouteStep> { routeStep };

                goodRouteSteps.Add(routeStep);
            }
        }

        return goodRouteSteps;
    }
}