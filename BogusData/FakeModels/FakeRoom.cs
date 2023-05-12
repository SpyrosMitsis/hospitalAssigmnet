using Bogus;
using hospitals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BogusData.FakeModels
{
    public class FakeRoom
    {
        Faker<Room> RoomFake;

        public FakeRoom()
        {
            Randomizer.Seed = new Random(210);

            RoomFake = new Faker<Room>()
                .RuleFor(u => u.RoomId, f => (f.IndexFaker) + 1)
                .RuleFor(u => u.RoomName, f =>
                {
                    string floor = f.PickRandom<Floor>().ToString();
                    return floor + f.Random.Replace("##");
                }
                )
                .RuleFor(u => u.Nurse, new Nurse()
                {
                    Id = 2,
                    Age = 34,
                    FirstName = "John",
                    LastName = "Stamos",
                    Salary = 4000
                }
                );
        }

        public Room GenerateRoom()
        {
            return RoomFake.Generate();
        }
    }
}
