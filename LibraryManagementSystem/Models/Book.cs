﻿using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Başlık zorunludur")]
        [StringLength(200, ErrorMessage = "Başlık 200 karakterden uzun olmamalıdır")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Yayın Yılı zorunludur")]
        public int PublishedYear { get; set; }

        [Required(ErrorMessage = "Fiyat zorunludur")]
        public decimal Price { get; set; }

        // Yazar ile ilişki
        [Required]
        public int AuthorId { get; set; }

        public Author Author { get; set; }
    }
}
