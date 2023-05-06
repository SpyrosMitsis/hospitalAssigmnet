using System.ComponentModel.DataAnnotations.Schema;

namespace hospitals.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string  FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public double Salary { get; set; }
    }
}
