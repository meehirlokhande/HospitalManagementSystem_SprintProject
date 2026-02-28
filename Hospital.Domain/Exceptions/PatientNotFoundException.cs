using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Domain.Exceptions
{
    public class PatientNotFoundException: Exception
    {
        public PatientNotFoundException(string message):  base(message){ }
        public PatientNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
