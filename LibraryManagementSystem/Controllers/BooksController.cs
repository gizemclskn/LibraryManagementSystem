using AutoMapper;
using LibraryManagementSystem.DTO;
using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public BooksController(IBookRepository bookRepository, IAuthorRepository authorRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        // Tüm kitapları getir
        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _bookRepository.GetAllBooks();
            var bookDtos = _mapper.Map<IEnumerable<BookDto>>(books);
            return Ok(bookDtos);
        }

        // ID'ye göre kitap getir
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _bookRepository.GetBookById(id);
            if (book == null)
            {
                return NotFound(new { message = "Kitap bulunamadı" });
            }

            var bookDto = _mapper.Map<BookDto>(book);
            return Ok(bookDto);
        }

        // Kitap ekle
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] BookDto bookDto)
        {
            if (bookDto == null)
            {
                return BadRequest(new { message = "Kitap verisi boş olamaz" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Geçersiz kitap verisi", errors = ModelState });
            }

            var author = await _authorRepository.GetAuthorById(bookDto.AuthorId);
            if (author == null)
            {
                return BadRequest(new { message = "Geçersiz yazar ID'si." });
            }

            var book = _mapper.Map<Book>(bookDto);
            await _bookRepository.AddBook(book);

            var createdBookDto = _mapper.Map<BookDto>(book);
            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, createdBookDto);
        }

        // Kitabı güncelle
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookDto bookDto)
        {
            if (bookDto == null)
            {
                return BadRequest(new { message = "Kitap verisi boş olamaz" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Geçersiz kitap verisi", errors = ModelState });
            }

            var existingBook = await _bookRepository.GetBookById(id);
            if (existingBook == null)
            {
                return NotFound(new { message = "Güncellenecek kitap bulunamadı" });
            }

            var author = await _authorRepository.GetAuthorById(bookDto.AuthorId);
            if (author == null)
            {
                return BadRequest(new { message = "Geçersiz yazar ID'si." });
            }

            _mapper.Map(bookDto, existingBook);
            await _bookRepository.UpdateBook(existingBook);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _bookRepository.GetBookById(id);
            if (book == null)
            {
                return NotFound(new { message = "Silinecek kitap bulunamadı" });
            }

            await _bookRepository.DeleteBook(id);
            return NoContent();
        }
    }
}
