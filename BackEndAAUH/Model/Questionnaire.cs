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
        //public void setAlertLevel(Flag flag, double currPoints) {
        //    Dictionary<Flag, int> highestFlagsPoints = getAllFlagsHighestPoints();

        //    double stage1 = highestFlagsPoints[flag] * 0.5;
        //    double stage2 = highestFlagsPoints[flag] * 0.7;
        //    double stage3 = highestFlagsPoints[flag] * 0.9;

        //    Console.WriteLine("Curr: " + currPoints);
        //    Console.WriteLine("S1: " + stage1);
        //    Console.WriteLine("S2: " + stage2);
        //    Console.WriteLine("S3: " + stage3);

        //    if (currPoints >= stage1 && currPoints < stage2) { 
        //        flag.alertLevel = "1";
        //    } else if(currPoints >= stage2 && currPoints < stage3) {
        //        flag.alertLevel = "2";
        //    } else if(currPoints >= stage3) {
        //        flag.alertLevel = "3";
        //    } else {
        //        flag.alertLevel = "0";
        //    }
        //}
    }
}
