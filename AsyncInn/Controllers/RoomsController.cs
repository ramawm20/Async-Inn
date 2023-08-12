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
using Microsoft.AspNetCore.Authorization;

namespace AsyncInn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoomsController : ControllerBase
    {
        private readonly IRooms _context;

        public RoomsController(IRooms context)
        {
            _context = context;
        }

        // GET: api/Rooms
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<RoomDTO>>> GetRooms()
        {
           var rooms = await _context.GetRooms();
            return Ok(rooms);
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        [AllowAnonymous]

        public async Task<ActionResult<RoomDTO>> GetRoom(int id)
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
        [Authorize (Policy = "update")]
        public async Task<IActionResult> PutRoom(int id, RoomDTO room)
        {
          return Ok(_context.PutRoom(id, room));    
        }

        // POST: api/Rooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = "create")]

        public async Task<ActionResult<RoomDTO>> PostRoom(RoomDTO room)
        {
            return Ok(_context.PostRoom(room));
        }
        [HttpPost("{roomId}/Amenity/{amenityId}")]
        [Authorize(Policy = "create")]

        public async Task<ActionResult> AddAmenityToRoom(int roomId, int amenityId)
        {
            await _context.AddAmenityToRoom(roomId, amenityId);
            return Ok();
        }
        [HttpDelete("{roomId}/Amenity/{amenityId}")]
        [Authorize(Policy = "delete")]

        public async Task<ActionResult> RemoveAmentityFromRoom(int roomId, int amenityId)
        {
            await _context.RemoveAmentityFromRoom(roomId, amenityId);
            return NoContent();
        }
        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "delete")]

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
