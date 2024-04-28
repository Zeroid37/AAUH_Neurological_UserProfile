using FrontEndAAUH.Model;
using FrontEndAAUH.Models;
using FrontEndAAUH.Service;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;

namespace FrontEndAAUH.Controllers {
    public class UserController : Controller {
        
        private readonly IPatientService _patientService;

        public UserController() {
            _patientService = new PatientService();
        }
        public IActionResult Index() {
            return View();
        }

        public async Task<IActionResult> UserProfileAsync(string id) {

            Patient patient = new Patient();

            try {
                patient = await _patientService.getPatientByPatientNo(id);

            } catch (Exception e) {
                patient = null;
            }

            //Answers for question 1
            Answer answer1 = new Answer("Answer 1", 1);
            Answer answer2 = new Answer("Answer 2", 2);
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
            List<Answer> answerList6= new List<Answer>();
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
            Flag flag1 = new Flag("1", "aids gul", "aids", false);
            Flag flag2 = new Flag("2", "kræft gul", "kræft", false);
            Flag flag3 = new Flag("3", "alzheimer rød", "alzheimer", false);
            Flag flag4 = new Flag("4", "Colora rød", "colora", false);
            Flag flag5 = new Flag("5", "penis rød", "penis", false);
            Flag flag6 = new Flag("6", "lol rød", "lol", false);
            Flag flag7 = new Flag("7", "aaa rød", "aaa", false);

            //Question creation
            Question question1 = new Question("1", "Question 1", flag1, answerList1);
            Question question2 = new Question("2", "Question 2", flag2, answerList2);
            Question question3 = new Question("3", "Question 3", flag3, answerList3);
            Question question4 = new Question("4", "Question 4", flag4, answerList4);
            Question question5 = new Question("5", "Question 5", flag5, answerList5);
            Question question6 = new Question("6", "Question 6", flag6, answerList6);
            Question question7 = new Question("7", "Question 7", flag7, answerList7);


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

            return View(patient);
        }

        [HttpPost]
        public ActionResult Submit(IFormCollection frm, [FromForm] Patient p, [FromForm] List<Questionnaire> questionnaires) {
            return View();
        }
    }
}
