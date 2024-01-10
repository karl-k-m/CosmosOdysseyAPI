using CosmosOdysseyAPI.Models;

/// <summary>
/// Class for finding all possible paths between two locations.
/// </summary>
public class FlightFinder
{
    private readonly Dictionary<string, List<Flight>> _flightsFromOrigin;
    
    public FlightFinder(IEnumerable<Flight> flights)
    {
        _flightsFromOrigin = flights
            .GroupBy(f => f.Origin)
            .ToDictionary(g => g.Key, g => g.ToList());
    }

    /// <summary>
    /// Finds all possible paths between two locations.
    /// </summary>
    /// <param name="start">Start location</param>
    /// <param name="end">Destination</param>
    /// <returns>All possible paths</returns>
    public List<List<Flight>> FindAllPaths(string start, string end)
    {
        var paths = new List<List<Flight>>();
        var visited = new HashSet<string>();
        DepthFirstSearch(start, end, visited, new List<Flight>(), paths, null);
        return paths;
    }

    /// <summary>
    /// Uses depth-first search to find all possible paths between two locations.
    /// </summary>
    /// <param name="current">Current location</param>
    /// <param name="end">Destination</param>
    /// <param name="visited">Visited locations</param>
    /// <param name="currentPath">Current path</param>
    /// <param name="paths">All possible paths</param>
    /// <param name="lastArrivalTime">Last arrival time</param>
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
            // Already visited this planet in the current path, kill this branch
            return;
        }

        visited.Add(current);
        
        if (_flightsFromOrigin.TryGetValue(current, out var flights))
        {
            foreach (var flight in flights)
            {
                if (lastArrivalTime == null || flight.DepartureTime > lastArrivalTime.Value.AddMinutes(15)) // Minimum layover 15 minutes
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