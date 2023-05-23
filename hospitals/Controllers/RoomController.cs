using hospitals.Models;
using hospitals.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace hospitals.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomRepository _roomRepository;
    
        /// <summary>
        /// Initializes a new instance of the <see cref="RoomController"/> class.
        /// </summary>
        /// <param name="roomRepository">The room repository.</param>
        public RoomController(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }
    
        /// <summary>
        /// Displays the list of rooms.
        /// </summary>
        /// <returns>The task that represents the asynchronous operation.</returns>
        public async Task<IActionResult> Index()
        {
            var floor = Request.Query["floor"];
            var rooms = await _roomRepository.GetAll();
    
            // Filter By Floor
            if (!string.IsNullOrEmpty(floor))
            {
                string floorName = floor.ToString();
                rooms = await _roomRepository.GetByFloorAsync(floorName);
            }
            
            ViewBag.UniqueFloors = await _roomRepository.GetUniqueFloorNames();
            return View(rooms);
        }
    
        /// <summary>
        /// Displays the details of a specific room.
        /// </summary>
        /// <param name="id">The ID of the room.</param>
        /// <returns>The task that represents the asynchronous operation.</returns>
        public async Task<IActionResult> Detail(int id)
        {
            Room room = await _roomRepository.GetByIdAsync(id);
            return View(room);
        }
    }

}
