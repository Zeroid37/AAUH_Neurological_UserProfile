using FrontEndAAUH.Models;
using System.Data.SqlClient;

namespace FrontEndAAUH.DataAccess {
    public class AnswerDB : AnswerDAO {
        private IConfiguration Configuration;
        private String? connectionString;

        public AnswerDB(IConfiguration configuration) {
            Configuration = configuration;
            connectionString = Configuration.GetConnectionString("DefaultConnection");
        }

        public bool addAnswerForPatientToDB(string patientNo, int answerID, DateTime answerUpdated) {

            int insertedRows = 0;

            string addAnswerToDBQuery = "insert into PatientAnswer (patientNo_FK, answerID_FK, answerUpdated)" +
                "values(@PATIENTNO_FK, @ANSWERID_FK, @ANSWERUPDATED)";

            using (SqlConnection con = new SqlConnection(connectionString)) {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted, "addPatientTransaction");
                using (SqlCommand cmd = new SqlCommand(addAnswerToDBQuery, con, transaction)) {
                    try {
                        cmd.CommandText = addAnswerToDBQuery;
                        cmd.Parameters.AddWithValue("PATIENTNO_FK", patientNo);
                        cmd.Parameters.AddWithValue("ANSWERID_FK", answerID);
                        cmd.Parameters.AddWithValue("ANSWERUPDATED", answerUpdated);

                        insertedRows = cmd.ExecuteNonQuery();
                        transaction.Commit();
                    } catch (SqlException e) {
                        transaction.Rollback();
                    }
                }
            }
            return insertedRows > 0;
        }
    }
}
