using FlagAPI.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlagAPI.DB {
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
            string getQuestionsByQuestionnaireIDQueryString = "SELECT id, questionDescription, flagID_FK FROM Question " +
                                                              "WHERE questionnaireID_FK = @QUESTIONNAIREFK";
            FlagDAO flagdb = new FlagDB();
            List<Question> questions = new List<Question>();

            using(SqlConnection con = new SqlConnection(connectionString))
            using(SqlCommand cmd = new SqlCommand(getQuestionsByQuestionnaireIDQueryString, con)) {
                con.Open();
                cmd.Parameters.AddWithValue("QUESTIONNAIREFK", questionnaireId);
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read()) {
                    int questionID = reader.GetInt32(reader.GetOrdinal("id"));
                    string questionDescription = reader.GetString(reader.GetOrdinal("questionDescription"));
                    int flagID = reader.GetInt32(reader.GetOrdinal("flagID_FK"));
                    Flag flag = flagdb.getFlagById(flagID);
                    List<Answer> answers = getAnswersByQuestionID(questionID);

                    Question question = new Question();
                    question.id = questionID.ToString();
                    question.questionDescription = questionDescription;
                    question.flag = flag;
                    foreach(Answer answer in answers) { 
                        question.addAnswer(answer);
                    }
                    questions.Add(question);
                }
            }
            return questions;
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
            string getAnswersByQuestionIDQueryString = "SELECT answerText, isChosen, answerValue FROM " +
                                                       "Answer WHERE questionID_FK = @QUESTIONFK";
            List<Answer> answers = new List<Answer>();

            using(SqlConnection con = new SqlConnection(connectionString))
            using(SqlCommand cmd = new SqlCommand(getAnswersByQuestionIDQueryString, con)) {
                con.Open();
                cmd.Parameters.AddWithValue("QUESTIONFK", questionID);
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read()) {
                    string answerText = reader.GetString(reader.GetOrdinal("answerText"));
                    bool isChosen = reader.GetBoolean(reader.GetOrdinal("isChosen"));
                    int answerValue = reader.GetInt32(reader.GetOrdinal("answerValue"));

                    Answer answer = new Answer(answerText, answerValue, isChosen);
                    answers.Add(answer);
                }
            }
            return answers;
        }
    }
}
