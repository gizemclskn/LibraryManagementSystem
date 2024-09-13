namespace LibraryManagementSystem.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int PublishedYear { get; set; }
        public decimal Price { get; set; }
        public int AuthorId { get; set; }
        public AuthorDto Author { get; set; }
    }
}
