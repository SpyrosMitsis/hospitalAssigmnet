using hospitals.Models;
using hospitals.Data;
using hospitals.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace hospitals.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly ApplicationDbContext _context;

        public RoomRepository(ApplicationDbContext context)
        {
            _context = context;

        }
        public bool Add(Room room)
        {
            _context.Add(room);
            return Save();
        }

        public bool Delete(Room room)
        {
            _context.Remove(room);
            return Save();
        }

        public async Task<IEnumerable<Room>> GetAll()
        {
            var rooms = _context.Room.OrderBy(r => r.RoomId).ToListAsync();
            return await rooms;
        }

        public async Task<Room> GetByIdAsync(int id)
        {
            var room = _context.Room.Include(r => r.Patients).Where(p => p.RoomId == id).FirstOrDefaultAsync();
            return await room;
        }
        public async Task<IEnumerable<Room>> GetByFloorAsync(string floor)
        {
            var rooms = _context.Room
                .Include(r => r.Patients)
                .Where(p => p.RoomName.StartsWith(floor))
                .ToListAsync();

            return await rooms;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Room room)
        {
            _context.Update(room);
            return Save();

        }
        public async Task<IEnumerable<string>> GetUniqueFloorNames()
        {
            var rooms = _context.Room
                .OrderBy(r => r.RoomName)
                .Select(r => r.RoomName.Substring(0, 1))
                .Distinct()
                .ToListAsync();

            return await rooms;
        }
    }
}
