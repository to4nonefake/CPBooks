using CourseProjectBL.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace CourseProjectBL.Controller
{
    public class BooksController
    {
        private List<Book> books;
        private List<Book> sortedBySecondName;
        public List<Book> Books { get { return books; } }
        public List<Book> SortedBySecondName { get { return sortedBySecondName; } }
        public string filePath;
        /// <summary>
        /// Инициализация коллекции
        /// </summary>
        public BooksController()
        {
            books = new List<Book>();
        }
        /// <summary>
        /// Создание записей для нового файла
        /// </summary>
        /// <param name="book">Информация о записи</param>
        public void AddBook(Book book)
        {
            books.Add(book);
        }
        /// <summary>
        /// Добавление новой записи к уже существующим
        /// </summary>
        /// <param name="autorName">Фамилия автора</param>
        /// <param name="bookName">Название книги</param>
        /// <param name="year">Год выпуска</param>
        public void AddBookToLibrary(string autorName, string bookName, ushort year)
        {
            books.Add(CreateBook(books.Last<Book>().index + 1, autorName, bookName, year));
        }
        /// <summary>
        /// Создание новой записи
        /// </summary>
        /// <param name="i">Индекс</param>
        /// <param name="autorName">Фамилия автора</param>
        /// <param name="bookName">Название книги</param>
        /// <param name="year">Год выпуска</param>
        /// <returns></returns>
        public Book CreateBook(int i, string autorName, string bookName, ushort year)
        {
            Book cache = new Book();
            cache.index = i;
            cache.autorName = autorName;
            cache.bookName = bookName;
            cache.year = year;
            return cache;
        }
        /// <summary>
        /// Удаление записи
        /// </summary>
        /// <param name="rowIndex">Индекс удаляемой записи</param>
        public void RemoveBook(int rowIndex)
        {
            int findIndex = books.FindIndex(c => c.index == rowIndex);
            books.RemoveAt(findIndex);
        }
        /// <summary>
        /// Обновление записи
        /// </summary>
        /// <param name="rowIndex">Индекс</param>
        /// <param name="autorName">Фамилия автора</param>
        /// <param name="bookName">Название книги</param>
        /// <param name="year">Год выпуска</param>
        public void UpdateBook(int rowIndex, string autorName, string bookName, ushort year)
        {
            int findIndex = books.FindIndex(c => c.index == rowIndex);
            books[findIndex] = CreateBook(rowIndex, autorName, bookName, year);
        }
        /// <summary>
        /// Фильтрация по автору и сортировка по дате выпуска
        /// </summary>
        /// <param name="autorName">Фамилия автора</param>
        public void Sort(string autorName)
        {
            sortedBySecondName = new List<Book>();
            foreach (var book in books) {
                if (book.autorName == autorName) {
                    sortedBySecondName.Add(book);
                }
            }
            sortedBySecondName.Sort();
        }
        /// <summary>
        /// Загрузка файла
        /// </summary>
        /// <param name="path">Полный путь</param>
        public void LoadFile(string path)
        {
            filePath = path;
            var binaryFormatter = new BinaryFormatter();
            using (var file = new FileStream(filePath, FileMode.Open)) {
                books = binaryFormatter.Deserialize(file) as List<Book>;
            }
            Console.Clear();
        }
        /// <summary>
        /// Очистка списка
        /// </summary>
        public void ClearCache()
        {
            books = new List<Book>();
        }
        /// <summary>
        /// Обновление файла
        /// </summary>
        public void SaveFile()
        {
            DoSave(filePath);
        }
        /// <summary>
        /// Сохранение файла
        /// </summary>
        /// <param name="path">Полный путь сохранения</param>
        public void SaveFile(string path)
        {
            DoSave(path);
        }
        private void DoSave(string path)
        {
            var binaryFormatter = new BinaryFormatter();
            using (var file = new FileStream(path, FileMode.Create)) {
                binaryFormatter.Serialize(file, books);
            }
        }
    }
}
