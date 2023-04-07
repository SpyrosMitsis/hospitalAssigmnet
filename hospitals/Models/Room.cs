namespace hospitals.Models;
    public class Room
    {
        public int RoomId { get; set; }
        public string RoomName{ get; set; }
        public ICollection<Patient> Patients { get; set; } = null!;
    }
