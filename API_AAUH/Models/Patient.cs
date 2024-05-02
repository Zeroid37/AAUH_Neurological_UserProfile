
using API_AAUH.Model;

namespace API_AAUH.Models {
    public class Patient : Person {
        public int id {  get; set; }
        public int patientNo { get; set; }
        public string cpr { get; set; }
        public List<Questionnaire> questionnaires { get; set; }

        public Patient() {
            this.questionnaires = new List<Questionnaire>();
        }
        public Patient(string firstName, string lastName, string phoneNo, DateTime dateOfBirth, int patientNo, string email, Address address, string cpr) : base(firstName, lastName, phoneNo, dateOfBirth, email, address) {
            this.patientNo = patientNo;
            this.cpr = cpr;
            this.questionnaires = new List<Questionnaire>();
        }
    }
}
