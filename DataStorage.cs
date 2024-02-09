namespace laba1
{
    public class DataStorage
    {
        public Dictionary<string, string> russianToEnglish = new Dictionary<string, string>();
        public Dictionary<string, string> englishToRussian = new Dictionary<string, string>();

        public DataStorage()
        {

            if (russianToEnglish.Count == 0 || russianToEnglish == null)
            {
                Initialize();
            }
        }
        private void Initialize()
        {
            // Заполнение словарей русско-английскими и англо-русскими словами
            russianToEnglish.Add("яблоко", "apple");
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
            }
        }
    }
}
