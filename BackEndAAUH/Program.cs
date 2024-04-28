// See https://aka.ms/new-console-template for more information
using BackEndAAUH.DB;
using BackEndAAUH.Model;

Console.WriteLine("Hello, World!");


Answer answer1 = new Answer("Svar 1", 1);
//answer1.isChosen = true;
Answer answer2 = new Answer("Svar 2", 2);
//answer2.isChosen = true;

List<Answer> answerList1 = new List<Answer>();
answerList1.Add(answer1);
answerList1.Add(answer2);

Flag flag1 = new Flag("1", "aids gul", "aids", false);



Answer answer3 = new Answer("Svar 3", 3);
//answer3.isChosen = true;
Answer answer4 = new Answer("Svar 4", 4);
//answer4.isChosen = true;

List<Answer> answerList2 = new List<Answer>();
answerList2.Add(answer3);
answerList2.Add(answer4);

Flag flag2 = new Flag("2", "kræft gul", "kræft", false);



Answer answer5 = new Answer("Svar 5", 5);
//answer5.isChosen = true;
Answer answer6 = new Answer("Svar 6", 6);
//answer6.isChosen = true;
Answer answer7 = new Answer("Svar 7", 7);
answer7.isChosen = true;

List<Answer> answerList3 = new List<Answer>();
answerList3.Add(answer5);
answerList3.Add(answer6);
answerList3.Add(answer7);

Flag flag3 = new Flag("3", "alzheimer rød", "alzheimer", false);
Question question1 = new Question("1", "aids spørgsmål", flag1, answerList1);
Question question2 = new Question("2", "kræft spørgsmål", flag1, answerList2);
Question question3 = new Question("3", "alzheimer spørgsmål", flag1, answerList3);



Questionnaire questionnaire = new Questionnaire("All around spørgeskema2");
questionnaire.addQuestion(question1);
questionnaire.addQuestion(question2);
questionnaire.addQuestion(question3);
questionnaire.addFlag(flag1);
//questionnaire.addFlag(flag2);
//questionnaire.addFlag(flag3);



Dictionary<Flag, int> flagSums = questionnaire.getAllFlagsPointSum();
foreach (KeyValuePair<Flag, int> kvp in flagSums) {
    Console.WriteLine($"Key = {kvp.Key.flagName}, Value = {kvp.Value}");
}



//Console.WriteLine($"Question 1 highest: {question1.findHighestPoints()}");
//Console.WriteLine($"Question 2 highest: {question2.findHighestPoints()}");
//Console.WriteLine($"Question 3 highest: {question3.findHighestPoints()}");



Dictionary<Flag, int> highestPoints = questionnaire.getAllFlagsHighestPoints();
foreach (KeyValuePair<Flag, int> kvp in highestPoints) {
    Console.WriteLine($"Key = {kvp.Key.flagName}, Value = {kvp.Value}");
}

List<Flag> allFlags = questionnaire.setAllAlertLevel();

Console.WriteLine("\n\n\n");
foreach(Flag flag in allFlags) {
    Console.WriteLine($"Flag {flag.flagName} alert: {flag.alertLevel}");
}

List<Question> questions = new List<Question>();
questions.Add(question1);
questions.Add(question2);
questions.Add(question3);

QuestionDAO qdb = new QuestionDB();
//qdb.addQuestionsToDB(questions, 1);