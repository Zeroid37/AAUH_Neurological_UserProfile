using FlagAPI.DB;
using FlagAPI.Model;

namespace FlagAPI.Business {
    public class FlagLogic {
        public FlagLogic() { }

        public void processFlags(DateOnly lastRead) {
            FlagDAO flagdb = new FlagDB();
            QuestionDAO questiondb = new QuestionDB();

            List<PatientAnswer> newAnswers = getNewAnswers(lastRead);
            foreach (PatientAnswer answer in newAnswers) {
                Console.WriteLine(answer.patientNo, answer.answerID);
            }
        }

        public List<PatientAnswer> getNewAnswers(DateOnly lastRead) {
            FlagDAO flagdb = new FlagDB();
            List<PatientAnswer> newAnswers = flagdb.getNewPatientAnswers(lastRead);
            return newAnswers;
        }
    }
}
