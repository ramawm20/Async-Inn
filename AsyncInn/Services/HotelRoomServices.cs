using AsyncInn.Data;
using AsyncInn.Interfaces;
using AsyncInn.Models;
using Microsoft.EntityFrameworkCore;

namespace AsyncInn.Services
{
    public class HotelRoomServices : IHotelRoom
    {
        private readonly AsyncInnDbContext _context;

        public HotelRoomServices(AsyncInnDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Deletes a room from a specific hotel.
        /// </summary>
        /// <param name="hotelId">The ID of the hotel where the room belongs to.</param>
        /// <param name="roomNumber">The room number of the room to delete.</param>

        public async Task DeleteRoom(int HotelId, int roomNumber)
        {
            var room = await GetRoom(HotelId, roomNumber);
            var rooms = await _context.hotelRoom
           .Where(hr => hr.HotelId == HotelId)
           .Select(hr => hr.room)
           .ToListAsync();
            rooms.Remove(room);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Retrieves all rooms available in a specific hotel.
        /// </summary>
        /// <param name="hotelId">The ID of the hotel for which to retrieve rooms.</param>
        /// <returns>An IEnumerable of Room representing all rooms in the specified hotel.</returns>


        public async Task<IEnumerable<Room>> getAllRooms(int hotelId)
        {
            var rooms = await _context.hotelRoom.Where(hr => hr.HotelId == hotelId).Select(e => e.room).ToListAsync();
            return rooms;
        }

        /// <summary>
        /// Retrieves a specific room in the specified hotel by its room number.
        /// </summary>
        /// <param name="roomId">The ID of the room to retrieve.</param>
        /// <param name="hotelId">The ID of the hotel where the room is located.</param>
        /// <returns>The Room representing the room with the given ID in the specified hotel.</returns>

        public async Task<Room> GetRoom(int roomId, int hotelId)
        {
            var room = await _context.hotelRoom
      .Where(hr => hr.HotelId == hotelId && hr.RoomNumber == roomId)
      .Select(hr => hr.room)
      .FirstOrDefaultAsync();

            return room;
        }

        /// <summary>
        /// Creates a new room in the specified hotel.
        /// </summary>
        /// <param name="HotelId">The ID of the hotel where the new room will be created.</param>
        /// <param name="hotelRoom">The information of the new room to create.</param>
        public async Task postRoom(int HotelId, HotelRoom hotelRoom)
        {
            var hotel = await _context.Hotels.Where(h => h.Id == HotelId).Include(e => e.hotelRooms).FirstOrDefaultAsync();
            var hotelToAdd = await _context.Hotels
               .Where(h => h.Id == HotelId)
               .FirstOrDefaultAsync();
            var roomToADD = await _context.Rooms
                .Where(h => h.Id == hotelRoom.RoomId)
                .FirstOrDefaultAsync();
            hotelRoom.hotel = hotelToAdd;
            hotelRoom.room = roomToADD;
            hotel.hotelRooms.Add(hotelRoom);
            await _context.SaveChangesAsync();

        }

        /// <summary>
        /// Updates an existing room in a specific hotel.
        /// </summary>
        /// <param name="hotelId">The ID of the hotel where the room belongs to.</param>
        /// <param name="roomNumber">The room number of the room to update.</param>
        /// <param name="room">The updated information for the room.</param>
        public async Task putRoom(int roomId, int roomNumber, Room room)
        {
            var roomToUpdata = await GetRoom(roomId, roomNumber);
            roomToUpdata.Name = room.Name;
            roomToUpdata.Layout = room.Layout;
            await _context.SaveChangesAsync();
        }
    }
}
