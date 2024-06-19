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

        public string getPatientNoByEmail(String email) {
            PersonDAO pdb = new PersonDB(Configuration);
            return pdb.getPatientNoByEmail(email);
        }


        public bool updatePatientAnswers(string patientNo, List<int> answerIds) {
            bool res = false;
            AnswerDAO adb = new AnswerDB(Configuration);
            DateTime dateStamp = new DateTime(2024, 10, 06); //YYYY-MM-DD

            foreach (int x in answerIds) {
                res = adb.addAnswerForPatientToDB(patientNo, x, dateStamp);
            }

            return res;
        }
    }
}
