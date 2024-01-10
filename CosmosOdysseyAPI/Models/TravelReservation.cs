using System.ComponentModel.DataAnnotations;

namespace CosmosOdysseyAPI.Models;

/// <summary>
/// A travel reservation made by a client.
/// </summary>
public class TravelReservation
{
    [Key] public string ReservationID { get; set; } // TravelReservation identifier (Human readable CCC-NNNN)
    public string PassengerFirstName { get; set; }  // Passenger first name
    public string PassengerLastName { get; set; }   // Passenger last name
    public long Distance { get; set; }              // Distance between origin and destination (km)
    public long Duration { get; set; }              // Duration of the trip (hours)
    public decimal Price { get; set; }              // Price of the trip
    public int ValidityCounter { get; set; }        // Internal counter for removing old TravelReservations
}