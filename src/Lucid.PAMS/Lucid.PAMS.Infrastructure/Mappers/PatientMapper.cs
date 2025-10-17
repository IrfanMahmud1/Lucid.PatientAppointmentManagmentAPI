using AutoMapper;
using Lucid.PAMS.Domain.Dtos;
using Lucid.PAMS.Domain.Entities;
using Lucid.PAMS.Domain.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucid.PAMS.Infrastructure.Mappers
{
    public class PatientMapper : IPatientMapper
    {
        private readonly IMapper _mapper;
        public PatientMapper(IMapper mapper)
        {
            _mapper = mapper;
        }
        public PatientDto Map(Patient patient)
        {
            return _mapper.Map<PatientDto>(patient);
        }
        public Patient Map(PatientDto patient)
        {
            return _mapper.Map<Patient>(patient);
        }
        public Patient Map(CreatePatientDto patient)
        {
            return _mapper.Map<Patient>(patient);
        }
        public Patient Map(UpdatePatientDto patient)
        {
            return _mapper.Map<Patient>(patient);
        }
    }
}
