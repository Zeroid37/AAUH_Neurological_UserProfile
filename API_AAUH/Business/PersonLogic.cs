using API_AAUH.DB;
using API_AAUH.Models;

namespace API_AAUH.BusinessLogic {
    public class PersonLogic {
        private IConfiguration _configuration;
        private string? connectionString;
        public PersonLogic(IConfiguration configuration) {
            _configuration = configuration;
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public bool addPatientToDB(Patient patient) {
            PersonDAO pdao = new PersonDB(_configuration);
            bool res = pdao.addPatientToDB(patient);
            return res;
        }

        public Patient getPatientFromDB(String patientNo) {
            Patient patient = new Patient();
            PersonDAO pdao = new PersonDB(_configuration);
            patient = pdao.getPatientByPatientNo(patientNo);

            return patient;
        }
    }
}
