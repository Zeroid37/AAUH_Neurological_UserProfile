using BackEndAAUH.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndAAUH.DB {
    public class QuestionnaireDB : QuestionnaireDAO {
        private IConfiguration Configuration;
        private String? connectionString;

        public QuestionnaireDB() {
            connectionString = "data Source=127.0.0.1,1433; Database=AAUH; user=sa; password=secret;"; //TODO1 Fix connection string
        }

        public bool addQuestionnaireToDB(Questionnaire questionnaire) {
            int insertedRowsNo = 0;
            int questionnaireId = -1;
            string addQuestionnaireToDBQueryString = "INSERT into Questionnaire(title) values(@TITLE); SELECT CAST(scope_identity() AS int)";

            QuestionDAO qdb = new QuestionDB();

            using (SqlConnection con = new SqlConnection(connectionString)) {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted, "addQuestionnaireTransaction"); //TODO1 ReadUncommited?
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

        public bool addToUserProfileQuestionnaireDB(Questionnaire questionnaire) { 
            int insertedRowsNo = 0;
            string addToUserProfileQuestionnaireDBQueryString = "insert into UserProfileQuestionnaire(userProfileID_FK, questionnaireID_FK) values(@USERPROFILEFK, @QUESTIONNAIREFK)";
            string findQuestionnaireIDByTitleQueryString = "select id from Questionnaire where title = @TITLE";

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmdAdd = new SqlCommand(addToUserProfileQuestionnaireDBQueryString, con))
            using (SqlCommand cmdFind = new SqlCommand(findQuestionnaireIDByTitleQueryString, con)) {
                con.Open();
                cmdFind.Parameters.AddWithValue("TITLE", questionnaire.title);
                SqlDataReader find = cmdFind.ExecuteReader();

                find.Read();
                cmdAdd.Parameters.AddWithValue("USERPROFILEFK", 1); //TODO1 select UserProfile 
                cmdAdd.Parameters.AddWithValue("QUESTIONNAIREFK", find.GetInt32(find.GetOrdinal("id")));
                find.Close();

                insertedRowsNo = cmdAdd.ExecuteNonQuery();
            }


            return (insertedRowsNo > 0);
        }

        private bool addToQuestionnaireFlagTable(int questionnaireId, Flag flag, SqlConnection con, SqlTransaction transaction) {
            int insertedRowsNo = 0;
            string addToQuestionnaireFlagTableQueryString = "insert into QuestionnaireFlag(questionnaireID_FK, flagID_FK) values(@QUESTIONNAIREFK, @FLAGFK)";

            using(SqlCommand cmd = new SqlCommand(addToQuestionnaireFlagTableQueryString, con, transaction)) {
                cmd.Parameters.AddWithValue("QUESTIONNAIREFK", questionnaireId);
                cmd.Parameters.AddWithValue("FLAGFK", flag.id);

                insertedRowsNo = cmd.ExecuteNonQuery();
            }
            return (insertedRowsNo > 0);
        }

        public Questionnaire getQuestionnaireByID(int id) {
            throw new NotImplementedException();
        }
    }
}
