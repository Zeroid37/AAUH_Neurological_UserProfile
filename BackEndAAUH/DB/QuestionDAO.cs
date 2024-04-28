using BackEndAAUH.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndAAUH.DB {
    public interface QuestionDAO {
        public bool addQuestionsToDB(List<Question> questions, int questionnaireID);
        public bool addQuestionToDB(Question question, int questionnaireID, SqlConnection con, SqlTransaction transaction);
        public List<Question> getQuestionsByQuestionnaireID(int questionnaireId);
    }
}
