using AsyncInn.Models;
using AsyncInn.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AsyncInn.Interfaces
{
    /// <summary>
    /// This interface defines the contract for managing amenities in a hotel.
    /// It provides methods to retrieve, update, create, and delete amenities.
    /// </summary>
    
    public interface IAmenities
    {
        public Task<IEnumerable<AmenityDTO>> GetAmenities();

        public Task<AmenityDTO> GetAmenities(int id);

        public  Task PutAmenities(int id, AmenityDTO amenities);
    
        public  Task<AmenityDTO> PostAmenities(AmenityDTO amenities);

        public  Task DeleteAmenities(int id);

        public bool AmenitiesExists(int id);

    }
}
