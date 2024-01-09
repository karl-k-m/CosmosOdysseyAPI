using System.ComponentModel.DataAnnotations;
using CosmosOdysseyAPI.Models;

namespace CosmosOdysseyAPI.Models;

/// <summary>
/// A flight belonging to a travel reservation.
/// </summary>
public class ReservationFlight
{
    [Key] public int ReservationFlightId { get; set; }  // ReservationFlight identifier
    public string ReservationID { get; set; }           // TravelReservation identifier
    public string FlightID { get; set; }                // Flight identifier
    public int ValidityCounter { get; set; }            // Internal counter for removing old ReservationFlights
}