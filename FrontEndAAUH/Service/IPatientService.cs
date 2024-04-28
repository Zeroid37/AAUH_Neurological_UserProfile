using FrontEndAAUH.Models;

namespace FrontEndAAUH.Service {
    public interface IPatientService {
        Task<Patient> getPatientByPatientNo(string patientNo);
        Task<bool> postPatient(Patient patient);
    }
}
