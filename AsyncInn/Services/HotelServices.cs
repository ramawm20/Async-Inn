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
        public async Task DeleteHotel(int id)
        {
           
            var deletedHotel = await GetHotel(id);
            if (deletedHotel != null)
            {
               // _context.Hotels.Remove(deletedHotel);
                await _context.SaveChangesAsync();
               
            }
            
        }

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

        public bool HotelExists(int id)
        {
            var Hotel=GetHotel(id);
            if (Hotel!=null)
            {
                return true;
            }
            return false;
        }

        public async Task<HotelDTO> PostHotel(HotelDTO hotel)
        {
          // _context.Hotels.Add(hotel);
           await _context.SaveChangesAsync();
           return hotel;
        }

        public async Task PutHotel(int id, HotelDTO hotel)
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

        }
    }
}
