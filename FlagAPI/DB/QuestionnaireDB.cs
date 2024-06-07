using FlagAPI.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlagAPI.DB {
    public class QuestionnaireDB : QuestionnaireDAO {
        private IConfiguration Configuration;
        private String? connectionString;

        public QuestionnaireDB(IConfiguration configuration) {
            Configuration = configuration;
            connectionString = Configuration.GetConnectionString("DefaultConnection");
        }

        public bool addQuestionnaireToDB(Questionnaire questionnaire) {
            int insertedRowsNo = 0;
            int questionnaireId = -1;
            string addQuestionnaireToDBQueryString = "INSERT into Questionnaire(title) values(@TITLE); SELECT CAST(scope_identity() AS int)";

            QuestionDAO qdb = new QuestionDB(Configuration);

            using (SqlConnection con = new SqlConnection(connectionString)) {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted, "addQuestionnaireTransaction");
                using (SqlCommand cmd = new SqlCommand(addQuestionnaireToDBQueryString, con, transaction)) {
                    try {
                        cmd.Parameters.AddWithValue("TITLE", questionnaire.title);
                        questionnaireId = (int)cmd.ExecuteScalar();

                        foreach (Flag flag in questionnaire.flags) {
                            addToQuestionnaireFlagTable(questionnaireId, flag, con, transaction);
                        }
                        foreach (Question question in questionnaire.questions) {
                            qdb.addQuestionToDB(question, questionnaireId, con, transaction);
                        }

                        transaction.Commit();
                        insertedRowsNo = 1;
                    } catch (SqlException) {
                        transaction.Rollback();
                    }
                }
            }

            return (insertedRowsNo > 0);
        }

        private bool addToQuestionnaireFlagTable(int questionnaireId, Flag flag, SqlConnection con, SqlTransaction transaction) {
            int insertedRowsNo = 0;
            string addToQuestionnaireFlagTableQueryString = "INSERT into QuestionnaireFlag(questionnaireID_FK, flagID_FK) values(@QUESTIONNAIREFK, @FLAGFK)";

            using (SqlCommand cmd = new SqlCommand(addToQuestionnaireFlagTableQueryString, con, transaction)) {
                cmd.Parameters.AddWithValue("QUESTIONNAIREFK", questionnaireId);
                cmd.Parameters.AddWithValue("FLAGFK", flag.id);

                insertedRowsNo = cmd.ExecuteNonQuery();
            }
            return (insertedRowsNo > 0);
        }

        public Questionnaire getQuestionnaireByTitle(string title) {
            string getQuestionnaireIDByTitleQueryString = "SELECT id FROM Questionnaire WHERE title = @TITLE";
            string getFlagIDByQuestionnaireIDQueryString = "SELECT flagID_FK FROM QuestionnaireFlag WHERE questionnaireID_FK = @QUESTIONNAIREFK";

            FlagDAO flagdb = new FlagDB(Configuration);
            QuestionDAO questiondb = new QuestionDB(Configuration);
            Questionnaire questionnaire = new Questionnaire(title);

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmdQuestionnaire = new SqlCommand(getQuestionnaireIDByTitleQueryString, con))
            using (SqlCommand cmdQuestionnaireFlag = new SqlCommand(getFlagIDByQuestionnaireIDQueryString, con)) {
                con.Open();
                int questionnaireID = -1;

                cmdQuestionnaire.Parameters.AddWithValue("TITLE", title);
                SqlDataReader reader = cmdQuestionnaire.ExecuteReader();

                while (reader.Read()) {
                    questionnaireID = reader.GetInt32(reader.GetOrdinal("id"));
                }
                reader.Close();

                cmdQuestionnaireFlag.Parameters.AddWithValue("QUESTIONNAIREFK", questionnaireID);
                reader = cmdQuestionnaireFlag.ExecuteReader();
                while (reader.Read() && questionnaireID != -1) {
                    int flagID = reader.GetInt32(reader.GetOrdinal("flagID_FK"));
                    questionnaire.addFlag(flagdb.getFlagById(flagID));
                }

                List<Question> questions = questiondb.getQuestionsByQuestionnaireID(questionnaireID);
                foreach (Question question in questions) {
                    questionnaire.addQuestion(question);
                }
            }
            return questionnaire;
        }

        public Questionnaire getQuestionnaireByQuestionnaireID(int id) {
            string getQuestionnaireIDByTitleQueryString = "SELECT title FROM Questionnaire WHERE id = @QUESTIONNAIREID";
            string getFlagIDByQuestionnaireIDQueryString = "SELECT flagID_FK FROM QuestionnaireFlag WHERE questionnaireID_FK = @QUESTIONNAIREFK";

            FlagDAO flagdb = new FlagDB(Configuration);
            QuestionDAO questiondb = new QuestionDB(Configuration);
            Questionnaire questionnaire = new Questionnaire();

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmdQuestionnaire = new SqlCommand(getQuestionnaireIDByTitleQueryString, con))
            using (SqlCommand cmdQuestionnaireFlag = new SqlCommand(getFlagIDByQuestionnaireIDQueryString, con)) {
                con.Open();

                cmdQuestionnaire.Parameters.AddWithValue("QUESTIONNAIREID", id);
                SqlDataReader reader = cmdQuestionnaire.ExecuteReader();

                while (reader.Read()) {
                    questionnaire.title = reader.GetString(reader.GetOrdinal("title"));
                }
                reader.Close();

                cmdQuestionnaireFlag.Parameters.AddWithValue("QUESTIONNAIREFK", id);
                reader = cmdQuestionnaireFlag.ExecuteReader();
                while (reader.Read()) {
                    int flagID = reader.GetInt32(reader.GetOrdinal("flagID_FK"));
                    questionnaire.addFlag(flagdb.getFlagById(flagID));
                }

                List<Question> questions = questiondb.getQuestionsByQuestionnaireID(id);
                foreach (Question question in questions) {
                    questionnaire.addQuestion(question);
                }
            }
            return questionnaire;
        }
    }
}
