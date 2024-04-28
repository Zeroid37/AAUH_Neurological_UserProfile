namespace API_AAUH.Models {
    public class Answer {
        public string answerText { get; set; }
        public bool isChosen { get; set; }
        public Answer(string answerText) {
            this.answerText = answerText;
            isChosen = false;
        }
        public Answer(string answerText, bool isChosen) {
            this.answerText = answerText;
            this.isChosen = isChosen;
        }

        public void setChosen() {
            isChosen = true;
        }
    }
}
