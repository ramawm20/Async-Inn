using AsyncInn.Models;
using AsyncInn.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AsyncInn.Interfaces
{
    public interface IHotels
    {
        public  Task<IEnumerable<HotelDTO>> GetHotels();

        public  Task<HotelDTO> GetHotel(int id);

        public Task  PutHotel(int id, HotelDTO hotel);

        public Task<HotelDTO> PostHotel(HotelDTO hotel);

        public  Task  DeleteHotel(int id);

        public bool HotelExists(int id);
    }
}
