using Lucid.PAMS.Domain;
using Lucid.PAMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucid.PAMS.Infrastructure
{
    public class ApplicationUnitOfWork : UnitOfWork, IApplicationUnitOfWork
    {
        public ApplicationUnitOfWork(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
