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

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            Settings SettingsForm = new Settings();
            SettingsForm.Show();
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            using (var editDialog = new Form())
            {
                editDialog.Text = "Редактирование и удаление слова";
                editDialog.StartPosition = FormStartPosition.CenterParent;

                var wordList = new ListBox()
                {
                    Location = new Point(10, 10),
                    Size = new Size(200, 200)
                };
                editDialog.Controls.Add(wordList);

                // Заполнение списка словами и их переводами
                foreach (var pair in DataStorage.russianToEnglish)
                {
                    wordList.Items.Add($"{pair.Key} - {pair.Value}");
                }

                var editButton = new Button()
                {
                    Text = "Редактировать",
                    Location = new Point(10, 220),
                    DialogResult = DialogResult.Yes
                };
                editDialog.Controls.Add(editButton);

                var deleteButton = new Button()
                {
                    Text = "Удалить",
                    Location = new Point(120, 220),
                    DialogResult = DialogResult.No
                };
                editDialog.Controls.Add(deleteButton);

                var addButton = new Button()
                {
                    Text = "Добавить",
                    Location = new Point(10, 250),
                    DialogResult = DialogResult.OK
                };
                editDialog.Controls.Add(addButton);

                editDialog.AcceptButton = editButton;
                editDialog.CancelButton = deleteButton;

                DialogResult result = editDialog.ShowDialog();

                if (result == DialogResult.Yes)
                {
                    // Редактирование выбранного слова
                    string selectedWord = wordList.SelectedItem?.ToString();

                    if (selectedWord == null)
                    {
                        MessageBox.Show("Сначала выберите слово для редактирования", "Ошибка", MessageBoxButtons.OK);
                        return;
                    }

                    string[] parts = selectedWord.Split(new string[] { " - " }, StringSplitOptions.None);
                    string word = parts[0];

                    using (var editWordDialog = new Form())
                    {
                        editWordDialog.Text = "Редактирование слова";
                        editWordDialog.StartPosition = FormStartPosition.CenterParent;

                        var label = new Label()
                        {
                            Text = "Введите новое слово:",
                            Location = new Point(10, 10),
                            AutoSize = true
                        };
                        editWordDialog.Controls.Add(label);

                        var textBox = new TextBox()
                        {
                            Text = word,
                            Location = new Point(10, 30),
                            Size = new Size(200, 20)
                        };
                        editWordDialog.Controls.Add(textBox);

                        var saveButton = new Button()
                        {
                            Text = "Сохранить",
                            Location = new Point(10, 60),
                            DialogResult = DialogResult.OK
                        };
                        editWordDialog.Controls.Add(saveButton);

                        var cancelButton = new Button()
                        {
                            Text = "Отмена",
                            Location = new Point(90, 60),
                            DialogResult = DialogResult.Cancel
                        };
                        editWordDialog.Controls.Add(cancelButton);

                        editWordDialog.AcceptButton = saveButton;
                        editWordDialog.CancelButton = cancelButton;

                        DialogResult editResult = editWordDialog.ShowDialog();

                        if (editResult == DialogResult.OK)
                        {
                            // Получение отредактированного слова из текстового поля
                            string editedWord = textBox.Text;

                            if (editedWord == "" || editedWord == null)
                            {
                                string message = "Одно из полей ввода не содержит информации, перепроверьте введённые даные";
                                string caption = "Ошибка";
                                MessageBoxButtons buttons = MessageBoxButtons.OK;
                                MessageBox.Show(message, caption, buttons);
                                return;
                            }

                            // Редактирование слова в словаре
                            if (DataStorage.russianToEnglish.ContainsKey(word))
                            {
                                string translation = DataStorage.russianToEnglish[word];
                                DataStorage.russianToEnglish.Remove(word);
                                DataStorage.russianToEnglish[editedWord] = translation;
                            }

                            // Обновление списка слов
                            UpdateWordList();
                        }
                    }
                }
                else if (result == DialogResult.No)
                {
                    // Удаление выбранного слова
                    string selectedWord = wordList.SelectedItem?.ToString();

                    if (selectedWord == null)
                    {
                        MessageBox.Show("Сначала выберите слово для удаления", "Ошибка", MessageBoxButtons.OK);
                        return;
                    }

                    string[] parts = selectedWord.Split(new string[] { "-" }, StringSplitOptions.None);
                    string word = parts[0];

                    // Удаление слова из словаря
                    if (DataStorage.russianToEnglish.ContainsKey(word))
                    {
                        DataStorage.russianToEnglish.Remove(word);
                    }

                    // Обновление списка слов
                    UpdateWordList();
                }
                else if (result == DialogResult.OK)
                {
                    // Добавление нового слова
                    using (var addWordDialog = new Form())
                    {
                        addWordDialog.Text = "Добавление слова";
                        addWordDialog.StartPosition = FormStartPosition.CenterParent;

                        var label = new Label()
                        {
                            Text = "Введите новое слово и его перевод:",
                            Location = new Point(10, 10),
                            AutoSize = true
                        };
                        addWordDialog.Controls.Add(label);

                        var wordLabel = new Label()
                        {
                            Text = "Слово:",
                            Location = new Point(10, 30),
                            AutoSize = true
                        };
                        addWordDialog.Controls.Add(wordLabel);

                        var wordTextBox = new TextBox()
                        {
                            Location = new Point(70, 30),
                            Size = new Size(200, 20)
                        };
                        addWordDialog.Controls.Add(wordTextBox);

                        var translationLabel = new Label()
                        {
                            Text = "Перевод:",
                            Location = new Point(10, 60),
                            AutoSize = true
                        };
                        addWordDialog.Controls.Add(translationLabel);

                        var translationTextBox = new TextBox()
                        {
                            Location = new Point(70, 60),
                            Size = new Size(200, 20)
                        };
                        addWordDialog.Controls.Add(translationTextBox);

                        var saveButton = new Button()
                        {
                            Text = "Сохранить",
                            Location = new Point(10, 90),
                            DialogResult = DialogResult.OK
                        };
                        addWordDialog.Controls.Add(saveButton);

                        var cancelButton = new Button()
                        {
                            Text = "Отмена",
                            Location = new Point(90, 90),
                            DialogResult = DialogResult.Cancel
                        };
                        addWordDialog.Controls.Add(cancelButton);

                        addWordDialog.AcceptButton = saveButton;
                        addWordDialog.CancelButton = cancelButton;

                        DialogResult addResult = addWordDialog.ShowDialog();

                        if (addResult == DialogResult.OK)
                        {
                            // Получение нового слова и его перевода из текстовых полей
                            string newWord = wordTextBox.Text;
                            string newTranslation = translationTextBox.Text;

                            if(newWord == "" || newWord == null || newTranslation == "" || newTranslation == null)
                            {
                                string message = "Одно из полей ввода не содержит информации, перепроверьте введённые даные";
                                string caption = "Ошибка";
                                MessageBoxButtons buttons = MessageBoxButtons.OK;
                                MessageBox.Show(message, caption, buttons);
                                return;
                            }

                            // Добавление нового слова в словарь
                            if (!DataStorage.russianToEnglish.ContainsKey(newWord))
                            {
                                DataStorage.russianToEnglish[newWord] = newTranslation;
                            }

                            // Обновление списка слов
                            this.OutputListBox.Items.Clear();
                            UpdateWordList();
                        }
                    }
                }
            }
            OutputListBox.Items.Clear();
            DataStorage.UpdateJsonFiles();  
        }

        private void UpdateWordList()
        {
            OutputListBox.Items.Clear();
            DataStorage.UpdateJsonFiles();
        }
    }
}