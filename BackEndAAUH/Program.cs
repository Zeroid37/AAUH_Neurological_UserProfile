// See https://aka.ms/new-console-template for more information
using BackEndAAUH.DB;
using BackEndAAUH.Model;
using BackEndAAUH.Business;

Console.WriteLine("Hello, World!");

Flag IHDFlag = new Flag("Ischeamic Heart Disease", "IHD flag", false);
Flag StrokeFlag = new Flag("Stroke", "Stroke flag", false);
Flag COPDFlag = new Flag("COPD Disease", "COPD flag", false);
Flag LungCancerFlag = new Flag("Tachea, bronchus, lung cancers", "Lung cancers flag", false);
Flag PrediabetesFlag = new Flag("Prediabetes", "Prediabetes flag", false);
Flag KidneyDiseaseFlag = new Flag("Kidney Disease", "Kidney Disease flag", false);



Questionnaire IHD = new Questionnaire("Ischeamic Heart Disease");
IHD.addFlag(IHDFlag);
Question IHDq1 = new Question("Vurder din smerte i brystet, specielt efter fysisk træning. (0 hvis du ingen smerte har)", IHDFlag);
IHDq1.addAnswer(new Answer("0", 0));
IHDq1.addAnswer(new Answer("1", 1));
IHDq1.addAnswer(new Answer("2", 2));
IHDq1.addAnswer(new Answer("3", 3));
IHDq1.addAnswer(new Answer("4", 4));
IHDq1.addAnswer(new Answer("5", 5));

Question IHDq2 = new Question("Har du følt dig svimmel eller besvimmet for nyeligt?", IHDFlag);
IHDq2.addAnswer(new Answer("Nej", 0));
IHDq2.addAnswer(new Answer("Ja, enkelte gange", 1));
IHDq2.addAnswer(new Answer("Ja, jævnligt", 2));
IHDq2.addAnswer(new Answer("Ja, ofte", 3));
IHDq2.addAnswer(new Answer("Ja, dagligt", 4));

Question IHDq3 = new Question("Har du mærket nogen forskel i dit hjertes banken?", IHDFlag);
IHDq3.addAnswer(new Answer("Nej", 0));
IHDq3.addAnswer(new Answer("Ja, lidt hurtigere", 1));
IHDq3.addAnswer(new Answer("Ja, meget hurtigere", 2));
IHDq3.addAnswer(new Answer("Ja, det føles som at det springer slag over", 3));

Question IHDq4 = new Question("Har du følt dig mere kortåndet eller forbustet for nyeligt?", IHDFlag);
IHDq4.addAnswer(new Answer("Nej", 0));
IHDq4.addAnswer(new Answer("Ja, en smule", 1));
IHDq4.addAnswer(new Answer("Ja, en del", 2));
IHDq4.addAnswer(new Answer("Ja, meget", 3));

Question IHDq5 = new Question("Har du lagt mærke til høvelser i dine fødder eller ankler?", IHDFlag);
IHDq5.addAnswer(new Answer("Nej", 0));
IHDq5.addAnswer(new Answer("Ja, en smule", 1));
IHDq5.addAnswer(new Answer("Ja, en del", 2));
IHDq5.addAnswer(new Answer("Ja, meget", 3));

IHD.addQuestion(IHDq1);
IHD.addQuestion(IHDq2);
IHD.addQuestion(IHDq3);
IHD.addQuestion(IHDq4);
IHD.addQuestion(IHDq5);



Questionnaire Stroke = new Questionnaire("Stroke");
Stroke.addFlag(StrokeFlag);
Question Strokeq1 = new Question("Hvad er dit blodtryk på?", StrokeFlag);
Strokeq1.addAnswer(new Answer("Mindre end 120/80", 0));
Strokeq1.addAnswer(new Answer("120-139/80-89", 1));
Strokeq1.addAnswer(new Answer("Mere end 140/90", 2));

