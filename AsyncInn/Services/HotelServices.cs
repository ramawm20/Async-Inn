using AsyncInn.Data;
using AsyncInn.Interfaces;
using AsyncInn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AsyncInn.Services
{
    public class HotelServices : IHotels
    {
        private readonly AsyncInnDbContext _context;

        public HotelServices(AsyncInnDbContext context)
        {
            _context = context;
        }
        public async Task DeleteHotel(int id)
        {
           
            var deletedHotel = await GetHotel(id);
            if (deletedHotel != null)
            {
                _context.Hotels.Remove(deletedHotel);
                await _context.SaveChangesAsync();
               
            }
            
        }

        public async Task<Hotel> GetHotel(int id)
        {
            var hotel= await _context.Hotels.Where(h=>h.Id==id).FirstOrDefaultAsync();

            return hotel;
        }

        public async Task<IEnumerable<Hotel>> GetHotels()
        {
            var Hotels= await _context.Hotels.ToListAsync();

            return Hotels;
        }

        public bool HotelExists(int id)
        {
            var Hotel=GetHotel(id);
            if (Hotel!=null)
            {
                return true;
            }
            return false;
        }

        public async Task<Hotel> PostHotel(Hotel hotel)
        {
           _context.Hotels.Add(hotel);
           await _context.SaveChangesAsync();
           return hotel;
        }

        public async Task PutHotel(int id, Hotel hotel)
        {
            var updatedHotel = await GetHotel(id);

            if (updatedHotel != null) 
            {
                updatedHotel.State = hotel.State;
                updatedHotel.StreetAdress = hotel.StreetAdress;
                updatedHotel.City= hotel.City;
                updatedHotel.Phone= hotel.Phone;
                await _context.SaveChangesAsync();
                
            }
            
        }
    }
}
