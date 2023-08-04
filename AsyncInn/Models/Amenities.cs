namespace AsyncInn.Models
{
    public class Amenities
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Room> Rooms { get; set; }   
    }
}
