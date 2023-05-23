using hospitals.Interfaces;
using hospitals.Models;
using Microsoft.AspNetCore.Mvc;

namespace hospitals.Controllers
{
    public class AddressController : Controller
{
    private readonly IAddressRepository _addressRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="AddressController"/> class.
    /// </summary>
    /// <param name="addressRepository">The address repository.</param>
    public AddressController(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }

    /// <summary>
    /// Displays the list of addresses.
    /// </summary>
    /// <returns>The task that represents the asynchronous operation.</returns>
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

    /// <summary>
    /// Displays the details of a specific address.
    /// </summary>
    /// <param name="id">The ID of the address.</param>
    /// <returns>The task that represents the asynchronous operation.</returns>
    public async Task<IActionResult> Detail(int id)
    {
        Address address = await _addressRepository.GetByIdAsync(id);
        return View(address);
    }
}

}
