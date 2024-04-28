﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndAAUH.Model {
    public class Answer {
        public string answerText { get; set; }
        public bool isChosen { get; set; }
        public int answerValue { get; set; }

        public Answer(string answerText, int answerValue) {
            this.answerText = answerText;
            this.answerValue = answerValue;
            this.isChosen = false;
        }
    }
}
