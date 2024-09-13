using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.DTO
{
    public class AuthorDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "İsim zorunludur")]
        [StringLength(100, ErrorMessage = "İsim 100 karakterden uzun olmamalıdır")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Doğum Tarihi zorunludur")]
        public DateTime BirthDate { get; set; }

        // DTO olarak kitaplar
     //   public ICollection<BookDto> Books { get; set; }
    }
}
