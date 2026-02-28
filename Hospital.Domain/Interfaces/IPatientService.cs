using System;
using System.Collections.Generic;
using System.Text;
using Hospital.Domain.Entities;

namespace Hospital.Domain.Interfaces
{
    public interface IPatientService
    {
        void AddPatient(string name, int age, string condition, DateTime appointment,int doctorId);
        IEnumerable<Patient>GetPatients();
        Patient? FindByName(string name);
        void UpdatePatient(int patientId, string? name,int? age,string? condition,DateTime? appointmentDate,int? doctorId);
        void DeletePatient(int patientId);

    }
}
