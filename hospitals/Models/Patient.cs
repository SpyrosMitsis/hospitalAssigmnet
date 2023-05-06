
namespace hospitals.Models
{
    public class Patient
    {
        //[Key]
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ExitDate { get; set; }

        //[ForeignKey("Address")]
        //public int AddressId{ get; set; }
        public Address Address { get; set; } = null!;

        //[ForeignKey("Room")]
        //public int RoomId { get; set; }
        public Room Room { get; set; } = null!;

        //[ForeignKey("Doctor")]
        //public int DoctorId { get; set; }
        public Doctor Doctor { get; set; } = null!;

    }
}
