
namespace BackEndAAUH.Model {
    public class ClinicProfessional : Person {
        public string employeeNo { get; set; }
        public string profession {  get; set; }
        public ClinicProfessional(string firstName, string lastName, string phoneNo, DateTime dateOfBirth, string employeeNo, string profession, string email, Address address) : base(firstName, lastName, phoneNo, dateOfBirth, email, address) {
            this.employeeNo = employeeNo;
            this.profession = profession;
        }
    }
}
