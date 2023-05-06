using hospitals.Models;
using hospitals.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace hospitals.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientRepository _patientRepository;

        public PatientController(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
            
        }

        public async Task<IActionResult> Index()
        {
            var groupByRoom = Request.Query["filter_by_room"];
            var groupByDoc= Request.Query["filter_by_doc"];
            var groupByAddress= Request.Query["filter_by_address"];
            var minAge = Request.Query["older_than"];
            var maxAge = Request.Query["younger_than"];
            var showAddress = Request.Query["show_address"];


            var patients = await _patientRepository.GetAll();

            if (string.Equals(showAddress,"true"))
            {
                var patientsWithRoom = await _patientRepository.GetAllWithAddresses();
                return View("PatientsWithAddresses",patientsWithRoom);
            }
            if (string.Equals(groupByRoom,"true"))
            {
                var patientsByRoom = await _patientRepository.GetAllByRoom();
                return View("GrouppedByRoom", patientsByRoom);
            }
            if (string.Equals(groupByDoc,"true"))
            {
                var patientsByDoc = await _patientRepository.GetAllByDoctor();
                return View("GrouppedByDoctor", patientsByDoc);
            }
            if (string.Equals(groupByAddress,"true"))
            {
                var patientsByAddress= await _patientRepository.GetAllByAddress();
                return View("GrouppedByAddress", patientsByAddress);
            }
            if (!string.IsNullOrEmpty(minAge) && !string.IsNullOrEmpty(maxAge))
            {
                var olderThan = int.Parse(minAge);
                var youngerThan = int.Parse(maxAge);
                patients = await _patientRepository.GetPatientByAge(youngerThan, olderThan);
            }

            return View(patients);
        }

        public async Task<IActionResult> Detail(int id)
        {
           Patient patient = await _patientRepository .GetByIdAsync(id);
           return View(patient);
        }
    }
}
