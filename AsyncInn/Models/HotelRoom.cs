namespace AsyncInn.Models
{
    public class HotelRoom
    {
        public int HotelId { get; set; }
        public int RoomNumber { get; set; }

        public int RoomId { get; set; }

        public decimal Rate { get; set; }

      
        public bool isPetFriendly { get; set; }

        public Hotel hotel { get; set; }

        public Room room { get; set; }
    }
}
