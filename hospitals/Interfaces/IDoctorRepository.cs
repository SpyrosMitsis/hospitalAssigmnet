using hospitals.Models;

namespace hospitals.Interfaces
{
    public interface IDoctorRepository
    {
        Task<Doctor> GetByIdAsync(int id);
    }
}
