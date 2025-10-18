using Lucid.PAMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lucid.PAMS.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Appointment>(appointment =>
            {
                appointment.HasKey(a => a.Id);
                appointment.Property(a => a.AppointmentDate).IsRequired();
                appointment.Property(a => a.Status).IsRequired().HasMaxLength(50);
                appointment.Property(a => a.TokenNumber).IsRequired();
                appointment.Property(a => a.PatientId).IsRequired();
                appointment.Property(a => a.DoctorId).IsRequired();
                appointment.HasOne(a => a.Patient)
                           .WithMany()
                           .HasForeignKey(a => a.PatientId)
                           .OnDelete(DeleteBehavior.Cascade);
                appointment.HasOne(a => a.Doctor)
                           .WithMany()
                           .HasForeignKey(a => a.DoctorId)
                           .OnDelete(DeleteBehavior.Cascade);
            });

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

            modelBuilder.Entity<Doctor>(doctor =>
            {
                doctor.HasKey(d => d.Id);
                doctor.Property(d => d.Name).IsRequired().HasMaxLength(100);
                doctor.Property(d => d.Department).IsRequired().HasMaxLength(100);
                doctor.Property(d => d.Phone).IsRequired().HasMaxLength(15);
                doctor.Property(d => d.Fee).IsRequired().HasColumnType("decimal(18,2)");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
