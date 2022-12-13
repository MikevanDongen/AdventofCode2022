namespace Day12;

internal class Routes
{
    private readonly Dictionary<Position, RouteStep> _routes = new();

    public RouteStep? Get(Position key)
    {
        return _routes.TryGetValue(key, out var value) ? value : null;
    }

    public bool TryAddRouteStep(RouteStep routeStep)
    {
        if (!_routes!.TryGetValue(routeStep.Position, out var alternativeRouteStep) || alternativeRouteStep.Size > routeStep.Size)
        {
            _routes[routeStep.Position] = routeStep;
            return true;
        }

        return false;
    }
}
