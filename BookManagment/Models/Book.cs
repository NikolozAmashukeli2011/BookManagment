namespace BookManagment.Models
{
    internal class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        private static int Count = 0;
        private int _YearOfPublish;
        public int YearOfPublish
        {
            get
            {
                return _YearOfPublish;
            }

            set
            {
                if (value < 1 || value > 2026)
                {
                    throw new ArgumentOutOfRangeException($"{nameof(value)} must be above 0, and must not be greater then 2026.");
                }
                else
                {
                    _YearOfPublish = value;
                }
            }
        }

        public Book()
        {

        }

        public Book(string title, string author, int yearOfPublish)
        {
            Count++;
            Id = Count;
            Title = title;
            Author = author;
            YearOfPublish = yearOfPublish;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Title: {Title}, Author: {Author}, Year Of Publish: {YearOfPublish}.";
        }
    }
}
