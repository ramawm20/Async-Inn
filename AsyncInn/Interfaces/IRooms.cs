using AsyncInn.Models;
using AsyncInn.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AsyncInn.Interfaces
{
    /// <summary>
    /// This interface defines the contract for managing rooms in a hotel.
    /// It provides methods to retrieve, update, create, and delete rooms, as well as manage room amenities.
    /// </summary>
    
    public interface IRooms
    {
        public  Task<IEnumerable<RoomDTO>> GetRooms();

        public Task<RoomDTO> GetRoom(int id);

        public Task PutRoom(int id, RoomDTO room);

        public Task<RoomDTO> PostRoom(RoomDTO room);

        public  Task DeleteRoom(int id);

        public bool RoomExists(int id);

        Task AddAmenityToRoom(int roomId, int amenityId);

        Task RemoveAmentityFromRoom(int roomId, int amenityId);



    }
}
