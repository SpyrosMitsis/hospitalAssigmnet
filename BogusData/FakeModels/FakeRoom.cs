using Bogus;
using hospitals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BogusData.FakeModels
{
    internal class FakeRoom
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
                .RuleFor(u => u.nurse, new FakeNurse().GenerateNurse()
                );
        }

        public Room GenerateRoom()
        {
            return RoomFake.Generate();
        }
    }
}
