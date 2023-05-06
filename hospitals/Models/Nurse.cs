using System.ComponentModel.DataAnnotations.Schema;

namespace hospitals.Models
{
    public class Nurse : Employee
    {
        public ICollection<Room> Rooms { get; set; }
    }
}
