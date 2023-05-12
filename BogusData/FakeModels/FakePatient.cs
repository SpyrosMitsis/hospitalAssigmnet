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
                .RuleFor(u => u.Room, new FakeRoom().GenerateRoom())
                .RuleFor(u => u.Address, new FakeAddress().GenerateAddress())
                .RuleFor(u => u.Doctor, new FakeDoctor().GenerateDoctor()

                //.RuleFor(u => u.Address, new Address()
                //{
                //    Id = 1,
                //    Name = "Amalias 2",
                //    City = "Athens",
                //    Country = "Greece",
                //    PostalCode = "12345",
                //})
                //.RuleFor(u => u.Room, new FakeRoom().GenerateRoom())
                ////.RuleFor(u => u.Room, new Room()
                ////{
                ////    RoomId = 1,
                ////    RoomName = "A5",
                ////    //Nurse = new FakeNurse().GenerateNurse()
                ////})
                //.RuleFor(u => u.Doctor, new Doctor()
                //{
                //    Id = 1,
                //    FirstName = "Mario",
                //    LastName = "Luigi",
                //    Age = 45,
                //    Salary = 45000,
                //}

                    );
        }

        public Patient GeneratePatient()
        {
            return patientFake.Generate();
        }
    }
}
