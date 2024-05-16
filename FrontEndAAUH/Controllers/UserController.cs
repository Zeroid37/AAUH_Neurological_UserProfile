using FrontEndAAUH.BusinessLogic;
using FrontEndAAUH.Model;
using FrontEndAAUH.Models;

using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;

namespace FrontEndAAUH.Controllers {
    public class UserController : Controller {
        
        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration) {
            _configuration = configuration;
        }
        public IActionResult Index() {
            return View();
        }

        public async Task<IActionResult> UserProfileAsync(string id) {

            Patient patient = new Patient();
            PatientLogic pLogic = new PatientLogic(_configuration);

            try {
                patient = pLogic.getPatientByPatientNo(id);
            } catch (Exception e) {
                patient = null;
            }

            if (patient != null) {
                patient = addQuestionnaireData(patient);

                return View(patient);
            } else {
                return View("UserNotFound");
            }
        }

        [HttpPost]
        public ActionResult Submit([FromForm]Patient p) {
            PatientLogic pLogic = new PatientLogic(_configuration);
            string patientNo = p.patientNo.ToString();
            List<int> chosenAnswerIds = new List<int>();
            
            Dictionary<string, List<int>> PatientAndAnswerIds = new Dictionary<String, List<int>>();


            foreach (Questionnaire qn in p.questionnaires) {
                foreach (Question q in qn.questions) {
                    chosenAnswerIds.Add(q.answers[Int32.Parse(q.chosenAnswerIndex)].id);
                }
            }
            PatientAndAnswerIds.Add(patientNo, chosenAnswerIds);

            bool success = pLogic.updatePatientAnswers(PatientAndAnswerIds);

            if(success) {
                return RedirectToAction("UserProfile", new { id = p.patientNo });
            }
            else {
                return View("SomethingWentWrong");
            }
        }
        

        public async Task<IActionResult> EditUserQuestionnaireAsync(string id) {

            Patient patient = new Patient();
            QuestionnaireModel qnm = new QuestionnaireModel();
            PatientLogic pLogic = new PatientLogic(_configuration);
            try {
                patient = pLogic.getPatientByPatientNo(id);
            } catch (Exception e) {
                patient = null;
            }
            if (patient != null) {
                patient = addQuestionnaireData(patient);
                qnm.patient = patient;

                return View(qnm);
            } else {
                return View("UserNotFound");
            }
        }



        public async Task<IActionResult> EditUserCustomQuestionsAsync(string id) {
            Patient patient = new Patient();
            CustomQuestionModel qqm = new CustomQuestionModel();
            PatientLogic pLogic = new PatientLogic(_configuration);

            try {
                patient = pLogic.getPatientByPatientNo(id);
            } catch (Exception e) {
                patient = null;
            }
            if (patient != null) {
                patient = addQuestionnaireData(patient);

                bool foundCustomQuestionnaire = false;

                foreach (Questionnaire qn in patient.questionnaires) {
                    if (qn.title.Equals("Custom")) {
                        foundCustomQuestionnaire = true;
                        break;
                    }
                }
                if (!foundCustomQuestionnaire) {
                    Questionnaire custom = new Questionnaire("Custom");
                    patient.questionnaires.Add(custom);
                }



                qqm.patient = patient;
                return View(qqm);
            } else {
                return View("UserNotFound");
            }
        }


        public ActionResult editQuestion([FromForm] CustomQuestionModel cqm, [FromForm]IFormCollection frm) {
            string index = frm["questionIndex"].ToString();

            return RedirectToAction("EditUserCustomQuestions", new {id = cqm.patient.patientNo});
        }

        
        public ActionResult editCurrentQuestionnaire([FromForm] QuestionnaireModel qnm) {

            //TODO: Fjern questionnaire fra DB logik

            return RedirectToAction("EditUserQuestionnaire", new {id = qnm.patient.patientNo});
        }

        public ActionResult addQuestionnaire([FromForm] QuestionnaireModel qnm) {

            //TODO: Fjern questionnaire fra DB logik

            return RedirectToAction("EditUserQuestionnaire", new { id = qnm.patient.patientNo });
        }

        public ActionResult addCutomQuestion([FromForm] QuestionnaireModel qnm) {

            //TODO: Fjern questionnaire fra DB logik

            return RedirectToAction("EditUserQuestionnaire", new { id = qnm.patient.patientNo });
        }






