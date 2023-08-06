using AsyncInn.Models;

namespace AsyncInn.Interfaces
{
    /// <summary>
    /// This interface defines the contract for managing hotel rooms.
    /// It provides methods to retrieve, create, update, and delete rooms within a hotel.
    /// </summary>
    
    public interface IHotelRoom
    {
        Task<IEnumerable<Room>> getAllRooms(int hotelId);

        Task postRoom(int HotelId,HotelRoom hotelRoom);

        Task<Room> GetRoom(int roomId, int hotelId);

        Task putRoom(int HotelId, int roomNumber, Room room);

        Task DeleteRoom(int HotelId, int roomNumber);
    }
}
