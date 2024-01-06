using System.ComponentModel.DataAnnotations;
using CosmosOdysseyAPI.Models;

namespace CosmosOdysseyAPI.Models;

public class ReservationFlight
{
    [Key] public int ReservationFlightId { get; set; }  // ReservationFlight identifier
    public string ReservationID { get; set; }           // TravelReservation identifier
    public string FlightId { get; set; }                // Flight identifier
}