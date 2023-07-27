namespace AsyncInn.Models
{
    public class Room
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int  Layout { get; set; }

        public List<Amenities> Amenities { get; set; } 
        public List<HotelRoom> HotelRooms { get; set; } 
    }
}
