using AsyncInn.Models;
using Microsoft.AspNetCore.Mvc;

namespace AsyncInn.Interfaces
{
    public interface IHotels
    {
        public  Task<IEnumerable<Hotel>> GetHotels();

        public  Task<Hotel> GetHotel(int id);

        public Task  PutHotel(int id, Hotel hotel);

        public Task<Hotel> PostHotel(Hotel hotel);

        public  Task  DeleteHotel(int id);

        public bool HotelExists(int id);
    }
}
