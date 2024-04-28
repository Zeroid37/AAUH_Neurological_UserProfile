using BackEndAAUH.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndAAUH.DB {
    public class QuestionDB : QuestionDAO {
        private IConfiguration Configuration;
        private String? connectionString;

        public QuestionDB() {
            connectionString = "data Source=127.0.0.1,1433; Database=AAUH; user=sa; password=secret;";
        }

        public bool addQuestionsToDB(List<Question> questions, int questionnaireID) {
            bool res = false;
            using (SqlConnection con = new SqlConnection(connectionString)) {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted, "addQuestionsTransaction"); //TODO1 ReadUncommited?
                try {
                    foreach (Question question in questions) {
                        addQuestionToDB(question, questionnaireID, con, transaction);
                    }
                    transaction.Commit();
                    res = true;
                } catch (SqlException) {
                    transaction.Rollback();
                }
                return res;
            }

        }

        public bool addQuestionToDB(Question question, int questionnaireID, SqlConnection con, SqlTransaction transaction) {
            int insertedRowsNo = 0;
            int questionID = -1;
            string addQuestionToDBQueryString = "INSERT into Question(questionDescription, flagID_FK, questionnaireID_FK)" +
                                                "values(@QUESTIONDESC, @FLAGFK, @QUESTIONNAIREFK); SELECT CAST(scope_identity() AS int)";

            using (SqlCommand cmd = new SqlCommand(addQuestionToDBQueryString, con, transaction)) {
                try {
                    cmd.Parameters.AddWithValue("QUESTIONDESC", question.questionDescription);
                    cmd.Parameters.AddWithValue("FLAGFK", question.flag.id);
                    cmd.Parameters.AddWithValue("QUESTIONNAIREFK", questionnaireID);

                    questionID = (int)cmd.ExecuteScalar();
                    foreach(Answer answer in question.answers) {
                        addAnswerToDB(answer, questionID, con, transaction);
                    }
                    insertedRowsNo = 1;
                } catch (SqlException) {
                    throw;
                }
            }

            return (insertedRowsNo > 0);
        }

        public List<Question> getQuestionsByQuestionnaireID(int questionnaireId) {
            throw new NotImplementedException();
        }

        public bool addAnswerToDB(Answer answer, int questionID) {
            int insertedNoRows = 0;
            string addAnswerToDBQueryString = "INSERT into Answer(answerText, isChosen, answerValue, questionID_FK)" +
                                              "values(@ANSWERTEXT, @ISCHOSEN, @ANSWERVALUE, @QUESTIONFK)";

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(addAnswerToDBQueryString, con)) {
                con.Open();
                cmd.Parameters.AddWithValue("ANSWERTEXT", answer.answerText);
                cmd.Parameters.AddWithValue("ISCHOSEN", answer.isChosen);
                cmd.Parameters.AddWithValue("ANSWERVALUE", answer.answerValue);
                cmd.Parameters.AddWithValue("QUESTIONFK", questionID);

                insertedNoRows = cmd.ExecuteNonQuery();
            }
            return (insertedNoRows > 0);
        }

        public bool addAnswerToDB(Answer answer, int questionID, SqlConnection con, SqlTransaction transaction) {
            int insertedNoRows = 0;
            string addAnswerToDBQueryString = "INSERT into Answer(answerText, isChosen, answerValue, questionID_FK)" +
                                              "values(@ANSWERTEXT, @ISCHOSEN, @ANSWERVALUE, @QUESTIONFK)";

            using (SqlCommand cmd = new SqlCommand(addAnswerToDBQueryString, con, transaction)) {
                try {
                    cmd.Parameters.AddWithValue("ANSWERTEXT", answer.answerText);
                    cmd.Parameters.AddWithValue("ISCHOSEN", answer.isChosen);
                    cmd.Parameters.AddWithValue("ANSWERVALUE", answer.answerValue);
                    cmd.Parameters.AddWithValue("QUESTIONFK", questionID);

                    insertedNoRows = cmd.ExecuteNonQuery();
                } catch (SqlException) {
                    throw;
                }
            }
            return (insertedNoRows > 0);
        }

        public List<Answer> getAnswersByQuestionID(int questionID) { 
            throw new NotImplementedException();
        }
    }
}
