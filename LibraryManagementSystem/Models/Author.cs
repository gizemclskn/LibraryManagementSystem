using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class Author
    {
        public int Id { get; set; }
       
        [Required(ErrorMessage = "İsim zorunludur")]
        [StringLength(100, ErrorMessage = "İsim 100 karakterden uzun olmamalıdır")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Doğum Tarihi zorunludur")]
        public DateTime BirthDate { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
