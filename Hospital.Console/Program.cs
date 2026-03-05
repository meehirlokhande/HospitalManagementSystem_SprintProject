using System;
using Hospital.Domain.Entities;
using Hospital.Domain.Interfaces;
using Hospital.Application.Services;
using Hospital.Infrastructure.Logging;
using Hospital.Infrastructure.Repositories;

namespace Hospital.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Day 2: use in-memory repositories and services (no DB yet)
            IRepository<Doctor> doctorRepository = new DoctorRepositoryMemory();
            IRepository<Patient> patientRepository = new PatientRepositoryMemory();

            IDoctorService doctorService = new DoctorService(doctorRepository);
            IPatientService patientService = new PatientService(patientRepository, doctorRepository);

            try
            {
                RunMenu(doctorService, patientService);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                System.Console.WriteLine("An unexpected error occurred. Please check errorlog.txt.");
                System.Console.ReadKey();
            }
        }

        private static void RunMenu(IDoctorService doctorService, IPatientService patientService)
        {
            bool exitRequested = false;

            while (!exitRequested)
            {
                System.Console.Clear();
                System.Console.WriteLine("========================================");
                System.Console.WriteLine("      HOSPITAL MANAGEMENT SYSTEM");
                System.Console.WriteLine("========================================");
                System.Console.WriteLine("1. Add Doctor");
                System.Console.WriteLine("2. List Doctors");
                System.Console.WriteLine("3. Add Patient");
                System.Console.WriteLine("4. List Patients");
                System.Console.WriteLine("5. Find Patient");
                System.Console.WriteLine("6. Edit Patient");
                System.Console.WriteLine("7. Delete Patient");
                System.Console.WriteLine("8. Exit");
                System.Console.WriteLine("========================================");
                System.Console.Write("Enter your choice: ");

                string? input = System.Console.ReadLine();

                switch (input)
                {
                    case "1":
                        HandleAddDoctor(doctorService);
                        break;
                    case "2":
                        HandleListDoctors(doctorService);
                        break;
                    case "3":
                        HandleAddPatient(patientService);
                        break;
                    case "4":
                        HandleListPatients(patientService);
                        break;
                    case "5":
                        HandleFindPatient(patientService);
                        break;
                    case "6":
                        HandleEditPatient(patientService);
                        break;
                    case "7":
                        HandleDeletePatient(patientService);
                        break;
                    case "8":
                        exitRequested = true;
                        break;
                    default:
                        System.Console.WriteLine("Invalid choice. Press any key to continue...");
                        System.Console.ReadKey();
                        break;
                }
            }
        }

        private static void HandleAddDoctor(IDoctorService doctorService)
        {
            try
            {
                System.Console.Write("Enter doctor name: ");
                string? name = System.Console.ReadLine();

                System.Console.Write("Enter specialization: ");
                string? specialization = System.Console.ReadLine();

                System.Console.Write("Enter consultation fee: ");
                string? feeInput = System.Console.ReadLine();

                if (!decimal.TryParse(feeInput, out var fee))
                {
                    System.Console.WriteLine("Invalid fee. Press any key to continue...");
                    System.Console.ReadKey();
                    return;
                }

                doctorService.AddDoctor(name ?? string.Empty, specialization ?? string.Empty, fee);
                System.Console.WriteLine("Doctor added successfully. Press any key to continue...");
                System.Console.ReadKey();
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                System.Console.WriteLine(ex.Message);
                System.Console.WriteLine("Press any key to continue...");
                System.Console.ReadKey();
            }
        }

        private static void HandleListDoctors(IDoctorService doctorService)
        {
            try
            {
                var doctors = doctorService.GetDoctors();

                System.Console.WriteLine("=== Doctors ===");
                foreach (var doctor in doctors)
                {
                    System.Console.WriteLine($"Id: {doctor.DoctorId}");
                    System.Console.WriteLine($"Name: {doctor.Name}");
                    System.Console.WriteLine($"Specialization: {doctor.Specialization}");
                    System.Console.WriteLine($"Consultation Fee: ₹{doctor.ConsultationFee:F2}");
                    System.Console.WriteLine("----------------------------------------");
                }

                System.Console.WriteLine("Press any key to continue...");
                System.Console.ReadKey();
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                System.Console.WriteLine(ex.Message);
                System.Console.WriteLine("Press any key to continue...");
                System.Console.ReadKey();
            }
        }

        private static void HandleAddPatient(IPatientService patientService)
        {
            try
            {
                System.Console.Write("Enter patient name: ");
                string? name = System.Console.ReadLine();

                System.Console.Write("Enter age: ");
                string? ageInput = System.Console.ReadLine();

                System.Console.Write("Enter condition: ");
                string? condition = System.Console.ReadLine();

                System.Console.Write("Enter appointment date (yyyy-MM-dd): ");
                string? dateInput = System.Console.ReadLine();

                System.Console.Write("Enter doctor id: ");
                string? doctorIdInput = System.Console.ReadLine();

                if (!int.TryParse(ageInput, out var age) ||
                    !DateTime.TryParse(dateInput, out var appointmentDate) ||
                    !int.TryParse(doctorIdInput, out var doctorId))
                {
                    System.Console.WriteLine("Invalid input. Press any key to continue...");
                    System.Console.ReadKey();
                    return;
                }

                patientService.AddPatient(
                    name ?? string.Empty,
                    age,
                    condition ?? string.Empty,
                    appointmentDate,
                    doctorId);

                System.Console.WriteLine("Patient added successfully. Press any key to continue...");
                System.Console.ReadKey();
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                System.Console.WriteLine(ex.Message);
                System.Console.WriteLine("Press any key to continue...");
                System.Console.ReadKey();
            }
        }

        private static void HandleListPatients(IPatientService patientService)
        {
            try
            {
                var patients = patientService.GetPatients();

                System.Console.WriteLine("=== Patients ===");
                foreach (var patient in patients)
                {
                    System.Console.WriteLine($"Id: {patient.PatientId}");
                    System.Console.WriteLine($"Name: {patient.Name}");
                    System.Console.WriteLine($"Age: {patient.Age}");
                    System.Console.WriteLine($"Condition: {patient.Condition}");
                    System.Console.WriteLine($"Appointment: {patient.AppointmentDate:yyyy-MM-dd}");
                    System.Console.WriteLine($"DoctorId: {patient.DoctorId}");
                    System.Console.WriteLine("----------------------------------------");
                }

                System.Console.WriteLine("Press any key to continue...");
                System.Console.ReadKey();
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                System.Console.WriteLine(ex.Message);
                System.Console.WriteLine("Press any key to continue...");
                System.Console.ReadKey();
            }
        }

        private static void HandleFindPatient(IPatientService patientService)
        {
            try
            {
                System.Console.Write("Enter patient name to search: ");
                string? name = System.Console.ReadLine();

                var patient = patientService.FindByName(name ?? string.Empty);

                if (patient == null)
                {
                    System.Console.WriteLine("Patient not found.");
                }
                else
                {
                    System.Console.WriteLine($"Id: {patient.PatientId}");
                    System.Console.WriteLine($"Name: {patient.Name}");
                    System.Console.WriteLine($"Age: {patient.Age}");
                    System.Console.WriteLine($"Condition: {patient.Condition}");
                    System.Console.WriteLine($"Appointment: {patient.AppointmentDate:yyyy-MM-dd}");
                    System.Console.WriteLine($"DoctorId: {patient.DoctorId}");
                }

                System.Console.WriteLine("Press any key to continue...");
                System.Console.ReadKey();
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                System.Console.WriteLine(ex.Message);
                System.Console.WriteLine("Press any key to continue...");
                System.Console.ReadKey();
            }
        }

        private static void HandleEditPatient(IPatientService patientService)
        {
            try
            {
                System.Console.Write("Enter patient id to edit: ");
                string? idInput = System.Console.ReadLine();

                if (!int.TryParse(idInput, out var patientId))
                {
                    System.Console.WriteLine("Invalid id. Press any key to continue...");
                    System.Console.ReadKey();
                    return;
                }

                // Optional updates: blank input keeps old value
                System.Console.Write("Enter new name (leave blank to keep): ");
                string? name = System.Console.ReadLine();
                System.Console.Write("Enter new age (leave blank to keep): ");
                string? ageInput = System.Console.ReadLine();
                System.Console.Write("Enter new condition (leave blank to keep): ");
                string? condition = System.Console.ReadLine();
                System.Console.Write("Enter new appointment date (yyyy-MM-dd, blank to keep): ");
                string? dateInput = System.Console.ReadLine();
                System.Console.Write("Enter new doctor id (leave blank to keep): ");
                string? doctorIdInput = System.Console.ReadLine();

                int? age = null;
                if (!string.IsNullOrWhiteSpace(ageInput) && int.TryParse(ageInput, out var parsedAge))
                    age = parsedAge;

                DateTime? appointmentDate = null;
                if (!string.IsNullOrWhiteSpace(dateInput) && DateTime.TryParse(dateInput, out var parsedDate))
                    appointmentDate = parsedDate;

                int? doctorId = null;
                if (!string.IsNullOrWhiteSpace(doctorIdInput) && int.TryParse(doctorIdInput, out var parsedDoctorId))
                    doctorId = parsedDoctorId;

                patientService.UpdatePatient(patientId, name, age, condition, appointmentDate, doctorId);

                System.Console.WriteLine("Patient updated successfully. Press any key to continue...");
                System.Console.ReadKey();
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                System.Console.WriteLine(ex.Message);
                System.Console.WriteLine("Press any key to continue...");
                System.Console.ReadKey();
            }
        }

        private static void HandleDeletePatient(IPatientService patientService)
        {
            try
            {
                System.Console.Write("Enter patient id to delete: ");
                string? idInput = System.Console.ReadLine();

                if (!int.TryParse(idInput, out var patientId))
                {
                    System.Console.WriteLine("Invalid id. Press any key to continue...");
                    System.Console.ReadKey();
                    return;
                }

                patientService.DeletePatient(patientId);

                System.Console.WriteLine("Patient deleted successfully. Press any key to continue...");
                System.Console.ReadKey();
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                System.Console.WriteLine(ex.Message);
                System.Console.WriteLine("Press any key to continue...");
                System.Console.ReadKey();
            }
        }
    }
}