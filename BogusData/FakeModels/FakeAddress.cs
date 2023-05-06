using Bogus;
using hospitals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BogusData.FakeModels
{
    public class FakeAddress
    {
        Faker<Address> addressFake;
        public FakeAddress()
        {
            Randomizer.Seed = new Random(210);

            addressFake = new Faker<Address>()
                .RuleFor(u => u.Id, f => (f.IndexFaker) + 1)
                .RuleFor(u => u.Name, f => f.Address.StreetAddress())
                .RuleFor(u => u.Country, f => f.Address.Country())
                .RuleFor(u => u.City, f => f.Address.City())
                .RuleFor(u => u.PostalCode, f => f.Address.ZipCode()
                );
        }
        public Address GenerateAddress()
        {
            return addressFake.Generate();
        }
    }
}
