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

            var city = Request.Query["city"];
            var country = Request.Query["country"];
            var addresses = await _addressRepository.GetAll();

            if (!string.IsNullOrEmpty(city))
            {
                string cityName = city.ToString();
                addresses = await _addressRepository.GetCityAsync(cityName);
            }
            if (!string.IsNullOrEmpty(country))
            {
                string countryName = country.ToString();
                addresses = await _addressRepository.GetCountryAsync(countryName);
            }


            ViewBag.UniqueCities = await _addressRepository.GetUniqueCitiesAsync();
            ViewBag.UniqueCountries = await _addressRepository.GetUniqueCountriesAsync();
            return View(addresses);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Address address = await _addressRepository.GetByIdAsync(id);
            return View(address);
        }

    }
}
