
namespace BackEndAAUH.Model {
    public class MedicalSecretary : Person {
        public string employeeNo { get; set; }
        public MedicalSecretary(string firstName, string lastName, string phoneNo, DateTime dateOfBirth, string employeeNo, string email, Address address) : base(firstName, lastName, phoneNo, dateOfBirth, email, address) {
            this.employeeNo = employeeNo;
        }
    }
}
