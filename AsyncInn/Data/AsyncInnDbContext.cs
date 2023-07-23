﻿using AsyncInn.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Numerics;

namespace AsyncInn.Data
{
    public class AsyncInnDbContext : DbContext
    {
        public AsyncInnDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Hotel> Hotels { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Amenities> Amenities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hotel>().HasData(
                 new Hotel()
                 {
                     Id = 1,
                     Name = "Example Hotel 1",
                     StreetAdress = "123 Main Street",
                     City = "New York City",
                     State = "New York",
                     Country = "United States",
                     Phone = "+1 (555) 123-4567"
                 },

                new Hotel()
                {

                    Id = 2,
                    Name = "Sample Resort 2",
                    StreetAdress = "456 Park Avenue",
                    City = "Los Angeles",
                    State = "California",
                    Country = "United States",
                    Phone = "+1 (555) 987-6543"
                },
                new Hotel()
                {
                    Id = 3,
                    Name = "Test Inn 3",
                    StreetAdress = "789 Ocean Boulevard",
                    City = "Miami",
                    State = "Florida",
                    Country = "United States",
                    Phone = "+1 (555) 222-3333"
                }
                );

            modelBuilder.Entity<Room>().HasData
                (
                    new Room()
                    {
                        Id = 1,
                        Name = "Standard Room",
                        Layout = 1
                    },
                    new Room()
                    {
                        Id = 2,
                        Name = "Deluxe Suite",
                        Layout = 2
                    },
                    new Room()
                    {
                        Id = 3,
                        Name = "Executive Suite",
                        Layout = 3
                    }
                );
            modelBuilder.Entity<Amenities>().HasData
                (
                    new Amenities()
                    {
                        Id = 1,
                        Name = "Free Wi-Fi"
                    },
                    new Amenities()
                    {
                        Id = 2,
                        Name = "Swimming Pool"
                    },
                    new Amenities()
                    {
                        Id = 3,
                        Name="Coffee Maker"
                    }
       
                    
                );







        }
    }
}