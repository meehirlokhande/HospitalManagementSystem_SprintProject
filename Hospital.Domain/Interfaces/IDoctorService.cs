using System;
using System.Collections.Generic;
using System.Text;
using Hospital.Domain.Entities;

namespace Hospital.Domain.Interfaces
{
    public interface IDoctorService
    {
        void AddDoctor(string name, string specialization, decimal consultationFee);
        IEnumerable<Doctor> GetDoctors();
    }
}
