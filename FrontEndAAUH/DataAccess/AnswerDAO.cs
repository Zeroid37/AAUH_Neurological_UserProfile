namespace FrontEndAAUH.DataAccess {
    public interface AnswerDAO {
        public bool addAnswerForPatientToDB(string patientNo, int answerID, DateTime answerUpdated);
    }
}
