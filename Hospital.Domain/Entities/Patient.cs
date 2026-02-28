using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Domain.Entities
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Condition { get; set; } = string.Empty;
        public DateTime AppointmentDate { get; set; }

        //Foreign Key: which doctor this patient assigned to.    
        public int DoctorId { get; set; }

        //Navigation: To get doctor name.
        public Doctor Doctor { get; set; } = null;
    }
}
