using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackEndAAUH.Model;

namespace BackEndAAUH.DB {
    public interface FlagDAO {
        public Flag getFlagById(int id);
        public int addFlagToDB(Flag flag);
        public List<PatientAnswer> getNewPatientAnswers(DateOnly lastRead);
        public bool updatePatientFlagLevel(int patientNo, string flagID, int flagStage);
        public bool updatePatientAnswerTime(PatientAnswer patientAnswer, DateOnly dateNow);
    }
}
