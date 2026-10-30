using System;
using System.Collections.Generic;
using System.Text;

namespace BookManagment.Models
{
    static class BookManager
    {
        public static HashSet<Book> SetOfBooks { get; set; } = new HashSet<Book>();
        public static Dictionary<string, HashSet<Book>> AuthorToBooksSetMap { get; set; } = new Dictionary<string, HashSet<Book>>();
        public static Dictionary<string, Book> TitleToBookMap { get; set; } = new Dictionary<string, Book>();
        public static SortedList<int, Book> YearToBookSortedList { get; set; } = new SortedList<int, Book>();

        public static bool AddBook(Book book)
        {
            if(!SetOfBooks.Add(book))
            {
                return false;
            }

            if (AuthorToBooksSetMap.TryGetValue(book.Author, out HashSet<Book> authorsBooksSet))
            {
                if(!authorsBooksSet.Add(book))
                {
                    return false;
                }
            } else
            {
                HashSet<Book> newSetOfBooks = new HashSet<Book>() { book };
                AuthorToBooksSetMap.Add(book.Author, newSetOfBooks);
            }

            if(!TitleToBookMap.ContainsKey(book.Title))
            {
                TitleToBookMap.Add(book.Title, book);
            }

            if(!YearToBookSortedList.ContainsKey(book.YearOfPublish))
            {
                YearToBookSortedList.Add(book.YearOfPublish, book);
            }

            return true;
        }

        public static void DisplayBooks()
        {
            Console.WriteLine("\n[SET OF BOOKS]\n");

            foreach(Book book in SetOfBooks)
            {
                Console.WriteLine($"{book.ToString()}\n");
            }

            Console.WriteLine("\n[AUTHOR TO BOOKS SET MAP]\n");

            foreach(HashSet<Book> setOfBooks in AuthorToBooksSetMap.Values)
            {
                foreach(Book book in setOfBooks)
                {
                    Console.WriteLine($"{book.ToString()}\n");
                }
            }

            Console.WriteLine("\n[TITLE TO BOOK MAP]\n");

            foreach ((string title, Book book) in TitleToBookMap)
            {
                Console.WriteLine($"{book.ToString()}\n");
            }

            Console.WriteLine("\n[YEAR TO BOOK SORTED LIST]\n");

            foreach(Book book in YearToBookSortedList.Values)
            {
                Console.WriteLine($"{book.ToString()}\n");
            }
        }

        public static bool GetBookByTitle(string title, out Book? foundBook)
        {
           TitleToBookMap.TryGetValue(title, out foundBook);
            return foundBook != null;
        }

    }
}
