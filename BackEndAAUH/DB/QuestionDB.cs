using BackEndAAUH.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndAAUH.DB {
    public class QuestionDB : QuestionDAO {
        private IConfiguration Configuration;
        private String? connectionString;

        public QuestionDB() {
            connectionString = "Data Source=192.168.87.133,1433; Database=AAUH; user=sa; password=SecretPassword123;Trusted_Connection=False; Encrypt=false; MultipleActiveResultSets=true";
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
            string addQuestionToDBQueryString = "INSERT into Question(questionDescription, questionnaireID_FK)" +
                                                "values(@QUESTIONDESC, @QUESTIONNAIREFK); SELECT CAST(scope_identity() AS int)";

            using (SqlCommand cmd = new SqlCommand(addQuestionToDBQueryString, con, transaction)) {
                try {
                    cmd.Parameters.AddWithValue("QUESTIONDESC", question.questionDescription);
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
            string getQuestionsByQuestionnaireIDQueryString = "SELECT id, questionDescription FROM Question " +
                                                              "WHERE questionnaireID_FK = @QUESTIONNAIREFK";
            List<Question> questions = new List<Question>();

            using(SqlConnection con = new SqlConnection(connectionString))
            using(SqlCommand cmd = new SqlCommand(getQuestionsByQuestionnaireIDQueryString, con)) {
                con.Open();
                cmd.Parameters.AddWithValue("QUESTIONNAIREFK", questionnaireId);
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read()) {
                    int questionID = reader.GetInt32(reader.GetOrdinal("id"));
                    string questionDescription = reader.GetString(reader.GetOrdinal("questionDescription"));
                    List<Answer> answers = getAnswersByQuestionID(questionID);

                    Question question = new Question();
                    question.id = questionID.ToString();
                    question.questionDescription = questionDescription;
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
                                              "values(@ANSWERTEXT, @ANSWERVALUE, @QUESTIONFK)";

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(addAnswerToDBQueryString, con)) {
                con.Open();
                cmd.Parameters.AddWithValue("ANSWERTEXT", answer.answerText);
                cmd.Parameters.AddWithValue("ANSWERVALUE", answer.answerValue);
                cmd.Parameters.AddWithValue("QUESTIONFK", questionID);

                insertedNoRows = cmd.ExecuteNonQuery();
            }
            return (insertedNoRows > 0);
        }

        public bool addAnswerToDB(Answer answer, int questionID, SqlConnection con, SqlTransaction transaction) {
            int insertedNoRows = 0;
            string addAnswerToDBQueryString = "INSERT into Answer(answerText, isChosen, answerValue, questionID_FK)" +
                                              "values(@ANSWERTEXT, @ANSWERVALUE, @QUESTIONFK)";

            using (SqlCommand cmd = new SqlCommand(addAnswerToDBQueryString, con, transaction)) {
                try {
                    cmd.Parameters.AddWithValue("ANSWERTEXT", answer.answerText);
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
            string getAnswersByQuestionIDQueryString = "SELECT answerText, answerValue FROM " +
                                                       "Answer WHERE questionID_FK = @QUESTIONFK";
            List<Answer> answers = new List<Answer>();

            using(SqlConnection con = new SqlConnection(connectionString))
            using(SqlCommand cmd = new SqlCommand(getAnswersByQuestionIDQueryString, con)) {
                con.Open();
                cmd.Parameters.AddWithValue("QUESTIONFK", questionID);
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read()) {
                    string answerText = reader.GetString(reader.GetOrdinal("answerText"));
                    int answerValue = reader.GetInt32(reader.GetOrdinal("answerValue"));

                    Answer answer = new Answer(answerText, answerValue);
                    answers.Add(answer);
                }
            }
            return answers;
        }

        public int getQuestionnaireIDByQuestionID(int questionid) {
            string getQuestionnaireIDByQuestionIDQueryString = "SELECT questionnaireID_FK FROM Question where id = @QUESTIONID";
            int result = -1;

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(getQuestionnaireIDByQuestionIDQueryString, con))
             {
                con.Open();
                cmd.Parameters.AddWithValue("QUESTIONID", questionid);
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read()) {
                    result = reader.GetInt32(reader.GetOrdinal("questionnaireID_FK"));
                }
            }
            return result;
        }

        public int getQuestionIDByAnswerID(int answerID) {
            string getQuestionIDByAnswerIDQueryString = "SELECT questionID_FK from Answer where id = @ANSWERID";
            int result = -1;

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(getQuestionIDByAnswerIDQueryString, con)) {
                con.Open();
                cmd.Parameters.AddWithValue("ANSWERID", answerID);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    result = reader.GetInt32(reader.GetOrdinal("questionID_FK"));
                }
            }
            return result;
        }

        public Answer getAnswerByAnswerID(int answerID) {
            string getAnswerByAnswerIDQueryString = "SELECT answerText, answerValue from Answer where id = @ANSWERID";
            Answer answer = new Answer();

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(getAnswerByAnswerIDQueryString, con)) {
                con.Open();
                cmd.Parameters.AddWithValue("ANSWERID", answerID);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    string answerText = reader.GetString(reader.GetOrdinal("answerText"));
                    int answerValue = reader.GetInt32(reader.GetOrdinal("answerValue"));

                    answer.answerText = answerText;
                    answer.answerValue = answerValue;
                }
                return answer;
            }
        }
    }
}
