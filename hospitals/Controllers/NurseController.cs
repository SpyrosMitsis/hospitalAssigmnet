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

        public NurseController(INurseRepository nurseRepository, IEmployeeRepository employeeRepository)
        {
            _nurseRepository = nurseRepository;
            _employeeRepository = employeeRepository;
            
        }
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


            if (!string.IsNullOrEmpty(salaryMin) && !string.IsNullOrEmpty(salaryMax))
            {
                var minSalary = float.Parse(salaryMin);
                var maxSalary = float.Parse(salaryMax);
                nurses = await _employeeRepository.GetEmployeeBySalary<Nurse>(minSalary, maxSalary);
            }
            if (!string.IsNullOrEmpty(minAge) && !string.IsNullOrEmpty(maxAge))
            {
                var olderThan = int.Parse(minAge);
                var youngerThan = int.Parse(maxAge);
                nurses = await _employeeRepository.GetEmployeeByAge<Nurse>(olderThan, youngerThan);
            }
            return View(nurses);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Nurse nurse = await _nurseRepository.GetByIdAsync(id);
            return View(nurse);

        }
    }
}