        private Patient addQuestionnaireData (Patient patient) {
            //Answers for question 1
            Answer answer1 = new Answer("Answer 1", 1);
            Answer answer2 = new Answer("Answer 2", 2);
            answer2.isChosen = true;
            List<Answer> answerList1 = new List<Answer>();
            answerList1.Add(answer1);
            answerList1.Add(answer2);

            //Answers for question 2
            Answer answer3 = new Answer("Answer 3", 3);
            Answer answer4 = new Answer("Answer 4", 4);
            List<Answer> answerList2 = new List<Answer>();
            answerList2.Add(answer3);
            answerList2.Add(answer4);

            //Answers for question 3
            Answer answer5 = new Answer("Answer 5", 5);
            Answer answer6 = new Answer("Answer 6", 6);
            Answer answer7 = new Answer("Answer 7", 7);
            List<Answer> answerList3 = new List<Answer>();
            answerList3.Add(answer5);
            answerList3.Add(answer6);
            answerList3.Add(answer7);


            //Answers for question 4
            Answer answer8 = new Answer("Answer 5", 5);
            Answer answer9 = new Answer("Answer 6", 6);
            Answer answer10 = new Answer("Answer 7", 7);
            List<Answer> answerList4 = new List<Answer>();
            answerList4.Add(answer8);
            answerList4.Add(answer9);
            answerList4.Add(answer10);


            //Answers for question 5
            Answer answer11 = new Answer("Answer 8", 5);
            Answer answer12 = new Answer("Answer 9", 6);
            Answer answer13 = new Answer("Answer 10", 7);
            List<Answer> answerList5 = new List<Answer>();
            answerList5.Add(answer11);
            answerList5.Add(answer12);
            answerList5.Add(answer13);


            //Answers for question 6
            Answer answer14 = new Answer("Answer 11", 5);
            Answer answer15 = new Answer("Answer 12", 6);
            Answer answer16 = new Answer("Answer 13", 7);
            List<Answer> answerList6 = new List<Answer>();
            answerList6.Add(answer14);
            answerList6.Add(answer15);
            answerList6.Add(answer16);


            //Answers for question 7
            Answer answer17 = new Answer("Answer 14", 5);
            Answer answer18 = new Answer("Answer 15", 6);
            Answer answer19 = new Answer("Answer 16", 7);
            List<Answer> answerList7 = new List<Answer>();
            answerList7.Add(answer17);
            answerList7.Add(answer18);
            answerList7.Add(answer19);

            //Flags for each question
            Flag flag1 = new Flag("aids gul", "aids", false);
            Flag flag2 = new Flag("kræft gul", "kræft", false);
            Flag flag3 = new Flag("alzheimer rød", "alzheimer", false);
            Flag flag4 = new Flag("Colora rød", "colora", false);
            Flag flag5 = new Flag("penis rød", "penis", false);
            Flag flag6 = new Flag("lol rød", "lol", false);
            Flag flag7 = new Flag("aaa rød", "aaa", false);

            //Question creation
            Question question1 = new Question("Question 1", flag1, answerList1);
            Question question2 = new Question("Question 2", flag2, answerList2);
            Question question3 = new Question("Question 3", flag3, answerList3);
            Question question4 = new Question("Question 4", flag4, answerList4);
            Question question5 = new Question("Question 5", flag5, answerList5);
            Question question6 = new Question("Question 6", flag6, answerList6);
            Question question7 = new Question("Question 7", flag7, answerList7);

            question1.id = 1;
            question2.id = 2;
            question3.id = 3;
            question4.id = 4;
            question5.id = 5;
            question6.id = 6;
            question7.id = 7;


            //Questionnaire creation
            Questionnaire questionnaire = new Questionnaire("All around questionnaire");
            questionnaire.addQuestion(question1);
            questionnaire.addQuestion(question2);
            questionnaire.addQuestion(question3);
            //questionnaire.addQuestion(question4);
            //questionnaire.addQuestion(question5);
            //questionnaire.addQuestion(question6);
            //questionnaire.addQuestion(question7);

            //Flags to questionnaire?
            questionnaire.addFlag(flag1);
            questionnaire.addFlag(flag2);
            questionnaire.addFlag(flag3);
            //questionnaire.addFlag(flag4);
            //questionnaire.addFlag(flag5);
            //questionnaire.addFlag(flag6);
            //questionnaire.addFlag(flag7);

            patient.questionnaires.Add(questionnaire);

            return patient;
        }
    }
}
