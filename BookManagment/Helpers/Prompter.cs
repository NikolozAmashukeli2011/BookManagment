using System.Globalization;

namespace BookManagment.Helpers
{
    static class Prompter
    {
        public static T PromptGeneric<T>(string startingText, string errorText) where T : IParsable<T>
        {
            T? result;

            Console.WriteLine(startingText);
            string? input = Console.ReadLine();

            while (!T.TryParse(input, CultureInfo.InvariantCulture, out result))
            {
                Console.WriteLine(errorText);
                input = Console.ReadLine();
            }

            return result;
        }
    }
}
