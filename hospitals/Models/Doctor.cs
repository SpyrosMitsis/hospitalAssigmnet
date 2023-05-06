using System.ComponentModel.DataAnnotations.Schema;

namespace hospitals.Models;

    public class Doctor : Employee
    {
    public ICollection<Patient> Patients { get; set; } = null!;
    }
