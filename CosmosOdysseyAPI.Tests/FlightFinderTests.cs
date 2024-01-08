using CosmosOdysseyAPI.Models;

namespace CosmosOdysseyAPI.Tests;

public class FlightFinderTests
{
    public class FindAllPathsTests
    {
        [Test]
        public void FindAllPaths_WhenCalled_ReturnsAllPaths()
        {
            // Create a list of flights
            var flights = new List<Flight>
            {
                new Flight
                {
                    FlightID = "1",
                    CompanyName = "SpaceX",
                    Origin = "Earth",
                    Destination = "Moon",
                    Distance = 384400,
                    DepartureTime = new DateTime(2021, 1, 1, 0, 0, 0),
                    ArrivalTime = new DateTime(2021, 1, 1, 0, 0, 0)
                },
                new Flight
                {
                    FlightID = "2",
                    CompanyName = "SpaceX",
                    Origin = "Moon",
                    Destination = "Mars",
                    Distance = 54600000,
                    DepartureTime = new DateTime(2021, 1, 1, 0, 0, 0),
                    ArrivalTime = new DateTime(2021, 1, 1, 0, 0, 0)
                },
                new Flight
                {
                    FlightID = "3",
                    CompanyName = "SpaceX",
                    Origin = "Earth",
                    Destination = "Jupiter",
                    Distance = 588000000,
                    DepartureTime = new DateTime(2021, 1, 1, 0, 0, 0),
                    ArrivalTime = new DateTime(2021, 1, 1, 0, 0, 0)
                },
                new Flight
                {
                    FlightID = "4",
                    CompanyName = "SpaceX",
                    Origin = "Jupiter",
                    Destination = "Moon",
                    Distance = 628000000,
                    DepartureTime = new DateTime(2021, 2, 1, 0, 0, 0),
                    ArrivalTime = new DateTime(2021, 1, 1, 0, 0, 0)
                },
                new Flight
                {
                    FlightID = "5",
                    CompanyName = "SpaceX",
                    Origin = "Saturn",
                    Destination = "Earth",
                    Distance = 2720000000,
                    DepartureTime = new DateTime(2021, 3, 1, 0, 0, 0),
                    ArrivalTime = new DateTime(2021, 1, 1, 0, 0, 0)
                },
            };

            // Create a flight finder
            var flightFinder = new FlightFinder(flights);

            // Find all paths
            var paths = flightFinder.FindAllPaths("Earth", "Moon");

            // Assert 2 paths were found
            Assert.AreEqual(2, paths.Count);
        }
        
        // Test that the layover time is at least 15 minutes
        [Test]
        public void FindAllPaths_WhenCalled_ReturnsAllPathsThatMeetLayoverTimeRequirement()
        {
            // Create a list of flights
            var flights = new List<Flight>
            {
                new Flight
                {
                    FlightID = "1",
                    CompanyName = "SpaceX",
                    Origin = "Earth",
                    Destination = "Moon",
                    Distance = 384400,
                    DepartureTime = new DateTime(2021, 1, 1, 0, 0, 0),
                    ArrivalTime = new DateTime(2021, 1, 1, 0, 0, 0)
                },
                new Flight
                {
                    FlightID = "3",
                    CompanyName = "SpaceX",
                    Origin = "Earth",
                    Destination = "Jupiter",
                    Distance = 588000000,
                    DepartureTime = new DateTime(2021, 1, 1, 0, 0, 0),
                    ArrivalTime = new DateTime(2021, 1, 1, 0, 0, 0)
                },
                new Flight
                {
                    FlightID = "4",
                    CompanyName = "SpaceX",
                    Origin = "Jupiter",
                    Destination = "Moon",
                    Distance = 628000000,
                    DepartureTime = new DateTime(2021, 1, 1, 0, 16, 0), // After 15 minutes
                    ArrivalTime = new DateTime(2021, 1, 1, 0, 0, 0)
                },
                new Flight
                {
                    FlightID = "5",
                    CompanyName = "SpaceX",
                    Origin = "Jupiter",
                    Destination = "Moon",
                    Distance = 2720000000,
                    DepartureTime = new DateTime(2021, 1, 1, 0, 14, 0), // Before 15 minutes
                    ArrivalTime = new DateTime(2021, 1, 1, 0, 0, 0)
                }
            };

            var flightFinder = new FlightFinder(flights);
            var paths = flightFinder.FindAllPaths("Earth", "Moon");

            // Assert 2 paths were found (Earth -> Moon and Earth -> Jupiter -> Moon)
            Assert.AreEqual(2, paths.Count);
        }
    }
}