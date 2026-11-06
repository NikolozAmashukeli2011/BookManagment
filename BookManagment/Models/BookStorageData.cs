namespace BookManagment.Models
{
    internal class BookStorageData
    {
        public int IdCount { get; set; }
        public IEnumerable<Book> Books { get; set; } = new List<Book>();

        public BookStorageData() { }

        public BookStorageData(int idCount, IEnumerable<Book> books)
        {
            IdCount = idCount;
            Books = books;
        }

    }
}
