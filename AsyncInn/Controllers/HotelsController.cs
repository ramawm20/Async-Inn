using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AsyncInn.Data;
using AsyncInn.Models;
using AsyncInn.Interfaces;

namespace AsyncInn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotels _context;

        public HotelsController(IHotels context)
        {
            _context = context;
        }

        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotels()
        {
            var hotels = await _context.GetHotels();
            return Ok(hotels);
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hotel>> GetHotel(int id)
        {
            var hotel =await _context.GetHotel(id);
            if (hotel == null)
            {
                return NotFound();
            }
            return Ok(hotel);
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, Hotel hotel)
        {
            return Ok(_context.PutHotel(id, hotel));
        }

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel(Hotel hotel)
        {
            await _context.PostHotel(hotel);
            return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);
        }
        public async Task<IActionResult> DeleteHotel(int id)
        {
            await _context.DeleteHotel(id);
            return NoContent();
        }
        private bool HotelExists(int id)
        {
            return _context.HotelExists(id);    
        }
    }
}
