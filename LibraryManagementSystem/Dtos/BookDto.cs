using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.DTO
{
    public class BookDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Başlık zorunludur")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Yayın Yılı zorunludur")]
        public int PublishedYear { get; set; }

        [Required(ErrorMessage = "Fiyat zorunludur")]
        public decimal Price { get; set; }

        // Author bilgisi yalnızca Id olarak gelir
        public int AuthorId { get; set; }
    }
}
