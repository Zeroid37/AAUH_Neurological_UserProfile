using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlagAPI.Model {
    public class Questionnaire {
        public string title { get; set; }
        public List<Question> questions;
        public List<Flag> flags;

        public Questionnaire() {
            questions = new List<Question>();
            flags = new List<Flag>();
        }
        public Questionnaire(string title) {
            this.title = title;
            questions = new List<Question>();
            flags = new List<Flag>();
        }
        public bool addQuestion(Question question) {
            bool result = false;
            if (!questions.Contains(question)) {
                questions.Add(question);
                result = true;
            }
            return result;
        }
        public bool addFlag(Flag flag) {
            bool result = false;
            if (!flags.Contains(flag)) {
                flags.Add(flag);
                result = true;
            }
            return result;
        }
        public int getMaximumPoints() {
            int maximumSum = 0;
            foreach (Question question in questions) {
                maximumSum += question.findHighestPoints();
            }
            return maximumSum;
        }
        public Flag getFlag() {
            return flags[0];
        }
    }
}
