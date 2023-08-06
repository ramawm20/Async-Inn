using AsyncInn.Models;
using AsyncInn.Models.DTOs;
using AsyncInn.Services;

namespace AsyncTesting
{
    public class UnitTest1 : Mock
    {
        [Fact]
        public async Task CanDeleteHotel()
        {
            // Arrange
            var hotel = await CreateNewHotel();
            var services = new HotelServices(_db);

            // Act
            await services.DeleteHotel(hotel.Id);

            // Assert
            var deletedHotel = await services.GetHotel(hotel.Id);
            Assert.Null(deletedHotel);
        }
        [Fact]
        public async Task CanGetHotel()
        {
            // Arrange
            var hotel = await CreateNewHotel();
            var services = new HotelServices(_db);

            // Act
            var hotelDTO = await services.GetHotel(hotel.Id);

            // Assert
            Assert.NotNull(hotelDTO);
            Assert.Equal(hotel.Id, hotelDTO.ID);
        }
        [Fact]
        public async Task CanGetHotelsReturnList()
        {
            // Arrange
            var services = new HotelServices(_db);

            // Act
            var hotelsDTOs = await services.GetHotels();

            // Assert
            Assert.NotNull(hotelsDTOs);
            Assert.NotEmpty(hotelsDTOs);
        }
        [Fact]
        public void HotelExistsValidId()
        {
            // Arrange
            var hotel = CreateNewHotel().GetAwaiter().GetResult();
            var services = new HotelServices(_db);

            // Act
            var result = services.HotelExists(hotel.Id);

            // Assert
            Assert.True(result);
        }
        [Fact]
        public void HotelExistsInvalidId()
        {
            // Arrange
            var services = new HotelServices(_db);

            // Act
            var result = services.HotelExists(30);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task CanPostHotel()
        {
            // Arrange
            var hotelDTO = new HotelDTO
            {
                Name = "New Hotel",
                StreetAddress = "123 New Street",
                City = "New City",
                State = "New State",
                Phone = "1234567890"
            };
            var services = new HotelServices(_db);

            // Act
            var result = await services.PostHotel(hotelDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(hotelDTO.Name, result.Name);
            Assert.Equal(hotelDTO.StreetAddress, result.StreetAddress);
            var addedHotel = await services.GetHotel(result.ID);
            

        }
        [Fact]
        public async void UpdateHotel()
        {
            var hotel = await CreateNewHotel();
            var hotelDto = new HotelDTO()
            {
                ID = hotel.Id,
                Name = hotel.Name,
                City = hotel.City,
                StreetAddress = hotel.StreetAdress,
                State = hotel.State,
                Phone = hotel.Phone,
            };
            var room = await CreateRoom();
            var repository = new HotelServices(_db);
            var hotelDtoToUpdate = new HotelDTO()
            {
                Name = "Updated Name",
                City = "Updated City",
                StreetAddress = hotel.StreetAdress,
                State = hotel.State,
                Phone = hotel.Phone,
            };
            hotelDtoToUpdate.hotelRooms = hotelDto.hotelRooms;
            var actHotel = await repository.PutHotel(hotelDto.ID, hotelDtoToUpdate);



            Assert.Equal(hotelDtoToUpdate.City, actHotel.City);
            Assert.Equal(hotelDtoToUpdate.StreetAddress, actHotel.StreetAddress);
            Assert.Equal(hotelDtoToUpdate.State, actHotel.State);
            Assert.Equal(hotelDtoToUpdate.Phone, actHotel.Phone);

        }
        [Fact]
        public async void CanGetRoom()
        {
            var room = await CreateRoom();
            var services = new RoomsServices(_db);


            var actualRoom = await services.GetRoom(room.Id);

            Assert.Equal(actualRoom.Name,room.Name);
            Assert.Equal(actualRoom.Layout, room.Layout);
        }
        [Fact]
        public async Task CanGetAllRooms()
        {
            // Arrange
            var services = new RoomsServices(_db);

            // Act
            var ActualList = await services.GetRooms();

            // Assert
            Assert.NotNull(ActualList);
            Assert.NotEmpty(ActualList);
        }
        [Fact]
        public async Task CanDeleteRoom()
        {
            // Arrange
            var room = await CreateRoom();
            var services = new RoomsServices(_db);

            // Act
            await services.DeleteRoom(room.Id);

            // Assert
            var deletedRoom = await services.GetRoom(room.Id);
            Assert.Null(deletedRoom);
        }

        [Fact]
        public async void UpdateRoom()
        {
            var room = await CreateRoom();

            var roomDto = new RoomDTO()
            {
                Name = room.Name,
                Layout = room.Layout
            };
            var repository = new RoomsServices(_db);
            var roomDtoToUpdate = new RoomDTO()
            {
                Name = "Neww Name",
                Layout = 1,
            };

            await repository.PutRoom(room.Id, roomDtoToUpdate);
            var actRoom = await repository.GetRoom(room.Id);

            Assert.Equal(roomDtoToUpdate.Name, room.Name);
            Assert.Equal(roomDtoToUpdate.Layout, room.Layout);
        }
    }
}