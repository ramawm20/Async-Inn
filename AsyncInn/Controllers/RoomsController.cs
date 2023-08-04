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
    public class RoomsController : ControllerBase
    {
        private readonly IRooms _context;

        public RoomsController(IRooms context)
        {
            _context = context;
        }

        // GET: api/Rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetRooms()
        {
           var rooms = await _context.GetRooms();
            return Ok(rooms);
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoom(int id)
        {
            var room= await _context.GetRoom(id);
            if (room == null)
            {
                return NotFound();
            }
            return Ok(room);
        }

        // PUT: api/Rooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(int id, Room room)
        {
          return Ok(_context.PutRoom(id, room));    
        }

        // POST: api/Rooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Room>> PostRoom(Room room)
        {
            return Ok(_context.PostRoom(room));
        }
        [HttpPost("{roomId}/Amenity/{amenityId}")]
        public async Task<ActionResult> AddAmenityToRoom(int roomId, int amenityId)
        {
            await _context.AddAmenityToRoom(roomId, amenityId);
            return Ok();
        }
        [HttpDelete("{roomId}/Amenity/{amenityId}")]
        public async Task<ActionResult> RemoveAmentityFromRoom(int roomId, int amenityId)
        {
            await _context.RemoveAmentityFromRoom(roomId, amenityId);
            return NoContent();
        }
        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
           return Ok(_context.DeleteRoom(id));
        }

        private bool RoomExists(int id)
        {
            return _context.RoomExists(id);
        }
    }
}
