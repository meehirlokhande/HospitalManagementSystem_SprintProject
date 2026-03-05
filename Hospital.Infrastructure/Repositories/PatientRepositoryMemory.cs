using System.Collections.Generic;
using System.Linq;
using Hospital.Domain.Entities;
using Hospital.Domain.Interfaces;
using System;

namespace Hospital.Infrastructure.Repositories
{
    public class PatientRepositoryMemory:IRepository<Patient>
    {
        private static readonly List<Patient> _patients = new();
        private static int _nextId = 1;

        public void Add(Patient entity){
            entity.PatientId = _nextId++;
            _patients.Add(entity);
        }

        public IEnumerable<Patient> GetAll(){
            return _patients.ToList();
        }

        public Patient? GetById(int id){
            return _patients.FirstOrDefault(p => p.PatientId == id);
        }

        public void Update(Patient entity){
            var patient = GetById(entity.PatientId);
            if(patient == null){
                return;
            }

            patient.Name = entity.Name;
            patient.Age = entity.Age;
            patient.Condition = entity.Condition;
            patient.AppointmentDate = entity.AppointmentDate;
            patient.DoctorId = entity.DoctorId;
        }

        public void Delete(Patient entity){
            _patients.Remove(entity);
        }
    }
}
