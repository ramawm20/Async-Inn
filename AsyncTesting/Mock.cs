using AsyncInn.Data;
using AsyncInn.Models;
using AsyncInn.Models.DTOs;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncTesting
{
    public abstract class Mock : IDisposable
    {
        private readonly SqliteConnection _connection;

         protected readonly AsyncInnDbContext _db;

        public Mock()
        {
            //Like connection string but this for mock database => will go away after test
            _connection = new SqliteConnection("Filename=:memory:");
            //open sqlite connection
            _connection.Open();

            _db = new AsyncInnDbContext(
                new DbContextOptionsBuilder<AsyncInnDbContext>()
                .UseSqlite(_connection).Options
                );
            //Ensure creating the database schema
            _db.Database.EnsureCreated();
        }

        //When we want to autoclean our method  => used to handle the resource managment to clean by garbage collecter

        public void Dispose()
        {
            //? => Ensure there is no nullable values
            _db?.Dispose();
            _connection?.Dispose();
        }

        protected async Task<Hotel> CreateNewHotel()
        {
            var hotel = new Hotel()
            {

                Name = "City Center Hotel 6",
                StreetAdress = "222 Downtown Street",
                City = "Chicago",
                State = "Illinois",
                Country = "United States",
                Phone = "+1 (555) 999-0000"
            };

            _db.Hotels.Add(hotel);
            await _db.SaveChangesAsync();

            Assert.NotEqual(0, hotel.Id);
            return hotel;
        }
        protected async Task<Room> CreateRoom()
        {
            var room = new Room
            {
                Name = "Deluxe Suite",
                Layout = 2
            };
            _db.Rooms.Add(room);
            await _db.SaveChangesAsync();
            Assert.NotEqual(0, room.Id);
            return room;
        }
    }
}
