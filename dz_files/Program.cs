using System.Collections;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks.Dataflow;
using static System.Net.Mime.MediaTypeNames;

namespace dz_files
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            PoemList obj = new PoemList();
            bool c = true;
            do
            {
                Console.WriteLine("1. Добавить новый стих.");
                Console.WriteLine("2. Удалить стих.");
                Console.WriteLine("3. Показать все стихи.");
                Console.WriteLine("4. Поиск стихов.");
                Console.WriteLine("5. Изменить стихи.");
                Console.WriteLine("6. Сохранить коллекцию стихов в файл.");
                Console.WriteLine("7. Загрузить коллекцию из файла.");
                Console.WriteLine("8. Отчеты.");
                Console.WriteLine("9. Выйти.");
                Console.Write("Введите нужный пункт меню: ");
                int user_choice = Int32.Parse(Console.ReadLine());
                switch (user_choice)
                {
                    case 1:
                        obj.AddPoem();
                        break;
                    case 2:
                        obj.RemovePoem();
                        break;
                    case 3:
                        obj.ShowList();
                        break;
                    case 4:
                        obj.SearchPoem();
                        break;
                    case 5:
                        obj.ChangeInfo();
                        break;
                    case 6:
                        obj.SaveListToFile();
                        break;
                    case 7:
                        obj.LoadFromFile();
                        break;
                    case 8:
                        obj.Reports();
                        break;
                    case 9:
                        c = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }
            } while (c);
        }
    }

    class Poem
    {
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public int Year { get; set; }
        public string PoemText { get; set; }
        public string PoemTheme { get; set; }

        public Poem() { }
        public void SetPoem()
        {
            Console.WriteLine("Введите название стиха:");
            Title = Console.ReadLine();
            Console.WriteLine("Введите имя автора:");
            AuthorName = Console.ReadLine();
            Console.WriteLine("Введите год написания:");
            Year = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Введите текст стиха:");
            string str = null;
            do
            {
                str = Console.ReadLine();
                if (str != "")
                    PoemText += string.Format(str + "\n");
            } while (str != "");
            Console.WriteLine("Введите тему стиха:");
            PoemTheme = Console.ReadLine();
        }

        public void ShowPoem()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"Title: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"{Title}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"Author Name: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"{AuthorName}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"Year of edition: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"{Year}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"Text of the poem:: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"\n{PoemText}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"Poem Theme: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"{PoemTheme}");
            Console.ForegroundColor = ConsoleColor.White;;
        }
    }

    class PoemList : Poem
    {
        List<Poem> list;

        public PoemList()
        {
            list = new List<Poem>();
        }

        public void AddPoem()
        {
            Poem poem = new Poem();
            poem.SetPoem();
            list.Add(poem);
        }

       
        public void RemovePoem()
        {
            //Console.WriteLine("Введите номер стиха для удаления");
            //int num = Int32.Parse(Console.ReadLine());
            //list.RemoveAt(1);

            Console.WriteLine("Введите название стиха для удаления");
            string str = Console.ReadLine();
                       
            foreach(Poem poem in list)
            {
                if(poem.Title == str)
                {
                    list.Remove(poem);
                    return;
                }
            }           
        }

        public void ChangeInfo()
        {
            bool c = true;
            do
            {
                Console.WriteLine("1. Изменить название.");
                Console.WriteLine("2. Изменить автора.");
                Console.WriteLine("3. Изменить год.");
                Console.WriteLine("4. Изменить тему.");
                Console.WriteLine("5. Выход.");

                int user_choice = Int32.Parse(Console.ReadLine());
                switch (user_choice)
                {
                    case 1:
                        Console.WriteLine("Введите новое название:" +
                           "\n(ввести название стиха, для которого нужны изменения, вы сможете в следующем шаге)");
                        string title = Console.ReadLine();
                        SearchByTitle().Title=title;
                        break;
                    case 2:
                        Console.WriteLine("Введите нового автора:" +
                            "\n(ввести название стиха, для которого нужны изменения, вы сможете в следующем шаге)");
                        string author = Console.ReadLine();
                        SearchByTitle().AuthorName=author;
                       break;
                    case 3:
                        Console.WriteLine("Введите новый год издания:" +
                             "\n(ввести название стиха, для которого нужны изменения, вы сможете в следующем шаге)");
                        int year = Int32.Parse(Console.ReadLine());
                        SearchByTitle().Year=year;
                        break;
                    case 4:
                        Console.WriteLine("Введите новую тему:" +
                           "\n(ввести название стиха, для которого нужны изменения, вы сможете в следующем шаге)");
                        string theme = Console.ReadLine();
                        SearchByTitle().PoemTheme=theme;
                        break;
                    case 5:
                        c = false;
                        break;
                }

            } while (c);
        }

        public Poem SearchByTitle()
        {           
            Console.WriteLine("Введите название стиха:");
            string str = Console.ReadLine();
            foreach(Poem poem in list)
            {
                if(poem.Title == str)
                {
                    //poem.ShowPoem();
                    return poem;
                }
            }
            return this;
        }

        public void SearchPoem()
        {
            bool c = true;
            do
            {
                Console.WriteLine("1. Искать стих по названию.");
                Console.WriteLine("2. Искать стих по автору.");
                Console.WriteLine("3. Искать стих по году издания.");
                Console.WriteLine("4. Искать стих по теме.");
                Console.WriteLine("5. Выход.");

                int user_choice = Int32.Parse(Console.ReadLine());
                switch (user_choice)
                {
                    case 1:
                        Console.WriteLine("Введите название стиха:");
                        string title = Console.ReadLine();
                        foreach (Poem poem in list)
                            if (poem.Title == title)
                                poem.ShowPoem();
                        break;
                    case 2:
                        Console.WriteLine("Введите автора стиха:");
                        string author = Console.ReadLine();
                        foreach (Poem poem in list)
                            if (poem.AuthorName == author)
                                poem.ShowPoem();
                        break;
                    case 3:
                        Console.WriteLine("Введите год издания стиха:");
                        int year = Int32.Parse(Console.ReadLine());
                        foreach (Poem poem in list)
                            if (poem.Year == year)
                                poem.ShowPoem();
                        break;
                    case 4:
                        Console.WriteLine("Введите тему стиха:");
                        string theme = Console.ReadLine();
                        foreach (Poem poem in list)
                            if (poem.PoemTheme == theme)
                                poem.ShowPoem();
                        break;
                    case 5:
                        c = false;
                        break;
                }
            } while (c);
        }

        public void ShowList()
        {
            int i = 1;
            foreach (Poem poem in list)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Стих № " + i++);
                Console.ForegroundColor = ConsoleColor.White;
                poem.ShowPoem();
                Console.WriteLine();
            }
        }

        public void ShowListforRep(List<Poem>rep)
        {
            int i = 1;
            foreach (Poem poem in rep)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Стих № " + i++);
                Console.ForegroundColor = ConsoleColor.White;
                poem.ShowPoem();
                Console.WriteLine();
            }
        }

        public void SaveListToFile()
        {
            Console.WriteLine("Введите название файла для сохранения" +
                "\n(сейчас в программе присутсвуют файл с названием poems.txt");
            string fileName = Console.ReadLine();
            string json = JsonSerializer.Serialize(list);
            File.WriteAllText(fileName, json);
            Console.WriteLine("Коллекция стихов сохранена в файл.");
        }

        public void LoadFromFile()
        {
            Console.WriteLine("Введите название файла, откуда загрузить стихи" +
                "\n(сейчас в программе присутсвуют файл с названием poems.txt");
            string fileName = Console.ReadLine();
            if (File.Exists(fileName))
            {
                string json = File.ReadAllText(fileName);
                list = JsonSerializer.Deserialize<List<Poem>>(json);
                Console.WriteLine("Коллекция стихов успешно загружена из файла.");
            }
            else
            {
                Console.WriteLine("Файл не найден.");
            }
        }

        public void SavePrintReport(List<Poem>poems)
        {
            Console.WriteLine("1. Вывести отчет на экран.");
            Console.WriteLine("2. Сохранить отчет в файл."); 
            int user_choice = Int32.Parse(Console.ReadLine());
            switch(user_choice)
            {
                case 1:
                    ShowListforRep(poems);
                    break;
                case 2:
                    Console.WriteLine("Введите название файла для сохранения" +
              "\n(сейчас в программе присутсвуют файл с названием poems.txt");
                    string fileName = Console.ReadLine();
                    string json = JsonSerializer.Serialize(poems);
                    File.WriteAllText(fileName, json);
                    Console.WriteLine("Отчет по названию сохранен в файл.");
                    break;
            }
        }

        public void ReportByTitle()
        {
            Console.WriteLine("Введите название стиха для генерации отчета по названию:");
            string title = Console.ReadLine();
            List<Poem> poems_by_title = new List<Poem>();
            foreach (var poem in list)
            {
                if (poem.Title == title)
                {
                    poems_by_title.Add(poem);
                   
                }
            }   
            if(poems_by_title.Count>0)
                SavePrintReport(poems_by_title);
            else
                Console.WriteLine($"Стихи с названием '{title}' не найдены.");            
        }

        public void ReportByAuthor()
        {
            Console.WriteLine("Введите автора стиха для генерации отчета по автору:");
            string author = Console.ReadLine();
            List<Poem> poems_by_author = new List<Poem>();
            foreach (var poem in list)
                if (poem.AuthorName == author)
                    poems_by_author.Add(poem);

            if (poems_by_author.Count>0)
                SavePrintReport(poems_by_author);
            else
                Console.WriteLine($"Стихи с автором '{author}' не найдены.");
        }

        public void ReportByTheme()
        {
            Console.WriteLine("Введите тему стиха для генерации отчета по теме:");
            string theme = Console.ReadLine();
            List<Poem> poems_by_theme = new List<Poem>();
            foreach (var poem in list)
                if (poem.PoemTheme == theme)
                    poems_by_theme.Add(poem);

            if (poems_by_theme.Count>0)
                SavePrintReport(poems_by_theme);
            else
                Console.WriteLine($"Стихи с темой '{theme}' не найдены.");
        }

        public void ReportByYear()
        {
            Console.WriteLine("Введите год издания стиха для генерации отчета по году:");
            int year = Int32.Parse(Console.ReadLine());
            List<Poem> poems_by_year = new List<Poem>();
            foreach (var poem in list)
                if (poem.Year == year)
                    poems_by_year.Add(poem);

            if (poems_by_year.Count>0)
                SavePrintReport(poems_by_year);
            else
                Console.WriteLine($"Стихи с годом издания '{year}' не найдены.");
        }

        public void ReportByWordInText()
        {
            Console.WriteLine("Введите слово стиха для генерации отчета по слову в тексте:");
            string word = Console.ReadLine();
            List<Poem> poems_by_word = new List<Poem>();
            foreach (var poem in list)
                if (poem.PoemText.Contains(word))
                    poems_by_word.Add(poem);

            if (poems_by_word.Count>0)
                SavePrintReport(poems_by_word);
            else
                Console.WriteLine($"Стихи со словом '{word}' в тексте не найдены.");
        }

        //public void ReportByLengthOfText()
        //{
        //    Console.WriteLine("Введите кол-во строк для генерации отчета по длине" +
        //        "(будут сгенерированы стихи с большим кол-вом строк)");
        //    int num = Int32.Parse(Console.ReadLine());
        //    List<Poem> poems_by_len = new List<Poem>();
        //    foreach (var poem in list)
        //    {
        //        int count = 0;
        //        if (poem.PoemText.Contains("\n"))
        //        {
        //            count ++;
        //            if(count > num)
        //            {
        //                poems_by_len.Add(poem);
        //            }
        //        }
        //        Console.WriteLine($"Count = {count}");
        //    }
             
        //    if (poems_by_len.Count>0)
        //        SavePrintReport(poems_by_len);
        //    else
        //        Console.WriteLine($"Стихи с кол-вом строк больше '{num}' в тексте не найдены.");
        //}

        public void Reports()
        {
            bool c = true;
            do
            {
                Console.WriteLine("1. Отчет по названию.");
                Console.WriteLine("2. Отчет по автору.");
                Console.WriteLine("3. Отчет по теме.");
                Console.WriteLine("4. Отчет по году издания.");
                Console.WriteLine("5. Отчет по слову в тексте.");
                Console.WriteLine("7. Выход.");

                int user_choice = Int32.Parse(Console.ReadLine());
                switch(user_choice)
                {
                    case 1:
                        ReportByTitle();
                        break;
                    case 2:
                        ReportByAuthor();
                        break;
                    case 3:
                        ReportByTheme();
                        break;
                    case 4:
                        ReportByYear();
                        break;
                    case 5:
                        ReportByWordInText();
                        break;
                    case 7:
                        c = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            } while (c);
        }
    }
}