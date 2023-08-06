using AsyncInn.Models;
using AsyncInn.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AsyncInn.Interfaces
{
    /// <summary>
    /// This interface defines the contract for managing hotels.
    /// It provides methods to retrieve, update, create, and delete hotels.
    /// </summary>

    public interface IHotels
    {
        public  Task<IEnumerable<HotelDTO>> GetHotels();

       
        public  Task<HotelDTO> GetHotel(int id);


        public Task <HotelDTO> PutHotel(int id, HotelDTO hotel);

        public Task<HotelDTO> PostHotel(HotelDTO hotel);

        public  Task DeleteHotel(int id);

        public bool HotelExists(int id);
    }
}
