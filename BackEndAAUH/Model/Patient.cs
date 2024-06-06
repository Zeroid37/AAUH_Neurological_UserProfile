
using BackEndAAUH.Model;

namespace BackEndAAUH.Model {
    public class Patient : Person {
        public int id {  get; set; }
        public int patientNo { get; set; }
        public string cpr { get; set; }
        public List<Questionnaire> questionnaires { get; set; }

        public Patient() {

        }
        public Patient(string firstName, string lastName, string phoneNo, DateTime dateOfBirth, int patientNo, string email, Address address, string cpr) : base(firstName, lastName, phoneNo, dateOfBirth, email, address) {
            this.patientNo = patientNo;
            this.cpr = cpr;
            this.questionnaires = new List<Questionnaire>();
        }
    }
}
