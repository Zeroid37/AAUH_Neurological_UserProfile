using FrontEndAAUH.Model;

namespace FrontEndAAUH.Models {
    public class QuestionnaireModel {

        public Patient patient { get; set; }
        public List<Questionnaire> questionnaires { get; set; }
        public Question Question { get; set; }
        public Answer answer { get; set; }


        public QuestionnaireModel() { 
        }
    }
}
