using Lucid.PAMS.Domain.Entities;
using Lucid.PAMS.Domain.Repositories;
using Lucid.PAMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucid.PAMS.Infrastructure.Repositories
{
    public class AppointmentRepository : Repository<Appointment,Guid>,IAppointmentRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public AppointmentRepository(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }
        public async Task<bool> IsAppointmentDuplicateAsync(Guid patinetId,Guid doctorId,DateTime date, Guid? id = null)
        {
            if (id.HasValue)
            {
                return await _dbContext.Appointments.AnyAsync(p => (p.PatientId == patinetId && p.DoctorId == doctorId && p.AppointmentDate == date) && p.Id != id.Value);
            }
            return await _dbContext.Appointments.AnyAsync(p => p.PatientId == patinetId && p.DoctorId == doctorId && p.AppointmentDate == date);
        }
    }
}
