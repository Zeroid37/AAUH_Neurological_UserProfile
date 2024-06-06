using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndAAUH.Model {
    public class Questionnaire {
        public string title { get; set; }
        public List<Question> questions { get; set; }
        public List<Flag> flags;
        public bool isChosen { get; set; }

        public Questionnaire(string title) {
            this.title = title;
            questions = new List<Question>();
            flags = new List<Flag>();
        }


        public Questionnaire() { 
        questions = new List<Question>();
        }
        public bool addQuestion(Question question) {
            bool result = false;
            if(!questions.Contains(question)) {
                questions.Add(question);
                result = true;
            }
            return result;
        }
        public bool addFlag(Flag flag) { 
            bool result = false;
            if(!flags.Contains(flag)) { 
                flags.Add(flag);
                result = true;
            }
            return result;
        }
    }
}
