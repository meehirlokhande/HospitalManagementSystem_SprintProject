using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Domain.Entities
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }
        public decimal ConsultationFee { get; set; }

        //This shows that one doctor has many patients.
        public ICollection<Patient> Patients { get; set; } = new List<Patient>();
    }
}
