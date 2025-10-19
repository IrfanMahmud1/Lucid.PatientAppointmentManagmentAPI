using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucid.PAMS.Domain.Dtos
{
    public class UpdateAppointmentDto
    {
        public Guid Id { get; set; }
        public string Status { get; set; }

    }
}
