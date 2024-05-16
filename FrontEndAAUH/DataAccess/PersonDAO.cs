using FrontEndAAUH.Models;

namespace FrontEndAAUH.DataAccess {
    public interface PersonDAO {
        public bool addPatientToDB(Patient patient);
        public bool addClinicalProfessionalToDB(ClinicProfessional clinicProfessional);
        public bool addMedicalSecretaryToDB(MedicalSecretary medicalSecretary);
        public Patient getPatientByPatientNo(String PatientNo);
        public bool getClinicalProfessionalByCPR(String CPR);
        public bool getMedicalSecretaryByCPR(String CPR);
    }
}
