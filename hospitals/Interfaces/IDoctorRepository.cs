using hospitals.Models;

namespace hospitals.Interfaces
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetAll();
        Task<Doctor> GetByIdAsync(int id);
        Task<IEnumerable<Doctor>> GetDoctorBySalary(float minSalary, float maxSalary);
        Task<IEnumerable<Doctor>> GetDoctorByAge(int minAge, int maxAge);
        bool Add(Doctor doctor);
        bool Update(Doctor doctor);
        bool Delete(Doctor doctor);
        bool Save();
    }
}
