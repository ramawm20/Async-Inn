using AsyncInn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AsyncInn.Interfaces
{
    public interface IAmenities
    {
        public Task<IEnumerable<Amenities>> GetAmenities();

        public Task<Amenities> GetAmenities(int id);

        public  Task PutAmenities(int id, Amenities amenities);
    
        public  Task<Amenities> PostAmenities(Amenities amenities);

        public  Task DeleteAmenities(int id);

        public bool AmenitiesExists(int id);

    }
}
