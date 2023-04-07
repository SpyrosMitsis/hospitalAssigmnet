using hospitals.Interfaces;
using hospitals.Models;
using Microsoft.AspNetCore.Mvc;

namespace hospitals.Controllers
{
    public class AddressController : Controller
    {
        private readonly IAddressRepository _addressRepository;

        public AddressController(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<IActionResult> Index()
        {
            var addresses = await _addressRepository.GetAll();
            return View(addresses);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Address address = await _addressRepository.GetByIdAsync(id);
            return View(address);
        }
    }
}