Question Strokeq2 = new Question("Hvad ligger dit kolesterol på?", StrokeFlag);
Strokeq2.addAnswer(new Answer("Under 5,0 mmol/L", 0));
Strokeq2.addAnswer(new Answer("5,1-6,4 mmol/L", 1));
Strokeq2.addAnswer(new Answer("6,5-7,9 mmol/L", 2));
Strokeq2.addAnswer(new Answer("Over 8,0 mmol/L", 3));

Question Strokeq3 = new Question("Har du diabetes?", StrokeFlag);
Strokeq3.addAnswer(new Answer("Nej", 0));
Strokeq3.addAnswer(new Answer("Ja, arvet", 1));
Strokeq3.addAnswer(new Answer("Ja", 2));

Question Strokeq4 = new Question("Ryger du?", StrokeFlag);
Strokeq4.addAnswer(new Answer("Nej", 0));
Strokeq4.addAnswer(new Answer("Ja, jeg prøver at stoppe", 1));
Strokeq4.addAnswer(new Answer("Ja", 2));

Question Strokeq5 = new Question("Hvordan er dit hjertes rymte?", StrokeFlag);
Strokeq5.addAnswer(new Answer("Mit hjertes banken har en normal rytme", 0));
Strokeq5.addAnswer(new Answer("Mit hjertes banken har en unormal rytme", 1));
Strokeq5.addAnswer(new Answer("Det ved jeg ikke", 2));

Question Strokeq6 = new Question("Hvordan er din vægt?", StrokeFlag);
Strokeq6.addAnswer(new Answer("Min vægt er sund", 0));
Strokeq6.addAnswer(new Answer("Jeg er lidt overvægtig", 1));
Strokeq6.addAnswer(new Answer("Jeg er meget overvægtig", 2));

Question Strokeq7 = new Question("Hvor ofte bevæger du dig eller dyrker sport?", StrokeFlag);
Strokeq7.addAnswer(new Answer("Jeg bevæger mig ofte", 0));
Strokeq7.addAnswer(new Answer("Jeg bevæger mig nogle gange", 1));
Strokeq7.addAnswer(new Answer("Jeg bevæger mig aldrig", 2));

Question Strokeq8 = new Question("Har din famile en historie med at ryge?", StrokeFlag);
Strokeq8.addAnswer(new Answer("Nej", 0));
Strokeq8.addAnswer(new Answer("Ja", 1));
Strokeq8.addAnswer(new Answer("Ved ikke", 2));

Stroke.addQuestion(Strokeq1);
Stroke.addQuestion(Strokeq2);
Stroke.addQuestion(Strokeq3);
Stroke.addQuestion(Strokeq4);
Stroke.addQuestion(Strokeq5);
Stroke.addQuestion(Strokeq6);
Stroke.addQuestion(Strokeq7);
Stroke.addQuestion(Strokeq8);



Questionnaire COPD = new Questionnaire("Chronic Obstructive Pulmonary Disease");
COPD.addFlag(COPDFlag);
Question COPDq1 = new Question("Hvor ofte hoster du?", COPDFlag);
COPDq1.addAnswer(new Answer("Aldrig", 0));
COPDq1.addAnswer(new Answer("Enkelte gange", 1));
COPDq1.addAnswer(new Answer("Jævnligt", 2));
COPDq1.addAnswer(new Answer("Ofte", 3));
COPDq1.addAnswer(new Answer("Dagligt", 4));

Question COPDq2 = new Question("På en skala fra 0-5, hvor fyldte føler du at dine lunger er med slim?", COPDFlag);
COPDq2.addAnswer(new Answer("0", 0));
COPDq2.addAnswer(new Answer("1", 1));
COPDq2.addAnswer(new Answer("2", 2));
COPDq2.addAnswer(new Answer("3", 3));
COPDq2.addAnswer(new Answer("4", 4));
COPDq2.addAnswer(new Answer("5", 5));

Question COPDq3 = new Question("På en skala fra 0-5, hvor meget føler du at du har tryk i brystet?", COPDFlag);
COPDq3.addAnswer(new Answer("0", 0));
COPDq3.addAnswer(new Answer("1", 1));
COPDq3.addAnswer(new Answer("2", 2));
COPDq3.addAnswer(new Answer("3", 3));
COPDq3.addAnswer(new Answer("4", 4));
COPDq3.addAnswer(new Answer("5", 5));

