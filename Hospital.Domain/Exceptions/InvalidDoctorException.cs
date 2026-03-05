using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Domain.Exceptions
{
    public class InvalidDoctorException:Exception
    {
        public InvalidDoctorException(string message) : base(message) { }
        public InvalidDoctorException(string message, Exception inner) : base(message, inner) { }

    }
}
