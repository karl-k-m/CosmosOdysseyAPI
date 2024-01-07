using System.ComponentModel.DataAnnotations;

namespace CosmosOdysseyAPI.Models;

/// <summary>
/// A specific flight between two locations, with a specific departure and arrival time.
/// </summary>
public class Flight
{
    [Key] public string FlightID { get; set; }  // Flight identifier

    public string RouteID { get; set; }         // Route identifier
    
    public string CompanyName { get; set; }     // Company name
    
    public string Origin { get; set; }          // Origin location
    public string Destination { get; set; }     // Destination location
    
    public long Distance { get; set; }          // Distance between origin and destination (km)
    public decimal Price { get; set; }             // Price of the flight
    
    public DateTime DepartureTime { get; set; } // Departure time
    public DateTime ArrivalTime { get; set; }   // Arrival time
}