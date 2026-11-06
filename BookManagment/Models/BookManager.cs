using BookManagment.Services;

namespace BookManagment.Models
{
    static class BookManager
    {
        public static HashSet<Book> SetOfBooks { get; set; } = new HashSet<Book>();
        public static Dictionary<string, HashSet<Book>> AuthorToBooksSetMap { get; set; } = new Dictionary<string, HashSet<Book>>(StringComparer.OrdinalIgnoreCase);
        public static Dictionary<string, Book> TitleToBookMap { get; set; } = new Dictionary<string, Book>(StringComparer.OrdinalIgnoreCase);
        public static Dictionary<int, Book> IdToBookMap { get; set; } = new Dictionary<int, Book>();
        public static SortedList<int, HashSet<Book>> YearToBooksSortedList { get; set; } = new SortedList<int, HashSet<Book>>();

        static BookManager()
        {
            BookStorageData data = BookStorage.Load();

            Book.Count = data.IdCount;

            foreach (Book b in data.Books)
            {
                AddToAll(b);
            }
        }

        private static void SaveAll()
        {
            BookStorage.Save(new BookStorageData(Book.Count, SetOfBooks));
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
            if (IdToBookMap.ContainsKey(b.Id) || TitleToBookMap.ContainsKey(b.Title)) return true; return false;
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
                SaveAll();
                return true;
            }
        }

        public static void DisplayAllCollectionsBooks()
        {
            if (SetOfBooks.Count == 0)
            {
                Console.WriteLine("\nThere are no books.");
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

        public static void DisplayBooksByYear()
        {
            if (YearToBooksSortedList.Count == 0)
            {
                Console.WriteLine("\nThere are no books.");
            }
            else
            {
                Console.WriteLine("\n[BOOKS BY YEAR]\n");

                foreach ((int year, HashSet<Book> books) in YearToBooksSortedList)
                {
                    Console.WriteLine($"[{year}]\n");

                    foreach (Book book in books)
                    {
                        Console.WriteLine($"{book.ToString()}\n");
                    }
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

                if (authorSet?.Count <= 1)
                {
                    AuthorToBooksSetMap.Remove(book.Author);
                } else 
                {
                    authorSet?.Remove(book);
                }

                YearToBooksSortedList.TryGetValue(book.YearOfPublish, out HashSet<Book>? yearSet);

                if (yearSet?.Count <= 1)
                {
                    YearToBooksSortedList.Remove(book.YearOfPublish);
                } else
                {
                    yearSet?.Remove(book);
                }

                SetOfBooks.Remove(book);
                TitleToBookMap.Remove(book.Title);
                IdToBookMap.Remove(book.Id);

                SaveAll();

                return true;
            }
            else
            {
                return false;
            }
        }

        public static void RemoveAllBooks()
        {
            SetOfBooks.Clear();
            TitleToBookMap.Clear();
            IdToBookMap.Clear();
            AuthorToBooksSetMap.Clear();
            YearToBooksSortedList.Clear();

            SaveAll();
        }

    }
}
