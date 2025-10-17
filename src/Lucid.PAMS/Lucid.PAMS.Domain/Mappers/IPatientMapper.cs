using Lucid.PAMS.Domain.Dtos;
using Lucid.PAMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucid.PAMS.Domain.Mappers
{
    public interface IPatientMapper
    {
        public PatientDto Map(Patient patient);
        public Patient Map(PatientDto patient);
        public Patient Map(CreatePatientDto patient);
        public Patient Map(UpdatePatientDto patient);
    }
}
