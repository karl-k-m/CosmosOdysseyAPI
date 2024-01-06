using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CosmosOdysseyAPI.Models;
using CosmosOdysseyAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace CosmosOdysseyAPI.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly ApiContext _context;
        
        public FlightController(ApiContext context)
        {
            _context = context;
        }
        
        // GET all flights
        [HttpGet]
        public JsonResult GetFlights()
        {
            return new JsonResult(_context.Flights);
        }
        
        // POST a new flight to the database
        [HttpPost]
        public JsonResult PostFlight(Flight flight)
        {
            _context.Flights.Add(flight);
            _context.SaveChanges();
            return new JsonResult(flight);
        }
    }
}