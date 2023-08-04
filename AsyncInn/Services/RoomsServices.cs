using AsyncInn.Data;
using AsyncInn.Interfaces;
using AsyncInn.Models;
using AsyncInn.Models.DTOs;
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
               // _context.Rooms.Remove(deletedRoom);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<RoomDTO> GetRoom(int id)
        {
            var room = await _context.Rooms.Where(r => r.Id == id)
                .Select(r => new RoomDTO()
                {
                    ID=r.Id,
                    Name=r.Name,
                    Layout=r.Layout,
                    Amenities=r.Amenities.Select(a => new AmenityDTO()
                    {
                        ID=a.Id,
                        Name=a.Name
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            

            return room;
        }

        public async Task<IEnumerable<RoomDTO>> GetRooms()
        {
            var Rooms = await _context.Rooms
                .Include(r => r.Amenities)
                .Select(r => new RoomDTO()
                {
                    ID = r.Id,
                    Name = r.Name,
                    Layout = r.Layout,
                    Amenities = r.Amenities.Select(a => new AmenityDTO()
                    {
                        ID = a.Id,
                        Name = a.Name
                    }).ToList()
                })
                .ToListAsync();
            return Rooms;
        }

        public async Task<RoomDTO> PostRoom(RoomDTO room)
        {

            var roomToAdd = new Room();
            roomToAdd.Name=room.Name;
            roomToAdd.Layout = room.Layout;
            roomToAdd.Amenities = room.Amenities.Select(a => new Amenities()
            {
                Id = a.ID,
                Name = a.Name
            }).ToList();

                await _context.Rooms.AddAsync(roomToAdd);
                await _context.SaveChangesAsync();
                return room;
            
        }

        public async Task PutRoom(int id, RoomDTO room)
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
