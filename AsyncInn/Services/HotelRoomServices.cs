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

        public async Task<IEnumerable<Room>> getAllRooms(int hotelId)
        {
            var rooms = await _context.hotelRoom.Where(hr => hr.HotelId == hotelId).Select(e => e.room).ToListAsync();
            return rooms;
        }

        public async Task<Room> GetRoom(int roomId, int hotelId)
        {
            var room = await _context.hotelRoom
      .Where(hr => hr.HotelId == hotelId && hr.RoomNumber == roomId)
      .Select(hr => hr.room)
      .FirstOrDefaultAsync();

            return room;
        }

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

        public async Task putRoom(int roomId, int roomNumber, Room room)
        {
            var roomToUpdata = await GetRoom(roomId, roomNumber);
            roomToUpdata.Name = room.Name;
            roomToUpdata.Layout = room.Layout;
            await _context.SaveChangesAsync();
        }
    }
}
