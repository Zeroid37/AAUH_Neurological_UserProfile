using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackEndAAUH.Model;

namespace BackEndAAUH.DB {
    public interface QuestionnaireDAO {
        public Questionnaire getQuestionnaireByTitle(string title);
        public bool addQuestionnaireToDB(Questionnaire questionnaire);
        public Questionnaire getQuestionnaireByQuestionnaireID(int id);
    }
}
