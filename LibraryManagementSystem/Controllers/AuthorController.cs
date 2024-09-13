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
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorsController(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            var authors = await _authorRepository.GetAllAuthors();
            var authorDtos = _mapper.Map<IEnumerable<AuthorDto>>(authors);
            return Ok(authorDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthor(int id)
        {
            var author = await _authorRepository.GetAuthorById(id);

            if (author == null)
            {
                return NotFound(new { message = "Yazar bulunamadı" });
            }

            var authorDto = _mapper.Map<AuthorDto>(author);
            return Ok(authorDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddAuthor([FromBody] AuthorDto authorDto)
        {
            if (authorDto == null)
            {
                return BadRequest(new { message = "Yazar verisi boş olamaz" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Geçersiz yazar verisi", errors = ModelState });
            }

            var author = _mapper.Map<Author>(authorDto);
            await _authorRepository.AddAuthor(author);

            // Dönüşte DTO'yu döndürmek yerine entitiye dönüş yaparak veriyi kontrol edebiliriz
            var createdAuthorDto = _mapper.Map<AuthorDto>(author);
            return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, createdAuthorDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] AuthorDto authorDto)
        {
            if (authorDto == null)
            {
                return BadRequest(new { message = "Yazar verisi boş olamaz" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Geçersiz yazar verisi", errors = ModelState });
            }

            var author = _mapper.Map<Author>(authorDto);
            author.Id = id;

            // Verinin güncellenip güncellenmediğini kontrol et
            var existingAuthor = await _authorRepository.GetAuthorById(id);
            if (existingAuthor == null)
            {
                return NotFound(new { message = "Güncellenecek yazar bulunamadı" });
            }

            await _authorRepository.UpdateAuthor(author);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var existingAuthor = await _authorRepository.GetAuthorById(id);
            if (existingAuthor == null)
            {
                return NotFound(new { message = "Silinecek yazar bulunamadı" });
            }

            await _authorRepository.DeleteAuthor(id);
            return NoContent();
        }
    }
}
