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
    public class DoctorRepository : Repository<Doctor,Guid>, IDoctorRepository
    {
        public DoctorRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
