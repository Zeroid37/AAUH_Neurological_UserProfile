namespace BackEndAAUH.Model {
    public class Person {
        public string firstName {  get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public Address address { get; set; }
        public string phoneNo { get; set; }
        public DateTime dateOfBirth { get; set; }

        public Person() {

        }
        public Person(string firstName, string lastName, string phoneNo, DateTime dateOfBirth, string email, Address address) {
            this.firstName = firstName;
            this.lastName = lastName;
            this.phoneNo = phoneNo;
            this.dateOfBirth = dateOfBirth;
            this.email = email;
            this.address = address;
        }

    }
}
