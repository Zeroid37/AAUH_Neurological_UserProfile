﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndAAUH.Model {
    public class Answer {
        public int id {  get; set; }
        public string answerText { get; set; }
        [BindProperty]
        public int answerValue { get; set; }

        public Answer(string answerText, int answerValue) {
            this.answerText = answerText;
            this.answerValue = answerValue;
        }

        public Answer() { }
    }
}