Question COPDq4 = new Question("På en skala fra 0-5, hvor let bliver du forpustet når du bevæger dig?", COPDFlag);
COPDq4.addAnswer(new Answer("0", 0));
COPDq4.addAnswer(new Answer("1", 1));
COPDq4.addAnswer(new Answer("2", 2));
COPDq4.addAnswer(new Answer("3", 3));
COPDq4.addAnswer(new Answer("4", 4));
COPDq4.addAnswer(new Answer("5", 5));

Question COPDq5 = new Question("På en skala fra 0-5, hvor begrænset føler du dig fysiskt når du laver aktiviteter derhjemme?", COPDFlag);
COPDq5.addAnswer(new Answer("0", 0));
COPDq5.addAnswer(new Answer("1", 1));
COPDq5.addAnswer(new Answer("2", 2));
COPDq5.addAnswer(new Answer("3", 3));
COPDq5.addAnswer(new Answer("4", 4));
COPDq5.addAnswer(new Answer("5", 5));

Question COPDq6 = new Question("På en skala fra 0-5, hvordan føler du at dit energi niveau er?", COPDFlag);
COPDq6.addAnswer(new Answer("0", 0));
COPDq6.addAnswer(new Answer("1", 1));
COPDq6.addAnswer(new Answer("2", 2));
COPDq6.addAnswer(new Answer("3", 3));
COPDq6.addAnswer(new Answer("4", 4));
COPDq6.addAnswer(new Answer("5", 5));

COPD.addQuestion(COPDq1);
COPD.addQuestion(COPDq2);
COPD.addQuestion(COPDq3);
COPD.addQuestion(COPDq4);
COPD.addQuestion(COPDq5);
COPD.addQuestion(COPDq6);



Questionnaire LungCancer = new Questionnaire("Tachea, bronchus, lung cancers");
LungCancer.addFlag(LungCancerFlag);
Question LungCancerq1 = new Question("Hvor ofte hoster du?", COPDFlag);
LungCancerq1.addAnswer(new Answer("Aldrig", 0));
LungCancerq1.addAnswer(new Answer("Enkelte gange", 1));
LungCancerq1.addAnswer(new Answer("Jævnligt", 2));
LungCancerq1.addAnswer(new Answer("Ofte", 3));
LungCancerq1.addAnswer(new Answer("Dagligt", 4));

Question LungCancerq2 = new Question("Hvor ofte hoster du blod eller rustfarvet slim?", LungCancerFlag);
LungCancerq2.addAnswer(new Answer("Aldrig", 0));
LungCancerq2.addAnswer(new Answer("Enkelte gange", 1));
LungCancerq2.addAnswer(new Answer("Ofte", 2));
LungCancerq2.addAnswer(new Answer("Dagligt", 3));

Question LungCancerq3 = new Question("Hvor meget smerte oplever du i dit bryst når du griner, hoster eller trækker vejret?", LungCancerFlag);
LungCancerq3.addAnswer(new Answer("0", 0));
LungCancerq3.addAnswer(new Answer("1", 1));
LungCancerq3.addAnswer(new Answer("2", 2));
LungCancerq3.addAnswer(new Answer("3", 3));
LungCancerq3.addAnswer(new Answer("4", 4));
LungCancerq3.addAnswer(new Answer("5", 5));

Question LungCancerq4 = new Question("Hvor hæs føler du dig?", LungCancerFlag);
LungCancerq4.addAnswer(new Answer("0", 0));
LungCancerq4.addAnswer(new Answer("1", 1));
LungCancerq4.addAnswer(new Answer("2", 2));
LungCancerq4.addAnswer(new Answer("3", 3));
LungCancerq4.addAnswer(new Answer("4", 4));
LungCancerq4.addAnswer(new Answer("5", 5));

