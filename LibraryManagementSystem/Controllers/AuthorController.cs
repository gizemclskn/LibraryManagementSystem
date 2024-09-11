using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorsController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            var authors = await _authorRepository.GetAllAuthors();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthor(int id)
        {
            var author = await _authorRepository.GetAuthorById(id);
            if (author == null) return NotFound(new { message = "Yazar bulunamadı" });
            return Ok(author);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] Author author)
        {
            if (author == null)
            {
                return BadRequest(new { mesaj = "Yazar verisi boş olamaz" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { mesaj = "Geçersiz yazar verisi", errors = ModelState });
            }
            author.Books = new List<Book>();

            await _authorRepository.AddAuthor(author);
            return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, Author author)
        {
            if (author == null)
            {
                return BadRequest(new { mesaj = "Yazar verisi boş olamaz" });
            }

            if (id != author.Id)
            {
                return BadRequest(new { mesaj = "Yazar ID uyuşmazlığı" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { mesaj = "Geçersiz yazar verisi", errors = ModelState });
            }

            var existingAuthor = await _authorRepository.GetAuthorById(id);
            if (existingAuthor == null)
            {
                return NotFound(new { mesaj = "Yazar bulunamadı" });
            }

            await _authorRepository.UpdateAuthor(author);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _authorRepository.GetAuthorById(id);
            if (author == null)
            {
                return NotFound(new { mesaj = "Yazar bulunamadı" });
            }

            if (author.Books.Any())
            {
                return BadRequest("Yazar silinemez çünkü yazara ait kayıtlı kitapları var.");
            }
            await _authorRepository.DeleteAuthor(id);
            return NoContent();
        }
    }
}
