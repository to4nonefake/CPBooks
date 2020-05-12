using System;

namespace CourseProjectBL.Model
{
    /// <summary>
    /// Книга
    /// </summary>
    [Serializable]
    public class Book : IComparable<Book>
    {
        /// <summary>
        /// Индекс книги
        /// </summary>
        public int index { get; set; }
        /// <summary>
        /// Фамилия автора
        /// </summary>
        public string autorName { get; set; }
        /// <summary>
        /// Название книги
        /// </summary>
        public string bookName { get; set; }
        /// <summary>
        /// Год выпуска
        /// </summary>
        public ushort year { get; set; }

        public Book()
        {
            autorName = "";
            bookName = "";
            year = 0;
        }
        //TODO проверить входные параметры
        /// <summary>
        /// Переопределение сортировки
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Book other)
        {
            if (this.year > other.year) {
                return 1;
            } else if (this.year < other.year) {
                return -1;
            } else {
                return 0;
            }
        }
    }
}
