using Lucid.PAMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucid.PAMS.Domain.Repositories
{
    public interface IPatientRepository : IRepository<Patient, Guid>
    {
        public Task<bool> IsPatientDuplicateAsync(string name, string phone, Guid? id = null);
    }
}
