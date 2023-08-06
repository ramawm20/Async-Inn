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

        /// <summary>
        /// Deletes a room by its ID.
        /// </summary>
        /// <param name="id">The ID of the room to delete.</param>

        public async Task DeleteRoom(int id)
        {
            var deletedRoom = await  _context.Rooms.Where(r => r.Id == id).FirstOrDefaultAsync();
            if (deletedRoom != null)
            {
                _context.Rooms.Remove(deletedRoom);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Retrieves a specific room by its ID.
        /// </summary>
        /// <param name="id">The ID of the room to retrieve.</param>
        /// <returns>The RoomDTO representing the room with the given ID.</returns>

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

        /// <summary>
        /// Retrieves a list of all rooms in the hotel.
        /// </summary>
        /// <returns>An IEnumerable of RoomDTO representing all rooms.</returns>

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

        /// <summary>
        /// Creates a new room.
        /// </summary>
        /// <param name="room">The information of the new room to create.</param>
        /// <returns>The newly created RoomDTO representing the newly created room.</returns>

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

        /// <summary>
        /// Updates an existing room.
        /// </summary>
        /// <param name="id">The ID of the room to update.</param>
        /// <param name="room">The updated information for the room.</param>

        public async Task PutRoom(int id, RoomDTO room)
        {
            var updatedRoom = await _context.Rooms.Where(r => r.Id == id).FirstOrDefaultAsync();
            if (updatedRoom != null)
            {
                updatedRoom.Name = room.Name;
                updatedRoom.Layout = room.Layout;
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Checks if a room with the given ID exists.
        /// </summary>
        /// <param name="id">The ID of the room to check.</param>
        /// <returns>True if a room with the given ID exists; otherwise, false.</returns>
        public bool RoomExists(int id)
        {
            var Room =  GetRoom(id);
            if(Room != null)
            {
                return true;

            }
            return false;
        }

        /// <summary>
        /// Adds an amenity to a room.
        /// </summary>
        /// <param name="roomId">The ID of the room to add the amenity to.</param>
        /// <param name="amenityId">The ID of the amenity to add to the room.</param>

        public async Task AddAmenityToRoom(int roomId, int amenityId)
        {

            var room = await _context.Rooms.Where(r => r.Id == roomId)
                .Include(a => a.Amenities)
                .FirstOrDefaultAsync();
            var amenity = await _context.Amenities.Where(a => a.Id == amenityId).FirstOrDefaultAsync();
            room.Amenities.Add(amenity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Removes an amenity from a room.
        /// </summary>
        /// <param name="roomId">The ID of the room to remove the amenity from.</param>
        /// <param name="amenityId">The ID of the amenity to remove from the room.</param>

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
