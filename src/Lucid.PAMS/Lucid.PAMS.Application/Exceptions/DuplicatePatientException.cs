using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucid.PAMS.Application.Exceptions
{
    public class DuplicatePatientException : Exception
    {
        public DuplicatePatientException(string name) : base(name) { }
    }
}
