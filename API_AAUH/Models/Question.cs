﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_AAUH.Model {
    public class Question {
        public string id { get; set; }
        public string questionDescription { get; set; }
        public Flag flag { get; set; }
        public List<Answer> answers { get; set; }
        public string chosenAnswerIndex { get; set; }
        public Question() { 
            this.answers = new List<Answer>();
        }
        public Question(string id, string questionTitle, Flag flag, List<Answer> answers) {
            this.answers = new List<Answer>();
            this.id = id;
            this.questionDescription = questionTitle;
            this.flag = flag;
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
    }
}
