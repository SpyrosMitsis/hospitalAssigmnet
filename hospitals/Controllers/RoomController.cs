using hospitals.Models;
using hospitals.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace hospitals.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomRepository _roomRepository;

        public RoomController(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<IActionResult> Index()
        {
            var floor = Request.Query["floor"];
            var rooms = await _roomRepository.GetAll();


            //Filter By Floor
            if (!string.IsNullOrEmpty(floor))
            {
                string floor_name = floor.ToString();
                rooms = await _roomRepository.GetByFloorAsync(floor_name);
            }
            ViewBag.UniqueFloors = await _roomRepository.GetUniqueFloorNames();
            return View(rooms);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Room room = await _roomRepository.GetByIdAsync(id);
            return View(room);
        }
    }
}
