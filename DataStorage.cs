using System.Text;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;

namespace laba1
{
    public class DataStorage
    {
        public static Dictionary<string, string> russianToEnglish = new Dictionary<string, string>();
        public static Dictionary<string, string> englishToRussian = new Dictionary<string, string>();
        public static string? JsonPath { get; set; }

        public DataStorage()
        {

            /*if (russianToEnglish.Count == 0 || russianToEnglish == null)
            {
                InitializeAsync();
            }*/
            CheckingJsonFromFile();
        }
        private async Task InitializeAsync()
        {
            // Заполнение словарей русско-английскими и англо-русскими словами
            /*russianToEnglish.Add("яблоко", "apple");
            russianToEnglish.Add("кот", "cat");
            russianToEnglish.Add("собака", "dog");
            russianToEnglish.Add("стол", "table");
            russianToEnglish.Add("книга", "book");
            russianToEnglish.Add("машина", "car");
            russianToEnglish.Add("дом", "house");
            russianToEnglish.Add("ручка", "pen");
            russianToEnglish.Add("школа", "school");
            russianToEnglish.Add("окно", "window");
            russianToEnglish.Add("компьютер", "computer");
            russianToEnglish.Add("стул", "chair");
            russianToEnglish.Add("гитара", "guitar");
            russianToEnglish.Add("телефон", "phone");
            russianToEnglish.Add("молоко", "milk");
            russianToEnglish.Add("класс", "class");
            russianToEnglish.Add("книжка", "booklet");
            russianToEnglish.Add("солнце", "sun");
            russianToEnglish.Add("парк", "park");
            russianToEnglish.Add("мяч", "ball");
            russianToEnglish.Add("флаг", "flag");
            russianToEnglish.Add("река", "river");
            russianToEnglish.Add("капуста", "cabbage");
            russianToEnglish.Add("банан", "banana");
            russianToEnglish.Add("дождь", "rain");
            russianToEnglish.Add("пианино", "piano");
            russianToEnglish.Add("зонт", "umbrella");
            russianToEnglish.Add("ноутбук", "laptop");
            russianToEnglish.Add("море", "sea");
            russianToEnglish.Add("музей", "museum");
            russianToEnglish.Add("звезда", "star");
            russianToEnglish.Add("птица", "bird");
            russianToEnglish.Add("космос", "space");
            russianToEnglish.Add("пароль", "password");
            russianToEnglish.Add("луна", "moon");
            russianToEnglish.Add("снег", "snow");
            russianToEnglish.Add("футбол", "football");
            russianToEnglish.Add("носок", "sock");
            russianToEnglish.Add("зебра", "zebra");
            russianToEnglish.Add("печенье", "cookie");
            russianToEnglish.Add("гора", "mountain");
            russianToEnglish.Add("планета", "planet");
            russianToEnglish.Add("цирк", "circus");
            russianToEnglish.Add("шапка", "hat");
            russianToEnglish.Add("змея", "snake");
            russianToEnglish.Add("шоколад", "chocolate");
            russianToEnglish.Add("поезд", "train");
            russianToEnglish.Add("песня", "song");
            russianToEnglish.Add("платье", "dress");
            russianToEnglish.Add("фильм", "movie");
            russianToEnglish.Add("ящик", "box");
            russianToEnglish.Add("замок", "castle");
            russianToEnglish.Add("кто", "who");
            russianToEnglish.Add("кора", "bark");

            // Заполнение англо-русского словаря на основе русско-английского
            foreach (var entry in russianToEnglish)
            {
                englishToRussian.Add(entry.Value, entry.Key);
            }*/

            /*JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            string russianJson = JsonSerializer.Serialize(russianToEnglish, options);
            string englishJson = JsonSerializer.Serialize(englishToRussian, options);

            File.WriteAllText("russianToEnglish.json", russianJson, Encoding.UTF8);
            File.WriteAllText("englishToRussian.json", englishJson, Encoding.UTF8);*/

        }
        public static bool CheckingJsonFromFile(string fileName = "russianToEnglish.json")
        {
            string basePath = Application.StartupPath;
            string filePath = Path.Combine(basePath, fileName);

            JsonPath = filePath;

            try
            {
                string json = File.ReadAllText(filePath, Encoding.UTF8);
                if (string.IsNullOrEmpty(json))
                {
                    ShowErrorMessage("Файл не содержит информации", "Были найдены ошибки в структуре JSON");
                    return false;
                }

                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    WriteIndented = true
                };

                // Проверка наличия пустых ключей
                if (!CheckEmptyKeys(json))
                {
                    return false;
                }

                russianToEnglish = JsonSerializer.Deserialize<Dictionary<string, string>>(json, options);

                // Дальнейшие действия с полученным словарем

                CreateReverseTranslation(options, fileName);
            }
            catch (JsonException ex)
            {
                ShowErrorMessage(ex.Message, "Были найдены ошибки в структуре JSON");
                return false;
            }
            catch (IOException ex)
            {
                ShowErrorMessage(ex.Message, "Возникла ошибка при чтении");
                return false;
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, "Возникла непредвиденная ошибка");
                return false;
            }
            return true;
        }

        public static bool UpdateJsonFiles(string fileName = "russianToEnglish.json")
        {
            string basePath = Application.StartupPath;
            string filePath = Path.Combine(basePath, fileName);
            string reverseFileName = Path.GetFileNameWithoutExtension(fileName) + "_reverse.json";
            string reverseFilePath = Path.Combine(basePath, reverseFileName);

            try
            {
                string json = File.ReadAllText(filePath, Encoding.UTF8);
                if (string.IsNullOrEmpty(json))
                {
                    ShowErrorMessage("Файл не содержит информации", "Были найдены ошибки в структуре JSON");
                    return false;
                }

                if (!CheckEmptyKeysInDictionary())
                {
                    return false;
                }

                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    WriteIndented = true
                };

                UpdateReverseTranslation(options, fileName, reverseFilePath);

                string updatedJson = JsonSerializer.Serialize(russianToEnglish, options);
                File.WriteAllText(filePath, updatedJson, Encoding.UTF8);
            }
            catch (JsonException ex)
            {
                ShowErrorMessage(ex.Message, "Были найдены ошибки в структуре JSON");
                return false;
            }
            catch (IOException ex)
            {
                ShowErrorMessage(ex.Message, "Возникла ошибка при чтении");
                return false;
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, "Возникла непредвиденная ошибка");
                return false;
            }
            return true;
        }

        private static bool CheckEmptyKeys(string json)
        {
            JObject jsonObject = JObject.Parse(json);
            foreach (var property in jsonObject.Properties())
            {
                if (property.Name == "")
                {
                    throw new JsonException("Найден пустой ключ в JSON");
                }
            }
            return true;
        }

        private static void CreateReverseTranslation(JsonSerializerOptions options, string fileName)
        {
            englishToRussian = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> pair in russianToEnglish)
            {
                englishToRussian[pair.Value] = pair.Key;
            }

            string reverseJson = JsonSerializer.Serialize(englishToRussian, options);
            string reverseFileName = Path.GetFileNameWithoutExtension(fileName) + "_reverse.json";
            string reverseFilePath = Path.Combine(Application.StartupPath, reverseFileName);
            File.WriteAllText(reverseFilePath, reverseJson, Encoding.UTF8);
        }

        private static bool CheckEmptyKeysInDictionary()
        {
            foreach (var pair in russianToEnglish)
            {
                if (string.IsNullOrEmpty(pair.Key))
                {
                    throw new JsonException("Найден пустой ключ в словаре");
                }
            }
            return true;
        }

        private static void UpdateReverseTranslation(JsonSerializerOptions options, string fileName, string reverseFilePath)
        {
            englishToRussian = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> pair in russianToEnglish)
            {
                englishToRussian[pair.Value] = pair.Key;
            }

            string reverseJson = JsonSerializer.Serialize(englishToRussian, options);
            File.WriteAllText(reverseFilePath, reverseJson, Encoding.UTF8);
        }

        private static void ShowErrorMessage(string message, string caption)
        {
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            MessageBox.Show(message, caption, buttons);
        }

    }
}
