using hospitals.Models;
using hospitals.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Logging;

namespace hospitals.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly ILogger<DoctorController> _logger;

        public DoctorController(IDoctorRepository doctorRepository, ILogger<DoctorController> logger)
        {
            _doctorRepository = doctorRepository;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {

            var salaryMin = Request.Query["salary_min"];
            var salaryMax = Request.Query["salary_max"];
            var minAge = Request.Query["older_than"];
            var maxAge = Request.Query["younger_than"];

            var doctors = await _doctorRepository.GetAll();
            
            // Filter by Salary
            if (!string.IsNullOrEmpty(salaryMin) && !string.IsNullOrEmpty(salaryMax))
            {
                var minSalary = float.Parse(salaryMin);
                var maxSalary = float.Parse(salaryMax);
                doctors  = await _doctorRepository.GetDoctorBySalary(minSalary, maxSalary);
            }
            // Filter by age
            if (!string.IsNullOrEmpty(minAge) && !string.IsNullOrEmpty(maxAge))
            {
                var olderThan = int.Parse(minAge);
                var youngerThan = int.Parse(maxAge);
                doctors = await _doctorRepository.GetDoctorByAge(youngerThan, olderThan);
            }
            // log here
            _logger.LogInformation($"{DateTime.Now} Doctor Shown Correctly");
            return View(doctors);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Doctor doctor = await _doctorRepository.GetByIdAsync(id);
            return View(doctor);

        }
    }
}
