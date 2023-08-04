using AsyncInn.Data;
using AsyncInn.Interfaces;
using AsyncInn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AsyncInn.Services
{
    public class AmenitiesServices : IAmenities
    {
            private readonly AsyncInnDbContext _context;

            public AmenitiesServices(AsyncInnDbContext context)
            {
                _context = context;
            }

        public bool AmenitiesExists(int id)
        {
            var Amenities = GetAmenities(id);
            if(Amenities == null)
            {
                return false;
            }
            return true;
        }

        public async Task DeleteAmenities(int id)
        {
            var deletedAmenities = await GetAmenities(id);
                _context.Amenities.Remove(deletedAmenities);
                await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Amenities>> GetAmenities()
        {
            var amenities= await _context.Amenities.ToListAsync();
            return amenities;
        }

        public async Task<Amenities> GetAmenities(int id)
        {
            var amenties = await _context.Amenities.Where(a => a.Id == id).FirstOrDefaultAsync();

            return amenties;
        }

        public async Task<Amenities> PostAmenities(Amenities amenities)
        {
            _context.Amenities.Add(amenities);
            await _context.SaveChangesAsync();
            return amenities;
        }

        public async Task PutAmenities(int id, Amenities amenities)
        {
            var updated= await GetAmenities(id);   

            if (updated != null)
            {
                updated.Name = amenities.Name;
                await _context.SaveChangesAsync();
            }
        }
    }
}
