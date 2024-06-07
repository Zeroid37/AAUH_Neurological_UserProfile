using FrontEndAAUH.DB;
using FrontEndAAUH.Model;
using FrontEndAAUH.Models;
using System.Data.SqlClient;

namespace FrontEndAAUH.DataAccess {
    public class PersonDB : PersonDAO {


        private IConfiguration Configuration;
        private String? connectionString;

        public PersonDB(IConfiguration configuration) {
            Configuration = configuration;
            connectionString = Configuration.GetConnectionString("DefaultConnection");
        }

        private bool addPersonToDB(Person person, SqlConnection con, SqlTransaction transaction) {

            int insertedRows = 0;
            string addPersonToDBQuery = "insert into Person(firstName, lastName, addressId_FK, phone, dateOfBirth, email)" +
                "values(@FIRSTNAME, @LASTNAME, @ADDRESSID_FK, @PHONE, @DATEOFBIRTH, @EMAIL)";

            using (SqlCommand cmd = new SqlCommand(addPersonToDBQuery, con, transaction)) {
                try {
                    int addressId = addAddressToDB(person.address, con, transaction);
                    cmd.CommandText = addPersonToDBQuery;

                    cmd.Parameters.AddWithValue("FIRSTNAME", person.firstName);
                    cmd.Parameters.AddWithValue("LASTNAME", person.lastName);
                    cmd.Parameters.AddWithValue("ADDRESSID_FK", addressId);
                    cmd.Parameters.AddWithValue("PHONE", person.phoneNo);
                    cmd.Parameters.AddWithValue("DATEOFBIRTH", person.dateOfBirth);
                    cmd.Parameters.AddWithValue("EMAIL", person.email);

                    insertedRows = cmd.ExecuteNonQuery();
                } catch (SqlException) {
                    throw;
                }
            }
            return(insertedRows > 0);
        }

        private int addAddressToDB(Address address, SqlConnection con, SqlTransaction transaction) {
            int addressId = -1;

            string queryStringZipCity = "insert into ZipCity(zip, city) values(@ZIP, @CITY)";
            string queryStringAddress = "insert into Address(street, houseNo, zip_FK) values (@STREET, @HOUSENO, @ZIP_FK); SELECT CAST(scope_identity() AS int)";

            using (SqlCommand cmd = new SqlCommand(queryStringZipCity, con, transaction)) {
                try {
                    cmd.Parameters.AddWithValue("ZIP", address.zip);
                    cmd.Parameters.AddWithValue("CITY", address.city);
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = queryStringAddress;
                    cmd.Parameters.AddWithValue("STREET", address.zip);
                    cmd.Parameters.AddWithValue("HOUSENO", address.houseNo);
                    cmd.Parameters.AddWithValue("ZIP_FK", address.zip);

                    addressId = (int)cmd.ExecuteScalar();
                } catch (SqlException) {
                    throw;
                }
            }
            return addressId;
        }

        public bool addPatientToDB(Patient patient) {
            int insertedRows = 0;
            string addPatientToDBQuery = "insert into Patient(email_FK, cpr) values(@EMAIL_FK, @CPR)";

            using (SqlConnection con = new SqlConnection(connectionString)) {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted, "addPatientTransaction");
                using(SqlCommand cmd = new SqlCommand(addPatientToDBQuery, con, transaction)) {
                    try {
                        addPersonToDB(patient, con, transaction);
                        cmd.CommandText = addPatientToDBQuery;
                        cmd.Parameters.AddWithValue("EMAIL_FK", patient.email);
                        cmd.Parameters.AddWithValue("CPR", patient.cpr);

                        insertedRows = cmd.ExecuteNonQuery();
                        transaction.Commit();
                    } catch (SqlException e) {
                        transaction.Rollback();
                    }
                }
            }
            return insertedRows > 0;
        }

        public bool addClinicalProfessionalToDB(ClinicProfessional clinicProfessional) {
            throw new NotImplementedException();
        }

        public bool addMedicalSecretaryToDB(MedicalSecretary medicalSecretary) {
            throw new NotImplementedException();
        }
  
