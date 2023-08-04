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
using AsyncInn.Models.DTOs;

namespace AsyncInn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotels _context;
        private readonly IHotelRoom _hotelRoom;


        public HotelsController(IHotels context, IHotelRoom hotelRoom)
        {
            _context = context;
            _hotelRoom = hotelRoom;
        }
        [HttpGet("{hotelId}/Rooms")]


        public async Task<ActionResult<IEnumerable<Room>>> GetHotelRooms(int hotelId)
        {
            var rooms=await _hotelRoom.getAllRooms(hotelId);

            return Ok(rooms);
        }
        [HttpGet("{hotelId}/Rooms/{roomNumber}")]
        public async Task<ActionResult<Room>> GetHotelRoom(int hotelId, int roomNumber)
        {
            var room = await _hotelRoom.GetRoom(hotelId, roomNumber);
            return Ok(room);
        }

        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelDTO>>> GetHotels()
        {
            var hotels = await _context.GetHotels();
            return Ok(hotels);
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelDTO>> GetHotel(int id)
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
        public async Task<IActionResult> PutHotel(int id, HotelDTO hotel)
        {
            return Ok(_context.PutHotel(id, hotel));
        }

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HotelDTO>> PostHotel(HotelDTO hotel)
        {
            await _context.PostHotel(hotel);
            return Ok(hotel);
        
        }
        [HttpPost("{hotelId}/rooms")]
        public async Task<ActionResult> PostRoom(int id, HotelRoom hotelRoom)
        {
            await _hotelRoom.postRoom(id, hotelRoom);
            return Ok();
        }
        [HttpDelete("{id}")]
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
