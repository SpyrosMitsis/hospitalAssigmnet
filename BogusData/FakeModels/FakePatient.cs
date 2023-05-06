using Bogus;
using hospitals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BogusData.FakeModels
{
    public class FakePatient
    {
        Faker<Patient> patientFake;
        public List<int> DocId { get; set; }
        public FakePatient()
        {
            Randomizer.Seed = new Random(210);

            patientFake = new Faker<Patient>()
                .RuleFor(u => u.PatientId, f => (f.IndexFaker) + 1)
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.LastName())
                .RuleFor(u => u.Age, f => f.Random.Int(30, 75))
                .RuleFor(u => u.EntryDate, f => f.Date.Past().Date)
                .RuleFor(u => u.ExitDate, f => f.Date.Future().Date)
                .RuleFor(u => u.Address, f => new FakeAddress().GenerateAddress())
                .RuleFor(u => u.Room, f => new FakeRoom().GenerateRoom())
                .RuleFor(u => u.Doctor, f => new FakeDoctor().GenerateDoctor()
                    );
        }

        public Patient GeneratePatient()
        {
            return patientFake.Generate();
        }
    }
}
