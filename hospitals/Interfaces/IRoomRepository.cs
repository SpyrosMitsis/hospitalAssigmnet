using hospitals.Models;

namespace hospitals.Interfaces
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetAll();
        Task<Room> GetByIdAsync(int id);
        Task<IEnumerable<Room>> GetByFloorAsync(string floor);
        Task<IEnumerable<string>> GetUniqueFloorNames();
        bool Add(Room room);
        bool Update(Room room);
        bool Delete(Room room);
        bool Save();
    }
}