Question LungCancerq5 = new Question("Hvor meget appetit føler du normalt at du har?", LungCancerFlag);
LungCancerq5.addAnswer(new Answer("0", 0));
LungCancerq5.addAnswer(new Answer("1", 1));
LungCancerq5.addAnswer(new Answer("2", 2));
LungCancerq5.addAnswer(new Answer("3", 3));
LungCancerq5.addAnswer(new Answer("4", 4));
LungCancerq5.addAnswer(new Answer("5", 5));

Question LungCancerq6 = new Question("Har du haft uforklareligt vægt tab?", LungCancerFlag);
LungCancerq6.addAnswer(new Answer("Nej", 0));
LungCancerq6.addAnswer(new Answer("Ja, lidt", 1));
LungCancerq6.addAnswer(new Answer("Ja, en del", 2));
LungCancerq6.addAnswer(new Answer("Ja, meget", 3));

Question LungCancerq7 = new Question("Hvor ofte føler du dig forpustet?", LungCancerFlag);
LungCancerq7.addAnswer(new Answer("Aldrig", 0));
LungCancerq7.addAnswer(new Answer("Til tider", 1));
LungCancerq7.addAnswer(new Answer("Ofte", 2));
LungCancerq7.addAnswer(new Answer("Konstant", 3));

Question LungCancerq8 = new Question("Hvor ofte føler du dig svag eller træt?", LungCancerFlag);
LungCancerq8.addAnswer(new Answer("Aldrig", 0));
LungCancerq8.addAnswer(new Answer("Til tider", 1));
LungCancerq8.addAnswer(new Answer("Ofte", 2));
LungCancerq8.addAnswer(new Answer("Konstant", 3));

LungCancer.addQuestion(LungCancerq1);
LungCancer.addQuestion(LungCancerq2);
LungCancer.addQuestion(LungCancerq3);
LungCancer.addQuestion(LungCancerq4);
LungCancer.addQuestion(LungCancerq5);
LungCancer.addQuestion(LungCancerq6);
LungCancer.addQuestion(LungCancerq7);
LungCancer.addQuestion(LungCancerq8);



Questionnaire Prediabetes = new Questionnaire("Prediabetes");
Prediabetes.addFlag(PrediabetesFlag);
Question Prediabetesq1 = new Question("Har du en mor, far, søster eller bror med diabetes?", PrediabetesFlag);
Prediabetesq1.addAnswer(new Answer("Nej", 0));
Prediabetesq1.addAnswer(new Answer("Ja", 1));

Question Prediabetesq2 = new Question("Har du nogensinde fået konstateret forhøjet blodtryk?", PrediabetesFlag);
Prediabetesq2.addAnswer(new Answer("Nej", 0));
Prediabetesq2.addAnswer(new Answer("Ja", 1));

Question Prediabetesq3 = new Question("Hvor gammel er du?", PrediabetesFlag);
Prediabetesq3.addAnswer(new Answer("Under 40", 0));
Prediabetesq3.addAnswer(new Answer("40-49", 1));
Prediabetesq3.addAnswer(new Answer("50-59", 2));
Prediabetesq3.addAnswer(new Answer("Over 60", 3));

Question Prediabetesq4 = new Question("Er du fysisk aktiv?", PrediabetesFlag);
Prediabetesq4.addAnswer(new Answer("Nej", 0));
Prediabetesq4.addAnswer(new Answer("Ja", 1));

Question Prediabetesq5 = new Question("Hvilket køn er du?", PrediabetesFlag);
Prediabetesq5.addAnswer(new Answer("Mand", 2));
Prediabetesq5.addAnswer(new Answer("Kvinde", 1));

Question Prediabetesq6 = new Question("Hvad er din BMI?", PrediabetesFlag);
Prediabetesq5.addAnswer(new Answer("Under 18,5", 1));
Prediabetesq5.addAnswer(new Answer("18,5-25", 2));
Prediabetesq5.addAnswer(new Answer("25-30", 3));
Prediabetesq5.addAnswer(new Answer("Over 30", 4));

