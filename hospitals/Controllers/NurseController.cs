using hospitals.Interfaces;
using hospitals.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace hospitals.Controllers
{
    public class NurseController : Controller
    {
        private readonly INurseRepository _nurseRepository;
        private readonly IEmployeeRepository _employeeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="NurseController"/> class.
        /// </summary>
        /// <param name="nurseRepository">The nurse repository.</param>
        /// <param name="employeeRepository">The employee repository.</param>
        public NurseController(INurseRepository nurseRepository, IEmployeeRepository employeeRepository)
        {
            _nurseRepository = nurseRepository;
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Displays the list of nurses.
        /// </summary>
        /// <returns>The task that represents the asynchronous operation.</returns>
        public async Task<IActionResult> Index()
        {
            var salaryMin = Request.Query["salary_min"];
            var salaryMax = Request.Query["salary_max"];
            var minAge = Request.Query["older_than"];
            var maxAge = Request.Query["younger_than"];

            var nurses = await _employeeRepository.GetAll<Nurse>();

            ViewBag.MaxAge = await _employeeRepository.MaxAgeAsync<Nurse>();
            ViewBag.MinAge = await _employeeRepository.MinAgeAsync<Nurse>();
            ViewBag.MaxSalary = await _employeeRepository.MaxSalaryAsync<Nurse>();
            ViewBag.MinSalary = await _employeeRepository.MinSalaryAsync<Nurse>();

            // Filter by Salary
            if (!string.IsNullOrEmpty(salaryMin) && !string.IsNullOrEmpty(salaryMax))
            {
                var minSalary = float.Parse(salaryMin);
                var maxSalary = float.Parse(salaryMax);
                nurses = await _employeeRepository.GetEmployeeBySalary<Nurse>(minSalary, maxSalary);
            }

            // Filter by age
            if (!string.IsNullOrEmpty(minAge) && !string.IsNullOrEmpty(maxAge))
            {
                var olderThan = int.Parse(minAge);
                var youngerThan = int.Parse(maxAge);
                nurses = await _employeeRepository.GetEmployeeByAge<Nurse>(olderThan, youngerThan);
            }

            return View(nurses);
        }

        /// <summary>
        /// Displays the details of a specific nurse.
        /// </summary>
        /// <param name="id">The ID of the nurse.</param>
        /// <returns>The task that represents the asynchronous operation.</returns>
        public async Task<IActionResult> Detail(int id)
        {
            Nurse nurse = await _nurseRepository.GetByIdAsync(id);
            return View(nurse);
        }
    }
}
