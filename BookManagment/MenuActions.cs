using BookManagment.Helpers;
using BookManagment.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookManagment
{
    static class MenuActions
    {
        public static void CreateAndAddBook()
        {
            string stringErrTxt = "input must be a text, try again: ";
            string intErrTxt = "Input must be an integer, try again: ";

            string title = Prompter.PromptGeneric<string>("Input new book's title: ", stringErrTxt);

            while (string.IsNullOrWhiteSpace(title) || title.All(char.IsDigit))
            {
                title = Prompter.PromptGeneric<string>("Input mustn't be an empty text, and should contain atleast 1 letter, try again: ", stringErrTxt);
            }

            string author = Prompter.PromptGeneric<string>("Input new book's author: ", stringErrTxt);

            while (string.IsNullOrWhiteSpace(author) || author.All(char.IsDigit))
            {
                author = Prompter.PromptGeneric<string>("Input mustn't be an empty text, and should contain atleast 1 letter, try again: ", stringErrTxt);
            }

            int yearOfPublish = Prompter.PromptGeneric<int>("Input new book's year of publish: ", intErrTxt);

            while(yearOfPublish < 1 || yearOfPublish > 2026)
            {
                yearOfPublish = Prompter.PromptGeneric<int>("New book's year of publish must be above 0, and mustn't be above 2026, try again: ", intErrTxt);
            }

            Book newBook = new Book(title, author, yearOfPublish);
            BookManager.AddBook(newBook);
            Console.WriteLine($"New Book: {newBook.ToString()}\n");
        }

        public static void GetTitleAndSearchBook()
        {
            string stringErrTxt = "input must be a text, try again: ";

            string title = Prompter.PromptGeneric<string>("Input book's title: ", stringErrTxt);

            while (string.IsNullOrWhiteSpace(title) || title.All(char.IsDigit))
            {
                title = Prompter.PromptGeneric<string>("Input mustn't be an empty text, and should contain atleast 1 letter, try again: ", stringErrTxt);
            }

            Book? foundBook;

           if (BookManager.GetBookByTitle(title, out foundBook))
            {
                Console.WriteLine("\nBook found.\n");
                Console.WriteLine($"{foundBook.ToString()}\n");
            } else
            {
                Console.WriteLine($"Book with title: {title} couldn't be found.\n");
            }
        }

    }
}
