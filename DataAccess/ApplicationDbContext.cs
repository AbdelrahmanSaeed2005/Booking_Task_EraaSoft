using Microsoft.EntityFrameworkCore;
using Task12_EraaSoft.Models;

namespace Task12_EraaSoft.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Hospital12;Integrated Security=True;TrustServerCertificate=True");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Specialization>().HasData(
                 new Specialization { SpecializationID = 1, Name = "Cardiology" },
                new Specialization { SpecializationID = 2, Name = "Pediatrics" },
                new Specialization { SpecializationID = 3, Name = "Dermatology" },
                new Specialization { SpecializationID = 4, Name = "Orthopedics" },
                new Specialization { SpecializationID = 5, Name = "Neurology" }
            );

            modelBuilder.Entity<Doctor>().HasData(
               new Doctor { DoctorId = 1, Name = "Dr. John Smith", SpecializationID = 1, Img = "doctor1.jpg" },
                    new Doctor { DoctorId = 2, Name = "Dr. Sarah Johnson", SpecializationID = 2, Img = "doctor2.jpg" },
                    new Doctor { DoctorId = 3, Name = "Dr. Emily Davis", SpecializationID = 3, Img = "doctor4.jpg" },
                    new Doctor { DoctorId = 4, Name = "Dr. Michael Lee", SpecializationID = 4, Img = "doctor3.jpg" },
                    new Doctor { DoctorId = 5, Name = "Dr. William Clark", SpecializationID = 5, Img = "doctor5.jpg" }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}
