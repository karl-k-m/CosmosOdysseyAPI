using System.Text.Json;
using CosmosOdysseyAPI.Models;
using CosmosOdysseyAPI.Data;

namespace CosmosOdysseyAPI.BackgroundProcesses;

/// <summary>
/// Background service for updating flights from the flights API.
/// </summary>
public class UpdateFlightsBackgroundService : BackgroundService
{
    private class Location
    {
        public string id { get; set; }
        public string name { get; set; }
    }
    
    private class RouteInfo
    {
        public string id { get; set; }
        public Location from { get; set; }
        public Location to { get; set; }
        public long distance { get; set; }
    }

    private class Company
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    private class ProvidedFlight
    {
        public string id { get; set; }
        public Company company { get; set; }
        public decimal price { get; set; }
        public DateTime flightStart { get; set; }
        public DateTime flightEnd { get; set; }
    }
    
    private class Route
    {
        public string id { get; set; }
        public RouteInfo routeInfo { get; set; }
        public List<ProvidedFlight> providers { get; set; }
    }
    
    private class Json
    {
        public string id { get; set; }
        public DateTime validUntil { get; set; }
        public List<Route> legs { get; set; }
    }
    
    private readonly ILogger<UpdateFlightsBackgroundService> _logger;
    private readonly IHttpClientFactory _clientFactory;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public UpdateFlightsBackgroundService(ILogger<UpdateFlightsBackgroundService> logger, IHttpClientFactory clientFactory, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _clientFactory = clientFactory;
        _serviceScopeFactory = serviceScopeFactory;
    }

    private string _lastProcessedId = null;                         // ID of the last processed JSON
    private DateTime _lastProcessedValidUntil = DateTime.MinValue;  // Valid until of the last processed JSON
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<ApiContext>();
                
                // Only fetch new data if previous data is no longer valid
                if (DateTime.UtcNow > _lastProcessedValidUntil)
                { 
                    var request = new HttpRequestMessage(HttpMethod.Get, "https://cosmos-odyssey.azurewebsites.net/api/v1.0/TravelPrices");
                    request.Headers.Add("Accept", "application/json");
                    request.Headers.Add("User-Agent", "CosmosOdysseyAPI");

                    var client = _clientFactory.CreateClient();

                    var response = await client.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var deserializedJson = JsonSerializer.Deserialize<Json>(json);

                        // Only process new data if it is different from the previous data
                        if (deserializedJson.id != _lastProcessedId)
                        {
                            // Update last processed ID and valid until
                            _lastProcessedId = deserializedJson.id;
                            _lastProcessedValidUntil = deserializedJson.validUntil;
                            
                            // Remove all flights from the database
                            _context.Flights.RemoveRange(_context.Flights);
                            await _context.SaveChangesAsync();
                            
                            // Process json and add flights to the database
                            foreach (var route in deserializedJson.legs)
                            {
                                foreach (var flight in route.providers)
                                {
                                    var newFlight = new Flight
                                    {
                                        FlightID = flight.id,
                                        RouteID = route.id,
                                        CompanyName = flight.company.name,
                                        Origin = route.routeInfo.from.name,
                                        Destination = route.routeInfo.to.name,
                                        Distance = route.routeInfo.distance,
                                        Price = flight.price,
                                        DepartureTime = flight.flightStart,
                                        ArrivalTime = flight.flightEnd
                                    };
                                    
                                    var existingFlight = await _context.Flights.FindAsync(newFlight.FlightID);
                                    _context.Flights.Add(newFlight);
                                }
                            }
                            await _context.SaveChangesAsync();
                            _logger.LogInformation("Successfully fetched flights from flights API, valid until {validUntil}", deserializedJson.validUntil.ToString("yyyy-MM-dd HH:mm:ss"));
                        }
                    }
                    
                    else
                    {
                        _logger.LogError("Error fetching flights from flights API, status code {statusCode}", response.StatusCode);
                    }
                    
                    // If previous data is no longer valid, try to fetch new data every 5 minutes
                    await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
                }
            }
        }
    }
}