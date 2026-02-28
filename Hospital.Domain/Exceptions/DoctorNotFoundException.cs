using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Domain.Exceptions
{
    public class DoctorNotFoundException:Exception
    { 
        public DoctorNotFoundException(string message) : base(message){ }
        public DoctorNotFoundException(string message, Exception inner) : base(message, inner) { } 
    }
}
