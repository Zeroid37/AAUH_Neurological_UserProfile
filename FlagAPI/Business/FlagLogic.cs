using FlagAPI.DB;
using FlagAPI.Model;

namespace FlagAPI.Business {
    public class FlagLogic {
        private IConfiguration Configuration;
        public double stage1 = 0.5;
        public double stage2 = 0.7;
        public double stage3 = 0.9;

        public FlagLogic(IConfiguration configuration) {
            Configuration = configuration;
        }

        public void processFlags(DateOnly lastRead) {
            Dictionary<int, Dictionary<int, int>> pointSums = new Dictionary<int, Dictionary<int, int>>();
            List<PatientAnswer> newAnswers = getNewAnswers(lastRead);

            pointSums = calculatePointSums(newAnswers, pointSums);
            updateUserFlags(pointSums);
            updateAnswersRead(newAnswers);
        }

        public List<PatientAnswer> getNewAnswers(DateOnly lastRead) {
            FlagDAO flagdb = new FlagDB(Configuration);
            List<PatientAnswer> newAnswers = flagdb.getNewPatientAnswers(lastRead);
            return newAnswers;
        }

        private Dictionary<int, Dictionary<int, int>> calculatePointSums(List<PatientAnswer> newAnswers, Dictionary<int, Dictionary<int, int>> pointSums) {
            QuestionnaireDAO questionnairedb = new QuestionnaireDB(Configuration);
            QuestionDAO questiondb = new QuestionDB(Configuration);

            foreach (PatientAnswer patientAnswer in newAnswers) {
                Answer answer = questiondb.getAnswerByAnswerID(patientAnswer.answerID);

                if (!pointSums.ContainsKey(patientAnswer.patientNo)) {
                    Dictionary<int, int> emptyDic = new Dictionary<int, int>();
                    pointSums.Add(patientAnswer.patientNo, emptyDic);
                }
                int questionid = questiondb.getQuestionIDByAnswerID(patientAnswer.answerID);
                int questionnaireid = questiondb.getQuestionnaireIDByQuestionID(questionid);

                if (!pointSums[patientAnswer.patientNo].ContainsKey(questionnaireid)) {
                    pointSums[patientAnswer.patientNo].Add(questionnaireid, answer.answerValue);
                }
                else {
                    pointSums[patientAnswer.patientNo][questionnaireid] += answer.answerValue;
                }
            }
            return pointSums;
        }

        private void updateUserFlags(Dictionary<int, Dictionary<int, int>> pointSums) {
            QuestionnaireDAO questionnairedb = new QuestionnaireDB(Configuration);
            FlagDAO flagdb = new FlagDB(Configuration);
            Questionnaire currQuestionnaire = new Questionnaire();

            foreach (KeyValuePair<int, Dictionary<int, int>> kvp in pointSums) {
                foreach (KeyValuePair<int, int> kvp2 in kvp.Value) {
                    currQuestionnaire = questionnairedb.getQuestionnaireByQuestionnaireID(kvp2.Key);
                    int maxPoints = currQuestionnaire.getMaximumPoints();
                    double patientPointsPercent = (double)kvp2.Value / (double)maxPoints;
                    string flagID = currQuestionnaire.getFlag().id;

                    if (patientPointsPercent >= stage1 && patientPointsPercent < stage2) {
                        flagdb.updatePatientFlagLevel(kvp.Key, flagID, 1);
                    }
                    else if (patientPointsPercent >= stage2 && patientPointsPercent < stage3) {
                        flagdb.updatePatientFlagLevel(kvp.Key, flagID, 2);
                    }
                    else if (patientPointsPercent >= stage3) {
                        flagdb.updatePatientFlagLevel(kvp.Key, flagID, 3);
                    }
                    else {
                        flagdb.updatePatientFlagLevel(kvp.Key, flagID, 0);
                    }
                }
            }
        }

        private void updateAnswersRead(List<PatientAnswer> patientAnswers) {
            FlagDAO flagdb = new FlagDB(Configuration);
            DateOnly dateNow = DateOnly.FromDateTime(DateTime.Now);
            foreach (PatientAnswer answer in patientAnswers) {
                flagdb.updatePatientAnswerTime(answer, dateNow);
            }
        }
    }
}
