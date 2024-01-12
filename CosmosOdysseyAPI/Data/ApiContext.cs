using CosmosOdysseyAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CosmosOdysseyAPI.Data;

public class ApiContext : DbContext
{
    public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }
    public DbSet<Flight> Flights { get; set; }
    public DbSet<TravelReservation> TravelReservations { get; set; }
    public DbSet<ReservationFlight> ReservationFlights { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(@"Host=cosmosodyssey.postgres.database.azure.com;Username=postgresql;Password=dH4G^CG&%@t#CwkkaV!a;Database=cosmosodyssey");
}