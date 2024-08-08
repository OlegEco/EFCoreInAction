namespace MyFirstEFCoreApp.Entity
{
    internal class Book
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishedOn { get; set; }
        public int AuthorID { get; set; }
        public Author Author { get; set; }
    }
}
