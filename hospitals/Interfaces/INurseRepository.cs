using hospitals.Models;

namespace hospitals.Interfaces
{
    public interface INurseRepository
    {
        Task<IEnumerable<Nurse>> GetAll();
        Task<Nurse> GetByIdAsync(int id);
        Task<IEnumerable<Nurse>> GetNurseBySalary(float minSalary, float maxSalary);
        Task<IEnumerable<Nurse>> GetNurseByAge(int minAge, int maxAge);
        bool Add(Nurse nurse);
        bool Update(Nurse nurse);
        bool Delete(Nurse nurse);
        bool Save();
    }
}
