using hospitals.Models;

namespace hospitals.Interfaces
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetAll();
        Task<IEnumerable<Patient>> GetAllWithAddresses();
        Task<Patient> GetByIdAsync(int id);
        Task<IEnumerable<IGrouping<Doctor, Patient>>> GetAllByDoctor();
        Task<IEnumerable<IGrouping<Room, Patient>>> GetAllByRoom();
        Task<IEnumerable<IGrouping<Address, Patient>>> GetAllByAddress();
        Task<IEnumerable<Patient>> GetPatientByAge(int minAge, int maxAge);
        bool Add(Patient patient);
        bool Update(Patient patient);
        bool Delete(Patient patient);
        bool Save();
    }
}
