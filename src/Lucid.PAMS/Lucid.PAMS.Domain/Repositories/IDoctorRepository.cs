using Lucid.PAMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucid.PAMS.Domain.Repositories
{
    public interface IDoctorRepository : IRepository<Doctor, Guid>
    {
    }
}
