namespace laba1
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void ChangeLanguageButton_Click(object sender, EventArgs e)
        {
            var temp = this.OutputLanguageLabel.Text;
            this.OutputLanguageLabel.Text = this.inputLanguageLabel.Text;
            this.inputLanguageLabel.Text = temp;
            var inputText = this.inputTextBox.Text;
            string[] words = inputText.Split(new[] { "\r\n", " " }, StringSplitOptions.RemoveEmptyEntries);
            List<string> outputWords = new List<string>();
            foreach(string word in this.OutputListBox.Items)
            {
                outputWords.Add(word);
            }
            this.OutputListBox.Items.Clear();
            foreach(string word in words)
            {
                this.OutputListBox.Items.Add(word);
            }
            string newInputText = "";
            foreach(string word in outputWords)
            {
                newInputText += word;
                newInputText+= "\r\n";
            }
            this.inputTextBox.Text = newInputText; 
            
        }

        private void translateButton_Click(object sender, EventArgs e)
        {
            if (this.inputTextBox.Text.Length== 0)
            {
                string message = "Поле ввода не заполнено, пожалуйста, введите слово, требующее перевод";
                string caption = "Ошибка";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
                return;  

            }
            this.OutputListBox.Items.Clear();
            string outputString = "";
            (List<string> outputText, bool isCorrect, List<string> primalWord) translatedText = Translate(inputTextBox.Text, stateOfLanguage);
            if (translatedText.outputText.Count() == 0 || translatedText.outputText == null)
            {
                string message = "Для введённого вами слова не был найден перевод, возможно данное слово отсутствует в словаре или содержит ошибку, перепроверьте введённое слово";
                string caption = "Ошибка";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
                return;
            }
            if (translatedText.isCorrect)
            {
                foreach (string item in translatedText.outputText)
                {
                    this.OutputListBox.Items.Add(item);
                }
            }
            if (!translatedText.isCorrect)
            {
                var inputVariantsText = "";
                this.inputTextBox.Text = "";
                foreach (var item in translatedText.primalWord)
                {
                    inputVariantsText += item + "\r\n";
                }
                this.inputTextBox.Text = inputVariantsText;
                if (translatedText.outputText.Count() > 1)
                {
                    string message = "Для данного слова не был найден однозначный перевод, поэтому представлены потенциальные варианты перевода и соответствующие исходые слова";
                    string caption = "Не был найден однозначный перевод";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show(message, caption, buttons);
                }
                foreach (string item in translatedText.outputText)
                {
                    this.OutputListBox.Items.Add(item);
                }

                this.OutputListBox.Text = outputString;
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            this.inputTextBox.Text = "";
            this.OutputListBox.Items.Clear();
        }
    }
}