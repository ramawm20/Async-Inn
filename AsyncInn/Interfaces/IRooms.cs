using AsyncInn.Models;
using AsyncInn.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AsyncInn.Interfaces
{
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
