using BookManagment.Helpers;
using BookManagment.Models;

namespace BookManagment.Services
{
    static class MenuActions
    {
        private static string TitleInput()
        {
            string title = Prompter.PromptGeneric<string>("Input new book's title: ", "input must be a text, try again: ");

            while (string.IsNullOrWhiteSpace(title) || title.All(char.IsDigit))
            {
                title = Prompter.PromptGeneric<string>("Input mustn't be an empty text," +
                                                        " and should contain atleast 1 letter, try again: ", "input must be a text, try again: ");
            }

            return title;
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

            return author;
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

        public static void CreateAndAddBook()
        {
            string title = TitleInput();
            string author = AuthorInput();
            int yearOfPublish = YearInput();

            Book newBook = new Book(title, author, yearOfPublish);
            BookManager.AddBook(newBook);
            Console.WriteLine($"New Book: {newBook.ToString()}\n");
        }

        public static void GetTitleAndSearchBook()
        {
            string title = TitleInput();

            Book? foundBook;

            if (BookManager.GetBookByTitle(title, out foundBook))
            {
                Console.WriteLine("\nBook found.\n");
                Console.WriteLine($"{foundBook.ToString()}\n");
            }
            else
            {
                Console.WriteLine($"Book with title: {title} couldn't be found.\n");
            }
        }

    }
}
