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

        private void button2_Click(object sender, EventArgs e)
        {
            this.stateOfLanguage = !stateOfLanguage;
            if (this.stateOfLanguage)
            {
                this.inputLanguageLabel.Text = "Русский";
                this.OutputLanguageLabel.Text = "Английский";
            } else
            {
                this.OutputLanguageLabel.Text = "Русский";
                this.inputLanguageLabel.Text = "Английский";
            }
            this.OutputListBox.Items.Clear();

        }

        private void translateButton_Click(object sender, EventArgs e)
        {
            if (this.inputTextBox.Text.Length== 0)
            {
                string message = "Пожалуйста, введите слово";
                string caption = "Ошибка в вводе";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
                return;  

            }
            this.OutputListBox.Items.Clear();
            string outputString = "";
            (List<string> outputText, bool isCorrect, List<string> primalWord) translatedText = Translate(inputTextBox.Text, stateOfLanguage);
            if (translatedText.outputText.Count() == 0 || translatedText.outputText == null)
            {
                string message = "По введённым вами даным не были найдены варианты перевода, возможно введённое вами слово содержит ошибку или же отсутствует в словаре";
                string caption = "Ошибка в вводе";
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
                    string message = "В введённом вами слове были найдены ошибка, в текстовом поле ввода представлены исправленные варианты того, что вы могли хотеть ввести, в текстовом поле вывода- соответствующие варианты перевода";
                    string caption = "Предупреждение";
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.inputTextBox.Text = "";
            this.OutputListBox.Items.Clear();
        }
    }
}