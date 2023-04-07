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
            var rooms = await _roomRepository.GetAll();
            return View(rooms);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Room room = await _roomRepository.GetByIdAsync(id);
            return View(room);
        }
    }
}
