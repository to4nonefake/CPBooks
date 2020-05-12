using System;
using System.Collections.Generic;
using CourseProjectBL.Controller;

namespace CourseProjectCMD
{
    class Program
    {
        private static int index = 0;
        private static int page = 0;
        private static string autorName;
        private static string bookName;
        private static ushort year;
        private static void Main(string[] args)
        {
            string selectedMenuItem = "";
            #region Описание страниц
            List<string> page1 = new List<string>() { //Создание открытие
                "Создать файл",
                "Открыть файл",
                "Выход"
            };
            List<string> page2 = new List<string>() { //Работа с файлом
                "Просмотреть данные",
                "Редактировать",
                "Фильтр",
                "Открыть другой файл",
                "Выход"
            };
            List<string> page3 = new List<string>() {//Фильтр
                "Более 2х слов в названии",
                "По фамилии в порядке возрастания годов изданий книг",
                "Назад"
            };
            List<string> page4 = new List<string>() {//Редактирование
                "Добавить запись",
                "Удалить запись",
                "Редактировать запись",
                "Назад"
            };
            #endregion
            Console.CursorVisible = false;
            var booksController = new BooksController();
            while (true) {
                if (page == 0) {
                    selectedMenuItem = drawMenu(page1);
                    Page1(selectedMenuItem, booksController);
                } else if (page == 1) {
                    selectedMenuItem = drawMenu(page2);
                    Page2(selectedMenuItem, booksController);
                } else if (page == 2) {
                    selectedMenuItem = drawMenu(page3);
                    Page3(selectedMenuItem, booksController);
                } else if (page == 3) {
                    selectedMenuItem = drawMenu(page4);
                    Page4(selectedMenuItem, booksController);
                }
            }
        }
        public static void Page1(string selectedMenuItem, BooksController BC)
        {
            if (selectedMenuItem == "Создать файл") {
                Console.Clear();
                Console.Write("Введите путь: ");
                BC.filePath = Console.ReadLine();
                Console.Write("Введите имя файла: ");
                BC.filePath += $"\\{Console.ReadLine()}.bin";
                Console.Write("Введите количество строк: ");
                int rowCounter = int.Parse(Console.ReadLine());
                Console.Clear();
                BC.ClearCache();
                for (int i = 0; i < rowCounter; i++) {
                    Console.WriteLine($"[{i}] ");
                    CreateBook();
                    BC.AddBook(BC.CreateBook(i, autorName, bookName, year));
                    Console.Clear();
                }
                BC.SaveFile(BC.filePath);
                Console.Clear();
                page++;
                index = 0;
            } else if (selectedMenuItem == "Открыть файл") {
                Console.Clear();
                Console.Write("Введите путь: ");
                string filePath = Console.ReadLine();
                BC.LoadFile(filePath);
                page++;
                index = 0;
            } else if (selectedMenuItem == "Выход") {
                Environment.Exit(0);
            }
        }
        public static void Page2(string selectedMenuItem, BooksController BC)
        {
            if (selectedMenuItem == "Просмотреть данные") {
                Console.Clear();
                foreach (var book in BC.Books) {
                    Console.WriteLine($"| {book.index,3} | {book.autorName,40} | {book.bookName,40} | {book.year,4} |");
                }
                Console.ReadKey();
                Console.Clear();
            } else if (selectedMenuItem == "Редактировать") {
                page += 2;
                index = 0;
                Console.Clear();
            } else if (selectedMenuItem == "Фильтр") {
                page++;
                index = 0;
                Console.Clear();
            } else if (selectedMenuItem == "Открыть другой файл") {
                page = 0;
                index = 0;
                Console.Clear();
            } else if (selectedMenuItem == "Выход") {
                Environment.Exit(0);
            }
        }
        public static void Page3(string selectedMenuItem, BooksController BC)
        {
            if (selectedMenuItem == "Более 2х слов в названии") {
                Console.Clear();
                foreach (var book in BC.Books) {
                    if (book.bookName.Contains(" ")) {
                        Console.WriteLine($"| {book.index,3} | {book.autorName,40} | {book.bookName,40} | {book.year,4} |");
                    }
                }
                Console.ReadKey();
                Console.Clear();
            } else if (selectedMenuItem == "По фамилии в порядке возрастания годов изданий книг") {
                Console.Clear();
                Console.Write("Введите фамилию автора: ");
                string autorName = Console.ReadLine();
                Console.Clear();
                BC.Sort(autorName);
                foreach (var book in BC.SortedBySecondName) {
                    Console.WriteLine($"| {book.index,3} | {book.autorName,40} | {book.bookName,40} | {book.year,4} |");
                }
                Console.ReadKey();
                Console.Clear();
            } else if (selectedMenuItem == "Назад") {
                page--;
                index = 0;
                Console.Clear();
            }
        }
        public static void Page4(string selectedMenuItem, BooksController BC)
        {
            if (selectedMenuItem == "Добавить запись") {
                Console.Clear();
                Console.WriteLine($"Добавление записи");
                CreateBook();
                BC.AddBookToLibrary(autorName, bookName, year);
                BC.SaveFile();
                Console.Clear();
            } else if (selectedMenuItem == "Удалить запись") {
                Console.Clear();
                Console.Write("Введите номер записи, которую хотите удалить: ");
                int rowIndex = int.Parse(Console.ReadLine());
                BC.RemoveBook(rowIndex);
                BC.SaveFile();
                Console.Clear();
            } else if (selectedMenuItem == "Редактировать запись") {
                Console.Clear();
                Console.Write("Введите номер записи, которую хотите редактировать: ");
                int rowIndex = int.Parse(Console.ReadLine());
                Console.Clear();
                Console.WriteLine($"Редактирование записи [{rowIndex}]");
                CreateBook();
                BC.UpdateBook(rowIndex, autorName, bookName, year);
                BC.SaveFile();
                Console.Clear();
            } else if (selectedMenuItem == "Назад") {
                page -= 2;
                index = 0;
                Console.Clear();
            }
        }
        /// <summary>
        /// Отрисовка меню
        /// </summary>
        /// <param name="items">Пункты меню на текущей странице</param>
        /// <returns></returns>
        private static string drawMenu(List<string> items)
        {
            for (int i = 0; i < items.Count; i++) {
                if (i == index) {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;

                    Console.WriteLine(items[i]);
                    Console.ResetColor();
                } else {
                    Console.WriteLine(items[i]);
                }

            }

            ConsoleKeyInfo ckey = Console.ReadKey();

            if (ckey.Key == ConsoleKey.DownArrow) {
                if (index == items.Count - 1) {
                    //index = 0; Возможность прокрутки
                } else { index++; }
            } else if (ckey.Key == ConsoleKey.UpArrow) {
                if (index <= 0) {
                    //index = menuItem.Count - 1; Возможность прокрутки
                } else { index--; }
            } else if (ckey.Key == ConsoleKey.Enter) {
                return items[index];
            } else {
                Console.Clear();
            }

            Console.Clear();
            return "";
        }
        /// <summary>
        /// Ввод данных для создания новой строки
        /// </summary>
        public static void CreateBook()
        {
            Console.Write("Введите фамилию автора: ");
            autorName = Console.ReadLine();
            Console.Write("Введите название книги: ");
            bookName = Console.ReadLine();
            Console.Write("Введите год издания: ");
            year = ushort.Parse(Console.ReadLine());
        }
    }
}
