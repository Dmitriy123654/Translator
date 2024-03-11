using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba1
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void SelectButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.json)|*.json";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog.FileName;
                DataStorage.CheckingJsonFromFile(selectedFilePath);

                string fileName = Path.GetFileNameWithoutExtension(selectedFilePath);
                string[] fileNames = fileName.Split('-');

                if (fileNames.Length == 2)
                {
                    string inputLanguage = fileNames[0];
                    string outputLanguage = fileNames[1];
                    Form1.inputText = inputLanguage;
                    Form1.outputText = outputLanguage;
                    Program.fm1.UpdateLanguageLabels();

                    string message = $"Выбранный вами словарь ({inputLanguage}-{outputLanguage}) был успешно загружен в память";
                    string caption = "Успех";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show(message, caption, buttons);
                }
                else
                {
                    //MessageBox.Show("Неверный формат названия файла словаря", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                this.Close();
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Close();   
        }
    }
}
