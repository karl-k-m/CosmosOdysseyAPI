using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CosmosOdysseyAPI.Models;
using CosmosOdysseyAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace CosmosOdysseyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationFlightController : ControllerBase
    {
        private readonly ApiContext _context;

        public ReservationFlightController(ApiContext context)
        {
            _context = context;
        }

        // GET all flights which are part of a specific travel reservation
        [HttpGet]
        public JsonResult GetReservationFlights(string reservationId)
        {
            return new JsonResult(_context.ReservationFlights.Where(rf => rf.ReservationID == reservationId));
        }

        // POST a new reservation flight to the database
        [HttpPost]
        public JsonResult PostReservationFlight(ReservationFlight reservationFlight)
        {
            _context.ReservationFlights.Add(reservationFlight);
            _context.SaveChanges();
            return new JsonResult(reservationFlight);
        }  
    }
}