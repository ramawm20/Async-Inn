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

        /// <summary>
        /// Checks if an amenity with the given ID exists.
        /// </summary>
        /// <param name="id">The ID of the amenity to check.</param>
        /// <returns>True if an amenity with the given ID exists; otherwise, false.</returns>

        public bool AmenitiesExists(int id)
        {
            var Amenities = GetAmenities(id);
            if(Amenities == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Deletes an amenity by its ID.
        /// </summary>
        /// <param name="id">The ID of the amenity to delete.</param>

        public async Task DeleteAmenities(int id)
        {
            var deletedAmenities = await GetAmenities(id);
               // _context.Amenities.Remove(deletedAmenities);
                await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Retrieves a list of all amenities available in the hotel.
        /// </summary>
        /// <returns>An IEnumerable of AmenityDTO representing all amenities.</returns>

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

        /// <summary>
        /// Retrieves a specific amenity by its ID.
        /// </summary>
        /// <param name="id">The ID of the amenity to retrieve.</param>
        /// <returns>The AmenityDTO representing the amenity with the given ID.</returns>

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

        /// <summary>
        /// Creates a new amenity.
        /// </summary>
        /// <param name="amenities">The information of the new amenity to create.</param>
        /// <returns>The newly created AmenityDTO representing the newly created amenity.</returns>

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

        /// <summary>
        /// Updates an existing amenity.
        /// </summary>
        /// <param name="id">The ID of the amenity to update.</param>
        /// <param name="amenities">The updated information for the amenity.</param>

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
