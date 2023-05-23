using hospitals.Models;
using hospitals.Data;
using hospitals.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace hospitals.Repository
{
    /// <summary>
    /// Repository class for managing rooms in the database.
    /// </summary>
    public class RoomRepository : IRoomRepository
    {
        private readonly ApplicationDbContext _context;
    
        public RoomRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    
        /// <summary>
        /// Adds a new room to the repository.
        /// </summary>
        /// <param name="room">The room to add.</param>
        /// <returns>True if the room was added successfully, false otherwise.</returns>
        public bool Add(Room room)
        {
            _context.Add(room);
            return Save();
        }
    
        /// <summary>
        /// Deletes a room from the repository.
        /// </summary>
        /// <param name="room">The room to delete.</param>
        /// <returns>True if the room was deleted successfully, false otherwise.</returns>
        public bool Delete(Room room)
        {
            _context.Remove(room);
            return Save();
        }
    
        /// <summary>
        /// Gets all rooms from the repository.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the list of rooms.</returns>
        public async Task<IEnumerable<Room>> GetAll()
        {
            var rooms = _context.Room.OrderBy(r => r.RoomId).ToListAsync();
            return await rooms;
        }
    
        /// <summary>
        /// Gets a room by its ID from the repository.
        /// </summary>
        /// <param name="id">The ID of the room to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the room.</returns>
        public async Task<Room> GetByIdAsync(int id)
        {
            var room = _context.Room.Include(r => r.Patients).Where(p => p.RoomId == id).FirstOrDefaultAsync();
            return await room;
        }
    
        /// <summary>
        /// Gets rooms on a specific floor from the repository.
        /// </summary>
        /// <param name="floor">The floor of the rooms to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the list of rooms on the specified floor.</returns>
        public async Task<IEnumerable<Room>> GetByFloorAsync(string floor)
        {
            var rooms = _context.Room
                .Include(r => r.Patients)
                .Where(p => p.RoomName.StartsWith(floor))
                .ToListAsync();
    
            return await rooms;
        }
    
        /// <summary>
        /// Saves changes made to the repository.
        /// </summary>
        /// <returns>True if changes were saved successfully, false otherwise.</returns>
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    
        /// <summary>
        /// Updates a room in the repository.
        /// </summary>
        /// <param name="room">The room to update.</param>
        /// <returns>True if the room was updated successfully, false otherwise.</returns>
        public bool Update(Room room)
        {
            _context.Update(room);
            return Save();
        }
    
        /// <summary>
        /// Gets the unique floor names from the repository.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the list of unique floor names.</returns>
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
