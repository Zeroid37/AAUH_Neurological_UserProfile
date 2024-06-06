using FrontEndAAUH.Models;
using FrontEndAAUH.DataAccess;

namespace FrontEndAAUH.BusinessLogic {

    public class PatientLogic {
        private IConfiguration Configuration;

        public PatientLogic(IConfiguration configuration) {
            Configuration = configuration;
        }



        public bool addPatientToDB(Patient p) {
            PersonDAO pdb = new PersonDB(Configuration);
            return pdb.addPatientToDB(p);
        }

        public Patient getPatientByPatientNo(String patientNo) {
            PersonDAO pdb = new PersonDB(Configuration);
            return pdb.getPatientByPatientNo(patientNo);
        }

        public bool updatePatientAnswers(Dictionary<String, List<int>> patientAnswers) {
            bool res = false;


            //TODO: Logic to update

            return res;


        }


    }
}
