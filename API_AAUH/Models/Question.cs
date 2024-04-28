namespace API_AAUH.Models {
    public class Question {
        public string Title { get; set; }
        public List<Answer> Answers { get; set; }
        public Question(string title) {
            Title = title;
            Answers = new List<Answer>();
        }

        public void addAnswer(Answer answer) {
            Answers.Add(answer);
        }
    }
}
