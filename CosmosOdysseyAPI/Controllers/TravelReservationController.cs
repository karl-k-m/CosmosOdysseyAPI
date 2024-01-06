using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CosmosOdysseyAPI.Models;
using CosmosOdysseyAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace CosmosOdysseyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelReservationController : ControllerBase
    {
        private readonly ApiContext _context;
        
        public TravelReservationController(ApiContext context)
        {
            _context = context;
        }

        // GET all travel reservations
        [HttpGet]
        public JsonResult GetTravelReservations()
        {
            return new JsonResult(_context.TravelReservations);
        }

        // POST a new travel reservation to the database
        [HttpPost]
        public JsonResult PostTravelReservation(TravelReservation travelReservation)
        {
            _context.TravelReservations.Add(travelReservation);
            _context.SaveChanges();
            return new JsonResult(travelReservation);
        }
    }
}