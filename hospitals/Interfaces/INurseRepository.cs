using hospitals.Models;

namespace hospitals.Interfaces
{
    public interface INurseRepository
    {
        Task<Nurse> GetByIdAsync(int id);
    }
}
