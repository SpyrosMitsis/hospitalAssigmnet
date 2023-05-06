using Bogus;
using hospitals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BogusData.FakeModels
{
    internal class FakeEmployee
    {
        Faker<Employee> employeeFake;

        public FakeEmployee()
        {
            Randomizer.Seed = new Random(210);

        employeeFake = new Faker<Employee>()
            .RuleFor(u => u.Id, f => (f.IndexFaker) + 1)
            .RuleFor(u => u.FirstName, f => f.Name.FirstName())
            .RuleFor(u => u.LastName, f => f.Name.LastName())
            .RuleFor(u => u.Age, f => f.Random.Int(30, 75))
            .RuleFor(u => u.Salary, f => f.Random.Float(40000, 90000)); 
        }

        public Employee GenerateEmployee()
        {
            return employeeFake.Generate();
        }
    }
}
