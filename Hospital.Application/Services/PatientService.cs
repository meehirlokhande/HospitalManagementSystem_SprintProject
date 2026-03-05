using System;
using System.Collections.Generic;
using System.Linq;
using Hospital.Domain.Entities;
using Hospital.Domain.Exceptions;
using Hospital.Domain.Interfaces;

namespace Hospital.Application.Services
{
    public class PatientService : IPatientService
    {
        private readonly IRepository<Patient> _patientRepository;
        private readonly IRepository<Doctor> _doctorRepository;

        public PatientService(IRepository<Patient> patientRepository, IRepository<Doctor> doctorRepository)
        {
            _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
            _doctorRepository = doctorRepository ?? throw new ArgumentNullException(nameof(doctorRepository));
        }

        public void AddPatient(string name, int age, string condition, DateTime appointment, int doctorId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Patient name cannot be empty.", nameof(name));

            if (age <= 0)
                throw new ArgumentException("Age must be greater than zero.", nameof(age));

            if (appointment.Date < DateTime.Today)
                throw new ArgumentException("Appointment date cannot be in the past.", nameof(appointment));

            var doctor = _doctorRepository.GetById(doctorId);
            if (doctor is null)
                throw new InvalidDoctorException("Doctor not found for the given id.");

            var patient = new Patient
            {
                Name = name.Trim(),
                Age = age,
                Condition = condition?.Trim() ?? string.Empty,
                AppointmentDate = appointment,
                DoctorId = doctorId
            };

            _patientRepository.Add(patient);
        }

        public IEnumerable<Patient> GetPatients()
        {
            return _patientRepository.GetAll();
        }

        public Patient? FindByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            return _patientRepository
                .GetAll()
                .FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public void UpdatePatient(int patientId, string? name, int? age, string? condition, DateTime? appointmentDate, int? doctorId)
        {
            var existing = _patientRepository.GetById(patientId);
            if (existing is null)
                throw new PatientNotFoundException($"Patient with id {patientId} was not found.");

            if (!string.IsNullOrWhiteSpace(name))
                existing.Name = name.Trim();

            if (age.HasValue)
            {
                if (age.Value <= 0)
                    throw new ArgumentException("Age must be greater than zero.", nameof(age));

                existing.Age = age.Value;
            }

            if (!string.IsNullOrWhiteSpace(condition))
                existing.Condition = condition.Trim();

            if (appointmentDate.HasValue)
            {
                if (appointmentDate.Value.Date < DateTime.Today)
                    throw new ArgumentException("Appointment date cannot be in the past.", nameof(appointmentDate));

                existing.AppointmentDate = appointmentDate.Value;
            }

            if (doctorId.HasValue)
            {
                var doctor = _doctorRepository.GetById(doctorId.Value);
                if (doctor is null)
                    throw new InvalidDoctorException("Doctor not found for the given id.");

                existing.DoctorId = doctorId.Value;
            }

            _patientRepository.Update(existing);
        }

        public void DeletePatient(int patientId)
        {
            var existing = _patientRepository.GetById(patientId);
            if (existing is null)
                throw new PatientNotFoundException($"Patient with id {patientId} was not found.");

            _patientRepository.Delete(existing);
        }
    }
}