using CosmosOdysseyAPI.Models;

public class FlightFinder
{
    private readonly Dictionary<string, List<Flight>> _flightsFromOrigin;

    public FlightFinder(IEnumerable<Flight> flights)
    {
        // Initialize the flights from each origin
        _flightsFromOrigin = flights
            .GroupBy(f => f.Origin)
            .ToDictionary(g => g.Key, g => g.ToList());
    }

    public List<List<Flight>> FindAllPaths(string start, string end)
    {
        var paths = new List<List<Flight>>();
        var visited = new HashSet<string>();
        DepthFirstSearch(start, end, visited, new List<Flight>(), paths, null);
        return paths;
    }

    private void DepthFirstSearch(string current, string end, HashSet<string> visited, List<Flight> currentPath, List<List<Flight>> paths, DateTime? lastArrivalTime)
    {
        if (current == end)
        {
            // Found a path to the destination
            paths.Add(new List<Flight>(currentPath));
            return;
        }

        if (visited.Contains(current))
        {
            // Already visited this planet in the current path
            return;
        }

        visited.Add(current);

        if (_flightsFromOrigin.TryGetValue(current, out var flights))
        {
            foreach (var flight in flights)
            {
                if (lastArrivalTime == null || flight.DepartureTime > lastArrivalTime.Value.AddMinutes(15))
                {
                    currentPath.Add(flight);
                    DepthFirstSearch(flight.Destination, end, visited, currentPath, paths, flight.ArrivalTime);
                    currentPath.RemoveAt(currentPath.Count - 1);
                }
            }
        }

        visited.Remove(current);
    }
}