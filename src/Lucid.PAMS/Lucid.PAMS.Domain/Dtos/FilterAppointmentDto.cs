using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucid.PAMS.Domain.Dtos
{
    public class FilterAppointmentDto
    {
        public Guid? DoctorId { get; set; }
        public DateTime? AppointmentDate { get; set; }

    }
}
