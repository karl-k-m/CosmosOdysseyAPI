using CosmosOdysseyAPI.Models;

namespace CosmosOdysseyAPI.BackgroundProcesses;

/// <summary>
/// This class is used to find all possible paths between two locations.
/// </summary>
public class PathFinder
{
    public static List<List<Flight>> findAllPaths(string startLoc, string endLoc, List<Flight> flights)
    {
        return null;
    }

    private static List<List<Flight>> findPaths(string startLoc, string endLoc, List<Flight> flights, List<List<Flight>> paths,
        List<Flight> currentPath, List<string> visited)
    {
        if (currentPath.Last().Destination == endLoc)
        {
            paths.Add(currentPath);
        }

        if (currentPath.Last().Destination == startLoc)
        {
            return paths;
        }
        
        foreach (var flight in flights)
        {
            if (flight.Origin == currentPath.Last().Destination && !visited.Contains(flight.FlightID))
            {
                visited.Add(flight.FlightID);
                currentPath.Add(flight);
                findPaths(startLoc, endLoc, flights, paths, currentPath, visited);
                currentPath.Remove(flight);
            }
        }

        return null;
    }
}