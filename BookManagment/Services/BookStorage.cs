using BookManagment.Models;
using System.Text.Json;

namespace BookManagment.Services
{
    internal static class BookStorage
    {
        private static readonly string FilePath = "book.json";
        private static readonly JsonSerializerOptions options = new JsonSerializerOptions() { WriteIndented = true };

        public static void Save(BookStorageData bookData)
        {
            string json = JsonSerializer.Serialize(bookData, options);

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

        public static BookStorageData Load()
        {
            if (!File.Exists(FilePath)) return new BookStorageData();

            try
            {
                using (StreamReader sr = new StreamReader(FilePath))
                {
                    string json = sr.ReadToEnd();
                    return JsonSerializer.Deserialize<BookStorageData>(json, options) ?? new BookStorageData();
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Couldn't load. {ex.Message}.");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"You don't have permission to access this file. {ex.Message}.");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"The text isn't a valid JSON. {ex.Message}.");
            }

            return new BookStorageData();
        }
    }
}
