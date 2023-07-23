using AsyncInn.Models;
using Microsoft.AspNetCore.Mvc;

namespace AsyncInn.Interfaces
{
    public interface IRooms
    {
        public  Task<IEnumerable<Room>> GetRooms();

        public Task<Room> GetRoom(int id);

        public Task PutRoom(int id, Room room);

        public Task<Room> PostRoom(Room room);

        public  Task DeleteRoom(int id);

        public bool RoomExists(int id);


        
    }
}
