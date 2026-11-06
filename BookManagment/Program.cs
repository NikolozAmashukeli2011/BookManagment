using BookManagment.Helpers;
using BookManagment.Models;
using BookManagment.Services;

int choice;

do
{
    Console.WriteLine("\n1. Add new book.\n" +
                     "2. Display all books info.\n" +
                     "3. Find book by title.\n" +
                     "4. Find book by id.\n" +
                     "5 Remove book by id.\n" +
                     "6. Exit.\n");

    string intErrTxt = "Input must be an integer, try again: ";

    choice = Prompter.PromptGeneric<int>("Input a number (1-6): ", intErrTxt);

    while (choice < 1 || choice > 6)
    {
        choice = Prompter.PromptGeneric<int>("Input must be a number 1-6. try again: ", intErrTxt);
    }

    switch (choice)
    {
        case 1: MenuActions.AddBook(); break;
        case 2: BookManager.DisplayAllCollectionsBooks(); break;
        case 3: MenuActions.GetTitleAndSearchBook(); break;
        case 4: MenuActions.GetIdAndSearchBook(); break;
        case 5: MenuActions.GetBookAndRemove(); break;
        case 6: break;
    }


} while (choice != 6);

