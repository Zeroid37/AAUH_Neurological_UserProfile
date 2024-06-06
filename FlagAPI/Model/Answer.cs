using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlagAPI.Model {
    public class Answer {
        public int id { get; set; }
        public string? answerText { get; set; }
        public bool isChosen { get; set; }
        public int answerValue { get; set; }

        public Answer() { }
        public Answer(string answerText, int answerValue) {
            this.answerText = answerText;
            this.answerValue = answerValue;
            this.isChosen = false;
        }

        public Answer(string answerText, int answerValue, bool isChosen) {
            this.answerText = answerText;
            this.answerValue = answerValue;
            this.isChosen = isChosen;
        }
    }
}
