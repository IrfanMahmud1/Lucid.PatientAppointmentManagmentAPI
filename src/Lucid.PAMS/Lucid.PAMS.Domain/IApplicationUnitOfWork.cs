using Lucid.PAMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucid.PAMS.Domain
{
    public interface IApplicationUnitOfWork : IUnitOfWork
    {
        public IPatientRepository PatientRepository { get; }
        public IDoctorRepository DoctorRepository { get; }
        public IAppointmentRepository AppointmentRepository { get; }
    }
}
