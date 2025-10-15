using Lucid.PAMS.Domain.Entities;
using Lucid.PAMS.Domain.Repositories;
using Lucid.PAMS.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucid.PAMS.Infrastructure.Repositories
{
    public class PatientRepository : Repository<Patient,Guid>,IPatientRepository
    {
        public PatientRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
