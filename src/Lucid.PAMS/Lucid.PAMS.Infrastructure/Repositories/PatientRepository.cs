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
    public class PatientRepository : Repository<Patient,Guid>,IPatientRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public PatientRepository(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }
        public async Task<bool> IsPatientDuplicateAsync(string name, string phone, Guid? id = null)
        {
            if (id.HasValue)
            {
                return await _dbContext.Patients.AnyAsync(p => (p.Name == name && p.Phone == phone) && p.Id != id.Value);
            }
            return await _dbContext.Patients.AnyAsync(p => p.Name == name && p.Phone == phone);
        }
    }
}