Prediabetes.addQuestion(Prediabetesq1);
Prediabetes.addQuestion(Prediabetesq2);
Prediabetes.addQuestion(Prediabetesq3);
Prediabetes.addQuestion(Prediabetesq4);
Prediabetes.addQuestion(Prediabetesq5);
Prediabetes.addQuestion(Prediabetesq6);



Questionnaire Kidney = new Questionnaire("Kidney Diseases");
Kidney.addFlag(KidneyDiseaseFlag);
Question Kidneyq1 = new Question("Har din læge fået at vide, at du har nogen af følgende tilstande?", KidneyDiseaseFlag);
Kidneyq1.addAnswer(new Answer("Diabetes eller at tage medicin for at kontrollere dit blodsukker", 4));
Kidneyq1.addAnswer(new Answer("Prædiabetes", 3));
Kidneyq1.addAnswer(new Answer("Forhøjet blodtryk eller at tage medicin for at kontrollere dit blodtryk", 2));
Kidneyq1.addAnswer(new Answer("Hjertesygdom eller hjertesvigt", 1));
Kidneyq1.addAnswer(new Answer("Ingen af disse gælder for mig", 0));

Question Kidneyq2 = new Question("Har nogen i din familie fået foretaget en nyretransplantation, haft nyresvigt eller været i dialyse?", KidneyDiseaseFlag);
Kidneyq2.addAnswer(new Answer("Ja", 1));
Kidneyq2.addAnswer(new Answer("Ingen", 0));
Kidneyq2.addAnswer(new Answer("Det ved jeg ikke", 0));

Question Kidneyq3 = new Question("Hvad er din BMI?", KidneyDiseaseFlag);
Kidneyq3.addAnswer(new Answer("Under 18,5", 1));
Kidneyq3.addAnswer(new Answer("18,5-25", 2));
Kidneyq3.addAnswer(new Answer("25-30", 3));
Kidneyq3.addAnswer(new Answer("Over 30", 4));

Question Kidneyq4 = new Question("Hvilket køn er du?", KidneyDiseaseFlag);
Kidneyq4.addAnswer(new Answer("Mand", 1));
Kidneyq4.addAnswer(new Answer("Kvinde", 0));

Question Kidneyq5 = new Question("Hvor gammel er du?", KidneyDiseaseFlag);
Kidneyq5.addAnswer(new Answer("Over 35", 0));
Kidneyq5.addAnswer(new Answer("36-50", 1));
Kidneyq5.addAnswer(new Answer("51-64", 2));
Kidneyq5.addAnswer(new Answer("Over 65", 3));

Kidney.addQuestion(Kidneyq1);
Kidney.addQuestion(Kidneyq2);
Kidney.addQuestion(Kidneyq3);
Kidney.addQuestion(Kidneyq4);
Kidney.addQuestion(Kidneyq5);


//FlagDB flagDB = new FlagDB();
//string IDHflagid = flagDB.addFlagToDB(IHDFlag).ToString();
//IHDFlag.id = IDHflagid;

//string Strokeflagid = flagDB.addFlagToDB(StrokeFlag).ToString();
//StrokeFlag.id = Strokeflagid;

//string COPDflagid = flagDB.addFlagToDB(COPDFlag).ToString();
//COPDFlag.id = COPDflagid;

//string LungCancerflagid = flagDB.addFlagToDB(LungCancerFlag).ToString();
//LungCancerFlag.id = LungCancerflagid;

//string Prediabetesflagid = flagDB.addFlagToDB(PrediabetesFlag).ToString();
//PrediabetesFlag.id = Prediabetesflagid;

//string KidneyDiseaseflagid = flagDB.addFlagToDB(KidneyDiseaseFlag).ToString();
//KidneyDiseaseFlag.id = KidneyDiseaseflagid;

//QuestionnaireDB qdb = new QuestionnaireDB();
//qdb.addQuestionnaireToDB(IHD);
//qdb.addQuestionnaireToDB(Stroke);
//qdb.addQuestionnaireToDB(COPD);
//qdb.addQuestionnaireToDB(LungCancer);
//qdb.addQuestionnaireToDB(Prediabetes);
//qdb.addQuestionnaireToDB(Kidney);


