using hospitals.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace hospitals.Data
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options ) : base(options)
        {
        }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Nurse> Nurse { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<Address> Address { get; set; }

    }
}
