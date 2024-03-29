using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CosmosOdysseyAPI.Models;
using CosmosOdysseyAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace CosmosOdysseyAPI.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightFinderController : ControllerBase
    {
        private readonly ApiContext _context;
        
        public FlightFinderController(ApiContext context)
        {
            _context = context;
        }
        
        // GET all paths between two locations
        [HttpGet]
        public JsonResult GetPaths(string startLoc, string endLoc)
        {
            // Only consider flights which are part of the current pricelist
            var flights = _context.Flights.Where(f => f.ValidityCounter == 0).ToListAsync().Result;
            
            FlightFinder finder = new FlightFinder(flights);
            var paths = finder.FindAllPaths(startLoc, endLoc);
            
            return new JsonResult(paths);
        }
    }
}