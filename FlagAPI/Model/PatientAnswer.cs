using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlagAPI.Model {
    public class PatientAnswer {
        public string patientNo;
        public int answerID;
        public PatientAnswer(string patientNo, int answerID) {
            patientNo = patientNo;
            answerID = answerID;
        }
    }
}
