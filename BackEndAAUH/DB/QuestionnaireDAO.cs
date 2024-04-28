using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackEndAAUH.Model;

namespace BackEndAAUH.DB {
    public interface QuestionnaireDAO {
        public Questionnaire getQuestionnaireByID(int id);
        public bool addQuestionnaireToDB(Questionnaire questionnaire);
        public bool addToUserProfileQuestionnaireDB(Questionnaire questionnaire);
    }
}
