using BookManagment.Models;
using System.Text.Json;

namespace BookManagment.Services
{
    internal static class BookStorage
    {
        private static readonly string FilePath = "book.json";
        private static readonly JsonSerializerOptions options = new JsonSerializerOptions() { WriteIndented = true };

        public static void Save(IEnumerable<Book> books)
        {
            string json = JsonSerializer.Serialize(FilePath, options);

            try
            {
                using (StreamWriter sw = new StreamWriter(FilePath))
                {
                    sw.Write(json);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Couldn't save. {ex.Message}.");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"You don't have permission to modify the file. {ex.Message}.");
            }
        }

        public static IEnumerable<Book> Load()
        {
            if (!File.Exists(FilePath)) return Enumerable.Empty<Book>();
            
            try
            {
                using(StreamReader sr = new StreamReader(FilePath))
                {
                    string json = sr.ReadToEnd();
                    return JsonSerializer.Deserialize<IEnumerable<Book>>(json) ?? Enumerable.Empty<Book>();
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Couldn't save. {ex.Message}.");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"You don't have permission to access this file. {ex.Message}.");
            } catch (JsonException ex)
            {
                Console.WriteLine($"The text isn't a valid JSON. {ex.Message}.");
            }

            return Enumerable.Empty<Book>();
        }
    }
}
