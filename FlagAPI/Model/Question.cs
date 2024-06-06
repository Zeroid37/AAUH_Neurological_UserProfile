using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlagAPI.Model {
    public class Question {
        public string id { get; set; }
        public string questionDescription { get; set; }
        public List<Answer> answers { get; set; }

        public Question() { 
            this.answers = new List<Answer>();
        }

        public Question(string questionDescription, Flag flag) {
            this.answers = new List<Answer>();
            this.questionDescription = questionDescription;
        }

        public Question(string questionTitle, Flag flag, List<Answer> answers) {
            this.questionDescription = questionTitle;
            this.answers = answers;
        }

        public int findHighestPoints() {
            int highest = 0;
            foreach(Answer answer in answers) {
                if(answer.answerValue>highest) {
                    highest = answer.answerValue;
                }
            }
            return highest;
        }

        public void addAnswer(Answer answer) {
            answers.Add(answer);
        }
    }
}
