using Lucid.PAMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lucid.PAMS.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Appointment>()
            //    .Property(a => a.AppointmentDate)
            //    .HasDefaultValueSql("GETDATE()");
            //modelBuilder.Entity<Appointment>()
            //    .Property(a => a.Status)
            //    .HasDefaultValue("Pending");

                
            modelBuilder.Entity<Patient>(patient => 
            {
                patient.HasKey(p => p.Id);
                patient.Property(p => p.CreatedDate).HasDefaultValueSql("GETDATE()").IsRequired();
                patient.Property(p => p.Name).IsRequired().HasMaxLength(100);
                patient.Property(p => p.Phone).IsRequired().HasMaxLength(15);
                patient.Property(p => p.Gender).IsRequired().HasMaxLength(10);
                patient.Property(p => p.Address).IsRequired().HasMaxLength(200);
                patient.Property(p => p.Age).IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
