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
        public Dictionary<Flag, int> getAllFlagsPointSum() {
            Dictionary<Flag, int> pointSums = new Dictionary<Flag, int>();
            foreach(Flag flag in flags) {
                int flagSum = 0;
                foreach(Question question in questions) {
                    if(question.flag == flag) {
                        foreach(Answer answer in question.answers) {
                            if(answer.isChosen) {
                                flagSum += answer.answerValue;
                            }
                        }
                    }
                }
                pointSums.Add(flag, flagSum);
            }
            return pointSums;
        }
        public Dictionary<Flag, int> getAllFlagsHighestPoints() {
            Dictionary<Flag, int> highestPoints = new Dictionary<Flag, int>();
            foreach(Flag flag in flags) {
                int flagSum = 0;
                foreach (Question question in questions) {
                    if(question.flag == flag) {
                        flagSum += question.findHighestPoints();
                    }
                }
                highestPoints.Add(flag, flagSum);
            }
            return highestPoints;
        }
        public List<Flag> setAllAlertLevel() {
            Dictionary<Flag, int> currFlagsPoints = getAllFlagsPointSum();

            foreach(Flag flag in flags) {
                double currPoints = currFlagsPoints[flag];
                setAlertLevel(flag, currPoints);
            }
            return flags;
        }
        public void setAlertLevel(Flag flag, double currPoints) {
            Dictionary<Flag, int> highestFlagsPoints = getAllFlagsHighestPoints();

            double stage1 = highestFlagsPoints[flag] * 0.5;
            double stage2 = highestFlagsPoints[flag] * 0.7;
            double stage3 = highestFlagsPoints[flag] * 0.9;

            Console.WriteLine("Curr: " + currPoints);
            Console.WriteLine("S1: " + stage1);
            Console.WriteLine("S2: " + stage2);
            Console.WriteLine("S3: " + stage3);

            if (currPoints >= stage1 && currPoints < stage2) { 
                flag.alertLevel = "1";
            } else if(currPoints >= stage2 && currPoints < stage3) {
                flag.alertLevel = "2";
            } else if(currPoints >= stage3) {
                flag.alertLevel = "3";
            } else {
                flag.alertLevel = "0";
            }
        }
    }
}
