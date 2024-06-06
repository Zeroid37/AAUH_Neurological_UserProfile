using BackEndAAUH.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndAAUH.DB {
    public class FlagDB : FlagDAO {
        private IConfiguration Configuration;
        private String? connectionString;

        public FlagDB() {
            connectionString = "Data Source=192.168.87.133,1433; Database=AAUH; user=sa; password=SecretPassword123;Trusted_Connection=False; Encrypt=false; MultipleActiveResultSets=true";
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

                    flag.id = "" + id;
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
                    int patientNo = reader.GetInt32(reader.GetOrdinal("patientNo_FK"));
                    int answerID = reader.GetInt32(reader.GetOrdinal("answerID_FK"));

                    PatientAnswer pa = new PatientAnswer();
                    pa.patientNo = patientNo;
                    pa.answerID = answerID;
                    patientAnswers.Add(pa);
                }
            }
            return patientAnswers;
        }

        public bool updatePatientFlagLevel(int patientNo, string flagID, int flagStage) {
            string updatePatientFlagQueryString = "UPDATE PatientFlag SET flagStage = @FLAGSTAGE WHERE patientNo_FK = @PATIENTNO AND flagID_FK = @FLAGID";
            int updatedRowsNo = 0;

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(updatePatientFlagQueryString, con)) {
                con.Open();
                cmd.Parameters.AddWithValue("FLAGSTAGE", flagStage);
                cmd.Parameters.AddWithValue("PATIENTNO", patientNo);
                cmd.Parameters.AddWithValue("FLAGID", flagID);
                updatedRowsNo = cmd.ExecuteNonQuery();
            }
            return (updatedRowsNo > 0);
        }

        public bool updatePatientAnswerTime(PatientAnswer patientAnswer, DateOnly dateNow) {
            string updatePatientAnswersTimeQueryString = "UPDATE PatientAnswer SET answerUpdated = @DATENOW " +
                                                         "WHERE patientNo_FK = @PATIENTNO AND answerID_FK = @ANSWERID";
            int updatedRowsNo = 0;
            DateTime dateTimeNow = dateNow.ToDateTime(TimeOnly.Parse("0:00 AM"));

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(updatePatientAnswersTimeQueryString, con)) {
                con.Open();
                cmd.Parameters.AddWithValue("PATIENTNO", patientAnswer.patientNo);
                cmd.Parameters.AddWithValue("ANSWERID", patientAnswer.answerID);
                cmd.Parameters.AddWithValue("DATENOW", dateTimeNow);
                updatedRowsNo = cmd.ExecuteNonQuery();
            }
            return (updatedRowsNo > 0);
        }
    }
}
