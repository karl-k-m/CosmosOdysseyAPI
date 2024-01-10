using CosmosOdysseyAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CosmosOdysseyAPI.Data;

public class ApiContext : DbContext
{
    public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }
    public DbSet<Flight> Flights { get; set; }
    public DbSet<TravelReservation> TravelReservations { get; set; }
    public DbSet<ReservationFlight> ReservationFlights { get; set; }
}