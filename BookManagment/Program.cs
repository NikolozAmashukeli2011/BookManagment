using BookManagment.Helpers;
using BookManagment.Models;
using BookManagment.Services;

int choice;

do
{
    Console.WriteLine("\n1. Add new book.\n" +
                     "2. Display all books info.\n" +
                     "3. Find book by its title.\n" +
                     "4. Exit.\n");

    string intErrTxt = "Input must be an integer, try again: ";

    choice = Prompter.PromptGeneric<int>("Input a number (1-4): ", intErrTxt);

    while (choice < 1 || choice > 4)
    {
        choice = Prompter.PromptGeneric<int>("Input must be a number 1-4. try again: ", intErrTxt);
    }

    switch (choice)
    {
        case 1: MenuActions.CreateAndAddBook(); break;
        case 2: BookManager.DisplayAllCollectionsBooks(); break;
        case 3: MenuActions.GetTitleAndSearchBook(); break;
        case 4: break;
    }


} while (choice != 4);

