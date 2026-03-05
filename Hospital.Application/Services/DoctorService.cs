using System;
using System.Collections.Generic;
using Hospital.Domain.Entities;
using Hospital.Domain.Exceptions;
using Hospital.Domain.Interfaces;

namespace Hospital.Application.Services
{
    // Exposed to Console and other layers
    public class DoctorService : IDoctorService
    {
        private readonly IRepository<Doctor> _doctorRepository;

        public DoctorService(IRepository<Doctor> doctorRepository)
        {
            _doctorRepository = doctorRepository ?? throw new ArgumentNullException(nameof(doctorRepository));
        }

        public void AddDoctor(string name, string specialization, decimal consultationFee)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidDoctorException("Doctor name cannot be empty.");

            if (string.IsNullOrWhiteSpace(specialization))
                throw new InvalidDoctorException("Specialization cannot be empty.");

            if (consultationFee <= 0)
                throw new InvalidDoctorException("Consultation fee must be greater than zero.");

            var doctor = new Doctor
            {
                Name = name.Trim(),
                Specialization = specialization.Trim(),
                ConsultationFee = consultationFee
            };

            _doctorRepository.Add(doctor);
        }

        public IEnumerable<Doctor> GetDoctors()
        {
            return _doctorRepository.GetAll();
        }
    }
}