//Questionnaire qtest = qdb.getQuestionnaireByTitle(COPD.title);
//Console.WriteLine(qtest.title);
//foreach(Question question in qtest.questions) {
//    Console.WriteLine($"\t{question.questionDescription}");
//    foreach(Answer answer in question.answers) {
//        Console.WriteLine($"\t\t{answer.answerText} - value: {answer.answerValue} | {answer.isChosen}");
//    }
//    Console.WriteLine("");
//}



//Answer answer1 = new Answer("Svar 1", 1);
////answer1.isChosen = true;
//Answer answer2 = new Answer("Svar 2", 2);
////answer2.isChosen = true;

//List<Answer> answerList1 = new List<Answer>();
//answerList1.Add(answer1);
//answerList1.Add(answer2);

//Flag flag1 = new Flag("1", "aids gul", "aids", false);



//Answer answer3 = new Answer("Svar 3", 3);
////answer3.isChosen = true;
//Answer answer4 = new Answer("Svar 4", 4);
////answer4.isChosen = true;

//List<Answer> answerList2 = new List<Answer>();
//answerList2.Add(answer3);
//answerList2.Add(answer4);

//Flag flag2 = new Flag("2", "kræft gul", "kræft", false);



//Answer answer5 = new Answer("Svar 5", 5);
////answer5.isChosen = true;
//Answer answer6 = new Answer("Svar 6", 6);
////answer6.isChosen = true;
//Answer answer7 = new Answer("Svar 7", 7);
//answer7.isChosen = true;

//List<Answer> answerList3 = new List<Answer>();
//answerList3.Add(answer5);
//answerList3.Add(answer6);
//answerList3.Add(answer7);

//Flag flag3 = new Flag("3", "alzheimer rød", "alzheimer", false);
//Question question1 = new Question("1", "aids spørgsmål", flag1, answerList1);
//Question question2 = new Question("2", "kræft spørgsmål", flag1, answerList2);
//Question question3 = new Question("3", "alzheimer spørgsmål", flag1, answerList3);



//Questionnaire questionnaire = new Questionnaire("All around spørgeskema2");
//questionnaire.addQuestion(question1);
//questionnaire.addQuestion(question2);
//questionnaire.addQuestion(question3);
//questionnaire.addFlag(flag1);
////questionnaire.addFlag(flag2);
////questionnaire.addFlag(flag3);



//Dictionary<Flag, int> flagSums = questionnaire.getAllFlagsPointSum();
//foreach (KeyValuePair<Flag, int> kvp in flagSums) {
//    Console.WriteLine($"Key = {kvp.Key.flagName}, Value = {kvp.Value}");
//}



////Console.WriteLine($"Question 1 highest: {question1.findHighestPoints()}");
////Console.WriteLine($"Question 2 highest: {question2.findHighestPoints()}");
////Console.WriteLine($"Question 3 highest: {question3.findHighestPoints()}");



//Dictionary<Flag, int> highestPoints = questionnaire.getAllFlagsHighestPoints();
//foreach (KeyValuePair<Flag, int> kvp in highestPoints) {
//    Console.WriteLine($"Key = {kvp.Key.flagName}, Value = {kvp.Value}");
//}

//List<Flag> allFlags = questionnaire.setAllAlertLevel();

//Console.WriteLine("\n\n\n");
//foreach(Flag flag in allFlags) {
//    Console.WriteLine($"Flag {flag.flagName} alert: {flag.alertLevel}");
//}

//List<Question> questions = new List<Question>();
//questions.Add(question1);
//questions.Add(question2);
//questions.Add(question3);

//QuestionDAO qdb = new QuestionDB();
//qdb.addQuestionsToDB(questions, 1);

//DateOnly lastRead = new DateOnly(2024, 1, 1); //TODO Remove dummy date

//FlagLogic fl = new FlagLogic();
//fl.processFlags(lastRead);