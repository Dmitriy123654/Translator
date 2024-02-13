using System.Linq;
using System.Text.RegularExpressions;

namespace laba1
{
    partial class Form1
    {
        private bool stateOfLanguage = true;
        private static DataStorage dataStorage = new DataStorage();
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private (List<string>, bool, List<string>) Translate(string inputString, bool stateOfLanguage)
        {
            List<string> OutPutlist = new List<string>();
            string output = "";
            //обрезаем пробелы и \n 
            inputString = inputString.Trim();
            var (statusValidation, russianLanguage) = CheckLanguageValidity(inputString);
            if (!statusValidation)
                return (OutPutlist, false, null);

            if (russianLanguage)
            {
                this.inputLanguageLabel.Text = "Русский";
                this.OutputLanguageLabel.Text = "Английский";
            }

            if(!russianLanguage)
            {
                this.inputLanguageLabel.Text = "Английский";
                this.OutputLanguageLabel.Text = "Русский";
            }


            if (dataStorage.russianToEnglish.ContainsKey(inputString))
            {
                output = dataStorage.russianToEnglish[inputString];
                OutPutlist.Add(output);
                return (OutPutlist, true, null);
            }
            else if (dataStorage.englishToRussian.ContainsKey(inputString))
            {
                output = dataStorage.englishToRussian[inputString];
                OutPutlist.Add(output);
                return (OutPutlist, true, null);
            }
            List<string> possibleErrors;
            if (russianLanguage)
                possibleErrors = FindPossibleErrors(inputString, dataStorage.russianToEnglish);
            else
                possibleErrors = FindPossibleErrors(inputString, dataStorage.englishToRussian);

            if (possibleErrors.Count == 1)
            {
                string correctedWord = possibleErrors[0];
                if (russianLanguage)
                {
                    if (dataStorage.russianToEnglish.ContainsKey(correctedWord))
                    {
                        // Найдено исправленное слово в русско-английском словаре
                        output = dataStorage.russianToEnglish[correctedWord];

                        OutPutlist.Add(output);
                    }
                }
                else if (dataStorage.englishToRussian.ContainsKey(correctedWord))
                {
                    //Найдено исправленное слово в англо-русском словаре
                    output = dataStorage.englishToRussian[correctedWord];
                    OutPutlist.Add(output);
                }
            }
            else if (possibleErrors.Count > 1)
            {

                OutPutlist.AddRange(possibleErrors.Distinct());
            }


            List<string> NonTranslated = new List<string>();
            if (russianLanguage)
                foreach (var NonTranslatedString in possibleErrors)
                {
                    NonTranslated.Add(dataStorage.englishToRussian[NonTranslatedString]);
                }
            else
            {
                foreach (var NonTranslatedString in possibleErrors)
                {
                    NonTranslated.Add(dataStorage.russianToEnglish[NonTranslatedString]);
                }
            }
            return (possibleErrors, false, NonTranslated);
        }
        private void InitializeComponent()
        {
            this.title = new System.Windows.Forms.Label();
            this.inputTextBox = new System.Windows.Forms.TextBox();
            this.inputLanguageLabel = new System.Windows.Forms.Label();
            this.OutputLanguageLabel = new System.Windows.Forms.Label();
            this.OutputListBox = new System.Windows.Forms.ListBox();
            this.translateButton = new System.Windows.Forms.Button();
            this.ChangeLanguageButton = new System.Windows.Forms.Button();
            this.ClearButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // title
            // 
            this.title.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.title.AutoSize = true;
            this.title.Location = new System.Drawing.Point(383, 9);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(94, 20);
            this.title.TabIndex = 0;
            this.title.Text = "Переводчик";
            this.title.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // inputTextBox
            // 
            this.inputTextBox.Location = new System.Drawing.Point(95, 85);
            this.inputTextBox.Multiline = true;
            this.inputTextBox.Name = "inputTextBox";
            this.inputTextBox.PlaceholderText = "Введите текст";
            this.inputTextBox.Size = new System.Drawing.Size(174, 108);
            this.inputTextBox.TabIndex = 1;
            this.inputTextBox.UseWaitCursor = true;
            // 
            // inputLanguageLabel
            // 
            this.inputLanguageLabel.AutoSize = true;
            this.inputLanguageLabel.Location = new System.Drawing.Point(95, 60);
            this.inputLanguageLabel.Name = "inputLanguageLabel";
            this.inputLanguageLabel.Size = new System.Drawing.Size(146, 20);
            this.inputLanguageLabel.TabIndex = 2;
            this.inputLanguageLabel.Text = "Русский";
            // 
            // OutputLanguageLabel
            // 
            this.OutputLanguageLabel.AutoSize = true;
            this.OutputLanguageLabel.Location = new System.Drawing.Point(619, 64);
            this.OutputLanguageLabel.Name = "OutputLanguageLabel";
            this.OutputLanguageLabel.Size = new System.Drawing.Size(176, 20);
            this.OutputLanguageLabel.TabIndex = 3;
            this.OutputLanguageLabel.Text = "Английский";
            this.OutputLanguageLabel.Click += new System.EventHandler(this.label3_Click);
            // 
            // OutputListBox
            // 
            this.OutputListBox.FormattingEnabled = true;
            this.OutputListBox.ItemHeight = 20;
            this.OutputListBox.Location = new System.Drawing.Point(619, 89);
            this.OutputListBox.Name = "OutputListBox";
            this.OutputListBox.Size = new System.Drawing.Size(176, 104);
            this.OutputListBox.TabIndex = 4;
            // 
            // translateButton
            // 
            this.translateButton.Location = new System.Drawing.Point(383, 122);
            this.translateButton.Name = "translateButton";
            this.translateButton.Size = new System.Drawing.Size(94, 29);
            this.translateButton.TabIndex = 5;
            this.translateButton.Text = "Перевести";
            this.translateButton.UseVisualStyleBackColor = true;
            this.translateButton.Click += new System.EventHandler(this.translateButton_Click);
            // 
            // ChangeLanguageButton
            // 
            this.ChangeLanguageButton.Location = new System.Drawing.Point(371, 60);
            this.ChangeLanguageButton.Name = "ChangeLanguageButton";
            this.ChangeLanguageButton.Size = new System.Drawing.Size(116, 24);
            this.ChangeLanguageButton.TabIndex = 6;
            this.ChangeLanguageButton.Text = "Сменить язык";
            this.ChangeLanguageButton.UseVisualStyleBackColor = true;
            this.ChangeLanguageButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.Location = new System.Drawing.Point(383, 164);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(94, 29);
            this.ClearButton.TabIndex = 7;
            this.ClearButton.Text = "Очистить";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(866, 461);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.ChangeLanguageButton);
            this.Controls.Add(this.translateButton);
            this.Controls.Add(this.OutputListBox);
            this.Controls.Add(this.OutputLanguageLabel);
            this.Controls.Add(this.inputLanguageLabel);
            this.Controls.Add(this.inputTextBox);
            this.Controls.Add(this.title);
            this.Name = "Form1";
            this.Text = "Переводчик";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        static List<string> FindPossibleErrors(string word, Dictionary<string, string> dictionaryWords)
        {
            List<string> possibleErrors = new List<string>();
            word = word.ToLower();


            foreach (string dictWord in dictionaryWords.Keys)
            {
                if (word.Length == dictWord.Length)
                {
                    // Проверка замены одной буквы
                    int mismatchCount = 0;
                    int mismatchIndex = -1;

                    for (int i = 0; i < word.Length; i++)
                    {
                        if (word[i] != dictWord[i])
                        {
                            mismatchCount++;
                            mismatchIndex = i;
                        }
                    }

                    if (mismatchCount == 1)
                    {
                        // Проверяем, что в замененных позициях символы не поменяны местами
                        if (word[mismatchIndex] == dictWord[mismatchIndex + 1] && word[mismatchIndex + 1] == dictWord[mismatchIndex])
                        {
                            possibleErrors.Add(dictionaryWords[dictWord]);
                        }
                    }

                    // Проверка перестановки букв
                    string sortedWord = String.Concat(word.OrderBy(c => c));
                    string sortedDictWord = String.Concat(dictWord.OrderBy(c => c));

                    if (sortedWord.Equals(sortedDictWord, StringComparison.OrdinalIgnoreCase))
                    {
                        var (swapsCount, nearestSymbols) = CountSwappedNeighboringCharacters(word, dictWord);
                        if (swapsCount == 2 && nearestSymbols)
                        {
                            possibleErrors.Add(dictionaryWords[dictWord]);
                        }
                     
                    }
                }
                else if (Math.Abs(word.Length - dictWord.Length) == 1)
                {
                    // Проверка пропущенного символа
                    for (int i = 0; i <= dictWord.Length; i++)
                    {
                        string partialDictWord = dictWord.Insert(i, word[word.Length - 1].ToString());

                        if (word.Equals(partialDictWord, StringComparison.OrdinalIgnoreCase))
                        {
                            possibleErrors.Add(dictionaryWords[dictWord]);
                        }
                    }

                    // Проверка лишнего символа
                    for (int i = 0; i < word.Length; i++)
                    {
                        string partialWord = word.Remove(i, 1);

                        if (partialWord.Equals(dictWord, StringComparison.OrdinalIgnoreCase))
                        {
                            possibleErrors.Add(dictionaryWords[dictWord]);
                        }
                    }
                }
            }
            // Проверка пропущенных букв
            foreach (string dictWord in dictionaryWords.Keys)
            {
                if (IsSimilarWord(word, dictWord))
                {
                    possibleErrors.Add(dictionaryWords[dictWord]);
                }
            }
            return possibleErrors.Distinct().ToList();
        }
        // Проверка пропущенных букв
        static bool IsSimilarWord(string word, string dictWord)
        {
            int wordLength = word.Length;
            int dictWordLength = dictWord.Length;

            if (Math.Abs(wordLength - dictWordLength) > 1)
            {
                return false;
            }

            if (wordLength == dictWordLength)
            {
                int mismatchCount = 0;

                for (int i = 0; i < wordLength; i++)
                {
                    if (word[i] != dictWord[i])
                    {
                        mismatchCount++;
                    }

                    if (mismatchCount > 1)
                    {
                        return false;
                    }
                }

                return true;
            }
            else
            {
                // Проверяем, содержит ли более короткое слово пропущенную букву
                string shorterWord = (wordLength < dictWordLength) ? word : dictWord;
                string longerWord = (wordLength < dictWordLength) ? dictWord : word;

                int shorterLength = shorterWord.Length;
                int longerLength = longerWord.Length;

                for (int i = 0; i < shorterLength; i++)
                {
                    if (shorterWord[i] != longerWord[i])
                    {
                        return shorterWord.Substring(i) == longerWord.Substring(i + 1);
                    }
                }

                // Если все символы совпадают, проверяем, является ли оставшаяся часть длинного слова пропущенной буквой
                return shorterLength == longerLength - 1;
            }
        }
        static (int,bool) CountSwappedNeighboringCharacters(string word1, string word2)
        {
            int count = 0;
            bool NearestSymbols = false;

            if (word1.Length != word2.Length)
            {
                return (count,NearestSymbols);
            }

            for (int i = 0; i < word1.Length - 1; i++)
            {
                if (word1[i] != word2[i])
                {
                    count++;

                    if (count < 2 && word1[i] == word2[i + 1] && word1[i + 1] == word2[i])
                    {
                        NearestSymbols = true;
                        if (word1[i] == word2[i + 1] && i + 1 == word1.Length - 1)
                            count++;
                    }
                }
            }

            return (count,NearestSymbols);
        }
        static Tuple<bool, bool> CheckLanguageValidity(string inputString)
        {
            Regex englishRegex = new Regex("^[a-zA-Z]+$");
            Regex russianRegex = new Regex("^[а-яА-ЯёЁ]+$");

            bool language;
            bool isValid = true;

            int englishCharCount = 0;
            int russianCharCount = 0;
            int specialCharCount = 0;

            foreach (char c in inputString)
            {
                if (englishRegex.IsMatch(c.ToString()))
                    englishCharCount++;
                else if (russianRegex.IsMatch(c.ToString()))
                    russianCharCount++;
                else
                    specialCharCount++;
            }

            if(russianCharCount > englishCharCount)
            {
                if((englishCharCount > 1 && russianCharCount > 0 && specialCharCount == 0) 
                    ||(englishCharCount >= 1  && specialCharCount >= 1 && russianCharCount > 0)
                    ||(specialCharCount > 1 && russianCharCount > 0 && englishCharCount == 0))
                {
                    isValid = false;
                    return Tuple.Create(false, false);
                }
            }
            else if(englishCharCount  > russianCharCount)
            {
                if((russianCharCount > 1 && englishCharCount > 0 && specialCharCount == 0)
                    || (russianCharCount >= 1 && specialCharCount >= 1 && englishCharCount > 0)
                    || (specialCharCount > 1 && englishCharCount > 0 && russianCharCount == 0))
                {
                    isValid = false;
                    return Tuple.Create(false, false);
                }
            }

            if (russianCharCount > englishCharCount)
            {
                return Tuple.Create(true, true);
            }
            else
            {
                return Tuple.Create(true, false);
            }
        }

    

        #endregion

        private Label title;
        private TextBox inputTextBox;
        private Label inputLanguageLabel;
        private Label OutputLanguageLabel;
        private ListBox OutputListBox;
        private Button translateButton;
        private Button ChangeLanguageButton;
        private Button ClearButton;
    }

}