using BookManagment.Helpers;
using BookManagment.Models;

namespace BookManagment.Services
{
    static class MenuActions
    {
        private static int IdInput()
        {
            return Prompter.PromptGeneric<int>("Input book's Id: ", "Input must be an integer, try again: ");
        }

        private static string TitleInput()
        {
            string title = Prompter.PromptGeneric<string>("Input new book's title: ", "input must be a text, try again: ");

            while (string.IsNullOrWhiteSpace(title) || title.All(char.IsDigit))
            {
                title = Prompter.PromptGeneric<string>("Input mustn't be an empty text," +
                                                        " and should contain atleast 1 letter, try again: ", "input must be a text, try again: ");
            }

            return title.Trim();
        }

        private static string AuthorInput()
        {
            string author = Prompter.PromptGeneric<string>("Input new book's author: ",
                                                           "input must be a text, try again: ");

            while (string.IsNullOrWhiteSpace(author) || author.All(char.IsDigit))
            {
                author = Prompter.PromptGeneric<string>("Input mustn't be an empty text, and should contain atleast 1 letter, try again: ",
                                                        "input must be a text, try again: ");
            }

            return author.Trim();
        }

        private static int YearInput()
        {
            int yearOfPublish = Prompter.PromptGeneric<int>("Input new book's year of publish: ", "Input must be an integer, try again: ");

            while (yearOfPublish < 1 || yearOfPublish > 2026)
            {
                yearOfPublish = Prompter.PromptGeneric<int>("New book's year of publish must be above 0, and mustn't be above 2026, try again: ",
                                                            "Input must be an integer, try again: ");
            }

            return yearOfPublish;
        }

        private static Book CreateBook()
        {
            string title = TitleInput();
            string author = AuthorInput();
            int yearOfPublish = YearInput();

            return new Book(title, author, yearOfPublish);
        }

        public static void AddBook()
        {
            Book book = CreateBook();

            bool success = BookManager.AddBook(book);

            if (success)
            {
                Console.WriteLine("Book added successfully!\n");
                Console.WriteLine($"{book.ToString()}\n");
            }
            else
            {
                Console.WriteLine("Adding the book failed!\n");
            }
        }

        public static void GetTitleAndSearchBook()
        {
            string title = TitleInput();

            Book? foundBook;

            if (BookManager.GetBookByTitle(title, out foundBook))
            {
                Console.WriteLine("\nBook found.\n");
                Console.WriteLine($"{foundBook?.ToString()}\n");
            }
            else
            {
                Console.WriteLine($"Book with title: {title} couldn't be found.\n");
            }
        }

        public static void GetIdAndSearchBook()
        {
            int id = IdInput();

            Book? foundBook;

            if (BookManager.GetBookById(id, out foundBook))
            {
                Console.WriteLine("\nBook found.\n");
                Console.WriteLine($"{foundBook?.ToString()}\n");
            }
            else
            {
                Console.WriteLine($"Book with id: {id} couldn't be found.\n");
            }
        }

        public static void GetBookAndRemove()
        {
            int id = IdInput();

            if (BookManager.GetBookById(id, out Book? foundBook))
            {
                BookManager.RemoveBook(foundBook);

                Console.WriteLine("Book removed successfully!\n");
                Console.WriteLine($"{foundBook.ToString()}\n");
            }
            else
            {
                Console.WriteLine($"Book with id: {id} couldn't be found.");
            }

        }

        public static void RemoveAllBooks()
        {
            
            string choice = Prompter.PromptGeneric<string>("Are you sure that you want to remove all books? (Yes or No):",
                                                            "Input must be a text, try again: ").Trim();

            bool sure = string.Equals(choice, "Yes", StringComparison.OrdinalIgnoreCase);
            bool unSure = string.Equals(choice, "No", StringComparison.OrdinalIgnoreCase);

            while (!sure && !unSure)
            {
                choice = Prompter.PromptGeneric<string>("Input must be either yes, or no, try again: ",
                                                        "Input must be a text, try again: ").Trim();

                sure = string.Equals(choice, "Yes", StringComparison.OrdinalIgnoreCase);
                unSure = string.Equals(choice, "No", StringComparison.OrdinalIgnoreCase);
            }
            
            if(sure)
            {
                BookManager.RemoveAllBooks();
                Console.WriteLine("\nBooks removed Successfully!\n");
            }
        }

    }
}
