using AsyncInn.Models;

namespace AsyncInn.Interfaces
{
    public interface IHotelRoom
    {
        Task<IEnumerable<Room>> getAllRooms(int hotelId);

        Task postRoom(int HotelId,HotelRoom hotelRoom);

        Task<Room> GetRoom(int roomId, int hotelId);

        Task putRoom(int HotelId, int roomNumber, Room room);

        Task DeleteRoom(int HotelId, int roomNumber);
    }
}
