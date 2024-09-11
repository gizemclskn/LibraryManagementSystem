using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Başlık zorunludur")]
        [StringLength(200, ErrorMessage = "Başlık 200 karakterden uzun olmamalıdır")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Yayın Yılı zorunludur")]
        [Range(1, int.MaxValue, ErrorMessage = "Yayın Yılı 0'dan büyük olmalıdır")]
        public int PublishedYear { get; set; }

        [Required(ErrorMessage = "Fiyat zorunludur")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Fiyat 0'dan büyük olmalıdır")]
        public decimal Price { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
