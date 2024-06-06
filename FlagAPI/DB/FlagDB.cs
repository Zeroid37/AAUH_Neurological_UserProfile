using FlagAPI.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlagAPI.DB {
    public class FlagDB : FlagDAO {
        private IConfiguration Configuration;
        private String? connectionString;

        public FlagDB() {
            connectionString = "data Source=127.0.0.1,1433; Database=AAUH; user=sa; password=secret;";
        }

        public int addFlagToDB(Flag flag) {
            int returnedId = -1;
            string addFlagToDBQueryString = "insert into Flag(flagName, flagDescription, flagRaised, flagAlertLevel)" +
                                            "values(@FLAGNAME, @FLAGDESC, @FLAGRAISED, @FLAGALERT); SELECT CAST(scope_identity() AS int)";

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(addFlagToDBQueryString, con)) {
                con.Open();
                cmd.Parameters.AddWithValue("FLAGNAME", flag.flagName);
                cmd.Parameters.AddWithValue("FLAGDESC", flag.flagDescription);
                cmd.Parameters.AddWithValue("FLAGRAISED", flag.flagRaised);
                cmd.Parameters.AddWithValue("FLAGALERT", flag.alertLevel);

                returnedId = (int)cmd.ExecuteScalar();
            }
            return returnedId;
        }

        public Flag getFlagById(int id) {
            string getFlagByIdQueryString = "SELECT flagName, flagDescription, flagRaised, flagAlertLevel from Flag where id = @ID";
            Flag flag = new Flag();

            using(SqlConnection con = new SqlConnection(connectionString))
            using(SqlCommand cmd = new SqlCommand(getFlagByIdQueryString, con)) {
                con.Open();
                cmd.Parameters.AddWithValue("ID", id);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    String flagName = reader.GetString(reader.GetOrdinal("flagName"));
                    String flagDescription = reader.GetString(reader.GetOrdinal("flagDescription"));
                    bool flagRaised = reader.GetBoolean(reader.GetOrdinal("flagRaised"));
                    String flagAlertLevel = reader.GetString(reader.GetOrdinal("flagAlertLevel"));

                    flag.flagName = flagName;
                    flag.flagDescription = flagDescription;
                    flag.flagRaised = flagRaised;
                    flag.alertLevel = flagAlertLevel;
                }
                reader.Close();
            }
            return flag;
        }

        public List<PatientAnswer> getNewPatientAnswers(DateOnly lastRead) {
            string getAllPatientAnswersQueryString = "SELECT patientNo_FK, answerID_FK from PatientAnswer where answerUpdated >= @LASTREAD";
            List<PatientAnswer> patientAnswers = new List<PatientAnswer>();

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(getAllPatientAnswersQueryString, con)) {
                con.Open();
                cmd.Parameters.AddWithValue("LASTREAD", lastRead.ToString());

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    string patientNo = reader.GetString(reader.GetOrdinal("patientNo_FK"));
                    int answerID = reader.GetInt32(reader.GetOrdinal("answerID_FK"));
                    PatientAnswer pa = new PatientAnswer(patientNo, answerID);
                    patientAnswers.Add(pa);
                }

            }

            return patientAnswers;
        }
    }
}
