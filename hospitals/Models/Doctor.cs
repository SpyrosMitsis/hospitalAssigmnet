namespace hospitals.Models;
    public class Doctor
    {
        public int Id { get; set; }
        public string  FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public double Salary { get; set; }

         public ICollection<Patient> Patients { get; set; } = null!;


    }
