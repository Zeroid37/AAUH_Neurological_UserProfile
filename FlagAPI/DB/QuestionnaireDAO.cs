using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlagAPI.Model;

namespace FlagAPI.DB {
    public interface QuestionnaireDAO {
        public Questionnaire getQuestionnaireByTitle(string title);
        public bool addQuestionnaireToDB(Questionnaire questionnaire);
    }
}
