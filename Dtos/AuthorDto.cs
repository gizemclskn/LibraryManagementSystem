namespace LibraryManagementSystem.Dtos
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public IEnumerable<BookDto> Books { get; set; }
    }
}
