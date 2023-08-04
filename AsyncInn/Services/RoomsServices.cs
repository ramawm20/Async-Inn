using AsyncInn.Data;
using AsyncInn.Interfaces;
using AsyncInn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AsyncInn.Services
{
    public class RoomsServices : IRooms
    {
        private readonly AsyncInnDbContext _context;

        public RoomsServices(AsyncInnDbContext context)
        {
            _context = context;
        }
        public async Task DeleteRoom(int id)
        {
            var deletedRoom = await  GetRoom(id);
            if (deletedRoom != null)
            {
                _context.Rooms.Remove(deletedRoom);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Room> GetRoom(int id)
        {
            var room = await _context.Rooms.Where(r => r.Id == id).FirstOrDefaultAsync();

            return room;
        }

        public async Task<IEnumerable<Room>> GetRooms()
        {
            var Rooms = await _context.Rooms
                .Include(r => r.Amenities)
                .ToListAsync();
            return Rooms;
        }

        public async Task<Room> PostRoom(Room room)
        {
            
                _context.Rooms.Add(room);
                await _context.SaveChangesAsync();
                return room;
            
        }

        public async Task PutRoom(int id, Room room)
        {
            var updatedRoom = await GetRoom(id);
            if (updatedRoom != null)
            {
                updatedRoom.Name = room.Name;
                updatedRoom.Layout = room.Layout;
            }
        }

        public bool RoomExists(int id)
        {
            var Room =  GetRoom(id);
            if(Room != null)
            {
                return true;

            }
            return false;
        }
        public async Task AddAmenityToRoom(int roomId, int amenityId)
        {

            var room = await _context.Rooms.Where(r => r.Id == roomId)
                .Include(a => a.Amenities)
                .FirstOrDefaultAsync();
            var amenity = await _context.Amenities.Where(a => a.Id == amenityId).FirstOrDefaultAsync();
            room.Amenities.Add(amenity);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveAmentityFromRoom(int roomId, int amenityId)
        {
            var room = await _context.Rooms.Where(r => r.Id == roomId)
                .Include(a => a.Amenities)
                .FirstOrDefaultAsync();

            var amenity = await _context.Amenities.Where(a => a.Id == amenityId).FirstOrDefaultAsync();

            room.Amenities.Remove(amenity);
            await _context.SaveChangesAsync();
        }
    }
}
