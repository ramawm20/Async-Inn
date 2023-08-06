using AsyncInn.Data;
using AsyncInn.Interfaces;
using AsyncInn.Models;
using AsyncInn.Models.DTOs;
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

        /// <summary>
        /// Deletes a hotel by its ID.
        /// </summary>
        /// <param name="id">The ID of the hotel to delete.</param>

        public async Task DeleteHotel(int id)
        {
           
            var deletedHotel= await _context.Hotels.Where(e => e.Id==id).FirstOrDefaultAsync();
            if (deletedHotel != null)
            {
               
                _context.Hotels.Remove(deletedHotel);
                await _context.SaveChangesAsync();
               
            }
            
        }
        /// <summary>
        /// Retrieves a specific hotel by its ID.
        /// </summary>
        /// <param name="id">The ID of the hotel to retrieve.</param>
        /// <returns>The HotelDTO representing the hotel with the given ID with it's details.</returns>

        public async Task<HotelDTO> GetHotel(int id)
        {
            var hotel = await _context.Hotels
        .Where(h => h.Id == id)
        .Include(h => h.hotelRooms)
            .ThenInclude(hr => hr.room)
            .ThenInclude(hrr => hrr.Amenities)

        .Select(h => new HotelDTO
        {
            ID = h.Id,
            Name = h.Name,
            StreetAddress = h.StreetAdress,
            City = h.City,
            State = h.State,
            Phone = h.Phone,
            hotelRooms = h.hotelRooms.Select(hr => new HotelRoomDTO
            {
                HotelID = hr.HotelId,
                RoomID = hr.RoomId,
                RoomNumber = hr.RoomNumber,
                Rate = hr.Rate,
                PetFriendly = hr.isPetFriendly,
                Room = new RoomDTO
                {
                    ID = hr.room.Id,
                    Name = hr.room.Name,
                    Layout = hr.room.Layout,
                    Amenities = hr.room.Amenities.Select(a => new AmenityDTO
                    {
                        ID = a.Id,
                        Name = a.Name
                    }).ToList()
                }
            }).ToList()
        })

                 .FirstOrDefaultAsync();

            return hotel;
        }

        /// <summary>
        /// Retrieves a list of all hotels.
        /// </summary>
        /// <returns>An IEnumerable of HotelDTO representing all hotels.</returns>
        
        public async Task<IEnumerable<HotelDTO>> GetHotels()
        {
            var hotels = await _context.Hotels
             .Select(h => new HotelDTO
             {
                 ID = h.Id,
                 Name = h.Name,
                 StreetAddress = h.StreetAdress,
                 City = h.City,
                 State = h.State,
                 Phone = h.Phone
             })

                .ToListAsync();
            return hotels;
        }

        /// <summary>
        /// Checks if a hotel with the given ID exists.
        /// </summary>
        /// <param name="id">The ID of the hotel to check.</param>
        /// <returns>True if a hotel with the given ID exists; otherwise, false.</returns>

        public bool HotelExists(int id)
        {
            return _context.Hotels.Any(h => h.Id == id);
        }

        /// <summary>
        /// Creates a new hotel.
        /// </summary>
        /// <param name="hotel">The information of the new hotel to create.</param>
        /// <returns>The newly created HotelDTO representing the newly created hotel.</returns>

        public async Task<HotelDTO> PostHotel(HotelDTO hotel)
        {
          // _context.Hotels.Add(hotel);
           await _context.SaveChangesAsync();
           return hotel;
        }

        /// <summary>
        /// Updates an existing hotel.
        /// </summary>
        /// <param name="id">The ID of the hotel to update.</param>
        /// <param name="hotel">The updated information for the hotel.</param>
        /// <returns>The updated HotelDTO representing the hotel after the update.</returns>
        public async Task<HotelDTO> PutHotel(int id, HotelDTO hotel)
        {
            var hotelupdata = await GetHotel(id);
            if (hotelupdata != null)
            {
                hotelupdata.State = hotel.State;
                hotelupdata.StreetAddress = hotel.StreetAddress;
                hotelupdata.City = hotel.City;
                hotelupdata.Phone = hotel.Phone;
                await _context.SaveChangesAsync();
            }
            return hotelupdata;

        }
    }
}
