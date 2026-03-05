using Hospital.Domain.Entities;
using Hospital.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Infrastructure.Repositories
{
    public class DoctorRepositoryMemory:IRepository<Doctor>
    {
        private static readonly List<Doctor> _doctors = new();
        private static int _nextId = 1;

        public void Add(Doctor entity)
        {
            entity.DoctorId = _nextId++;
            _doctors.Add(entity);
        }

        public IEnumerable<Doctor> GetAll(){
            return _doctors.ToList();
        }

        public Doctor? GetById(int id){
            return _doctors.FirstOrDefault(d => d.DoctorId == id);
        }

        public void Update(Doctor entity){
            var doctor = GetById(entity.DoctorId);
            if(doctor == null){
                return;
            }

            doctor.Name = entity.Name;
            doctor.Specialization = entity.Specialization;
            doctor.ConsultationFee = entity.ConsultationFee;
        }

        public void Delete(Doctor entity){
            _doctors.Remove(entity);
        }
    }
}
