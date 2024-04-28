using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
namespace API_AAUH.Models {
    public class Patient : Person {
        public int Id { get; set; }
        public int patientNo { get; set; }
        public string CPR { get; set; }
        public List<Question> Questions { get; set; } //TODO: Delete when flag implemented

        public Patient() {

        }
        public Patient(string firstName, string lastName, string phoneNo, DateTime dateOfBirth, int patientNo, string email, Address address, string CPR) : base(firstName, lastName, phoneNo, dateOfBirth, email, address) {
            this.patientNo = patientNo;
            this.CPR = CPR;
        }
    }
}