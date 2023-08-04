using AsyncInn.Data;
using AsyncInn.Interfaces;
using AsyncInn.Models;
using AsyncInn.Models.DTOs;
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
               // _context.Amenities.Remove(deletedAmenities);
                await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AmenityDTO>> GetAmenities()
        {
            var amenities= await _context.Amenities
                .Select(a => new AmenityDTO
                {
                    ID= a.Id,
                    Name=a.Name
                }
                )
                .ToListAsync();
            return amenities;
        }

        public async Task<AmenityDTO> GetAmenities(int id)
        {
            var amenties = await _context.Amenities.Where(a => a.Id == id).FirstOrDefaultAsync();

            var ADTO = new AmenityDTO
            {
                ID = amenties.Id,
                Name = amenties.Name
            };

            return ADTO;
        }

        public async Task<AmenityDTO> PostAmenities(AmenityDTO amenities)
        {
            var AmenityToAdd = new Amenities
            {
                Name = amenities.Name
            };
            await _context.Amenities.AddAsync(AmenityToAdd);
            await _context.SaveChangesAsync();
            return amenities;
        }

        public async Task PutAmenities(int id, AmenityDTO amenities)
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
