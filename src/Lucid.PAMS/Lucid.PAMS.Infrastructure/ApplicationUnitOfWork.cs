using Lucid.PAMS.Domain;
using Lucid.PAMS.Domain.Repositories;
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
        public IPatientRepository PatientRepository { get; private set; }
        public IDoctorRepository DoctorRepository { get; private set; }
        public ApplicationUnitOfWork(ApplicationDbContext applicationDbContext,
            IPatientRepository patientRepository,
            IDoctorRepository doctorRepository) : base(applicationDbContext)
        {
            PatientRepository = patientRepository;
            DoctorRepository = doctorRepository;
        }
    }
}
