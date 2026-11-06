using BookManagment.Services;

namespace BookManagment.Models
{
    static class BookManager
    {
        public static HashSet<Book> SetOfBooks { get; set; } = new HashSet<Book>();
        public static Dictionary<string, HashSet<Book>> AuthorToBooksSetMap { get; set; } = new Dictionary<string, HashSet<Book>>();
        public static Dictionary<string, Book> TitleToBookMap { get; set; } = new Dictionary<string, Book>();
        public static Dictionary<int, Book> IdToBookMap { get; set; } = new Dictionary<int, Book>();
        public static SortedList<int, HashSet<Book>> YearToBooksSortedList { get; set; } = new SortedList<int, HashSet<Book>>();

        static BookManager()
        {
            foreach (Book b in BookStorage.Load())
            {
                AddToAll(b);
            }
        }

        private static void AddToAll(Book b)
        {
            if (!AuthorToBooksSetMap.TryGetValue(b.Author, out HashSet<Book>? booksSet))
            {
                booksSet = new HashSet<Book>();
                AuthorToBooksSetMap[b.Author] = booksSet;
            }

            if (!YearToBooksSortedList.TryGetValue(b.YearOfPublish, out HashSet<Book>? booksByYear))
            {
                booksByYear = new HashSet<Book>();
                YearToBooksSortedList[b.YearOfPublish] = booksByYear;
            }

            booksSet.Add(b);
            booksByYear.Add(b);
            TitleToBookMap.Add(b.Title, b);
            SetOfBooks.Add(b);
            IdToBookMap.Add(b.Id, b);
        }


        private static bool BookExists(Book b)
        {
            if (IdToBookMap.ContainsKey(b.Id)) return true; return false;
        }


        public static bool AddBook(Book book)
        {
            if (BookExists(book))
            {
                return false;
            }
            else
            {
                AddToAll(book);
                BookStorage.Save(SetOfBooks);
                return true;
            }
        }

        public static void DisplayAllCollectionsBooks()
        {
            if (SetOfBooks.Count == 0)
            {
                Console.WriteLine("There are no books.");
            }
            else
            {
                Console.WriteLine("\n[SET OF BOOKS]\n");

                foreach (Book book in SetOfBooks)
                {
                    Console.WriteLine($"{book.ToString()}\n");
                }
            }

        }

        public static bool GetBookByTitle(string title, out Book? foundBook)
        {
            TitleToBookMap.TryGetValue(title, out foundBook);
            return foundBook != null;
        }

        public static bool GetBookById(int id, out Book? foundBook)
        {
            IdToBookMap.TryGetValue(id, out foundBook);
            return foundBook != null;
        }

        public static bool RemoveBook(Book book)
        {
            if (BookExists(book))
            {
                AuthorToBooksSetMap.TryGetValue(book.Author, out HashSet<Book>? authorSet);
                authorSet?.Remove(book);

                SetOfBooks.Remove(book);
                TitleToBookMap.Remove(book.Title);
                IdToBookMap.Remove(book.Id);

                BookStorage.Save(SetOfBooks);

                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
