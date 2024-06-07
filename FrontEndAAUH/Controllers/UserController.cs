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

        public Task<IActionResult> UserProfile(string id) {

            Patient patient = new Patient();
            PatientLogic pLogic = new PatientLogic(_configuration);

            try {
                patient = pLogic.getPatientByPatientNo(id);
            } catch (Exception e) {
                patient = null;
            }

            if (patient != null) {

                return Task.FromResult<IActionResult>(View(patient));
            } else {
                return Task.FromResult<IActionResult>(View("UserNotFound"));
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

            //TODO Implementer metode

            return RedirectToAction("EditUserQuestionnaire", new {id = qnm.patient.patientNo});
        }

        public ActionResult addQuestionnaire([FromForm] QuestionnaireModel qnm) {

            //TODO Implementer metode

            return RedirectToAction("EditUserQuestionnaire", new { id = qnm.patient.patientNo });
        }

        public ActionResult addCutomQuestion([FromForm] QuestionnaireModel qnm) {

            //TODO Implementer metode

            return RedirectToAction("EditUserQuestionnaire", new { id = qnm.patient.patientNo });
        }
    }
}
