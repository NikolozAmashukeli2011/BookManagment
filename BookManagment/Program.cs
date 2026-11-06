using BookManagment.Helpers;
using BookManagment.Models;
using BookManagment.Services;

int choice;

do
{
    Console.WriteLine("\n1. Add new book.\n" +
                     "2. Display all books info.\n" +
                     "3. Display books by year.\n" +
                     "4. Find book by title.\n" +
                     "5. Find book by id.\n" +
                     "6. Remove book by id.\n" +
                     "7. Remove All Books.\n" +
                     "8. Exit.\n");

    string intErrTxt = "Input must be an integer, try again: ";

    choice = Prompter.PromptGeneric<int>("Input a number (1-8): ", intErrTxt);

    while (choice < 1 || choice > 8)
    {
        choice = Prompter.PromptGeneric<int>("Input must be a number 1-8. try again: ", intErrTxt);
    }

    switch (choice)
    {
        case 1: MenuActions.AddBook(); break;
        case 2: BookManager.DisplayAllCollectionsBooks(); break;
        case 3: BookManager.DisplayBooksByYear(); break;
        case 4: MenuActions.GetTitleAndSearchBook(); break;
        case 5: MenuActions.GetIdAndSearchBook(); break;
        case 6: MenuActions.GetBookAndRemove(); break;
        case 7: MenuActions.RemoveAllBooks(); break;
        case 8: break;
    }


} while (choice != 8);