        public Patient getPatientByPatientNo(string patientNo) {
            string getPatientByCPRQuery = "Select email_fk, cpr from Patient where patientNo=@PATIENTNO";
            string getPersonByEmailQuery = "Select firstName, lastName, addressId_FK, phone, dateOfBirth from Person where email=@EMAIL";
            string getQuestionnaireIdByPatientNoQuery = "select questionnaireID_FK from PatientQuestionnaire where patientNo_FK=@PATIENTNO";
            Patient patient = new Patient();
            patient.patientNo = Int32.Parse(patientNo);
            List<int> idList = new List<int>();
            QuestionnaireDAO qnDB = new QuestionnaireDB(Configuration);
            
            try {
                using (SqlConnection con = new SqlConnection(connectionString))
                using (SqlCommand cmdPatient = new SqlCommand(getPatientByCPRQuery, con))
                using (SqlCommand cmdQuestionnaire = new SqlCommand(getQuestionnaireIdByPatientNoQuery, con))
                using (SqlCommand cmdPerson = new SqlCommand(getPersonByEmailQuery, con)) {
                    con.Open();
                    cmdPatient.Parameters.AddWithValue("PATIENTNO", patientNo);
                    SqlDataReader readerPatient = cmdPatient.ExecuteReader();
                    while (readerPatient.Read()) {
                        patient.cpr = readerPatient.GetString(readerPatient.GetOrdinal("cpr"));
                        patient.email = readerPatient.GetString(readerPatient.GetOrdinal("email_FK"));
                    }
                    if (patient.cpr != null) {
                        readerPatient.Close();
                        cmdPerson.Parameters.AddWithValue("EMAIL", patient.email);
                        SqlDataReader readerPerson = cmdPerson.ExecuteReader();
                        while (readerPerson.Read()) {
                            string firstName = readerPerson.GetString(readerPerson.GetOrdinal("firstName"));
                            string lastName = readerPerson.GetString(readerPerson.GetOrdinal("lastName"));
                            Address address = getAddressByAdressId(readerPerson.GetInt32(readerPerson.GetOrdinal("addressId_FK")));
                            string phoneNo = readerPerson.GetString(readerPerson.GetOrdinal("phone"));
                            DateTime dateOfBirth = readerPerson.GetDateTime(readerPerson.GetOrdinal("dateOfBirth"));

                            patient.firstName = firstName;
                            patient.lastName = lastName;
                            patient.phoneNo = phoneNo;
                            patient.address = address;
                            patient.dateOfBirth = dateOfBirth;
                        }
                        cmdQuestionnaire.Parameters.AddWithValue("PATIENTNO", patientNo);
                        SqlDataReader readerQuestionnaire = cmdQuestionnaire.ExecuteReader();
                        while (readerQuestionnaire.Read()) {
                            int qnId = readerQuestionnaire.GetInt32(readerQuestionnaire.GetOrdinal("questionnaireID_FK"));
                            idList.Add(qnId);
                        }
                        foreach (int qnId in idList) {
                            patient.questionnaires.Add(qnDB.getQuestionnaireByQuestionnaireID(qnId));
                        }

                    } else {
                        patient = null;
                    }
                }
            } catch (SqlException e) {
                throw;
            }
            return patient;
        }

        public bool getClinicalProfessionalByCPR(string CPR) {
            throw new NotImplementedException();
        }

        public bool getMedicalSecretaryByCPR(string CPR) {
            throw new NotImplementedException();
        }

        private Address getAddressByAdressId(int id) {
            Address a = new Address();

            string getAddressQuery = "select street, houseNo, zip_FK from Address where id = @ID";
            string getZipCityQuery = "select zip, city from ZipCity where zip = @ZIP";

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(getAddressQuery, con))
            using (SqlCommand cmd2 = new SqlCommand(getZipCityQuery, con)) {
                con.Open();
                cmd.Parameters.AddWithValue("ID", id);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    a.street = reader.GetString(reader.GetOrdinal("street"));
                    a.houseNo = reader.GetString(reader.GetOrdinal("houseNo"));
                    a.zip = reader.GetString(reader.GetOrdinal("zip_FK"));
                }
                reader.Close();
                cmd2.Parameters.AddWithValue("ZIP", a.zip);
                SqlDataReader reader2 = cmd2.ExecuteReader();
                while (reader2.Read()) {
                    a.city = reader2.GetString(reader2.GetOrdinal("city"));
                }
            }
            return a;
        }

    }
}
