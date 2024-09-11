using LibraryManagementSystem.Data;
using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _context;

        public AuthorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Author>> GetAllAuthors()
        {
            return await _context.Authors.Include(a => a.Books).ToListAsync();
        }

        public async Task<Author> GetAuthorById(int id)
        {
            return await _context.Authors.Include(a => a.Books).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAuthor(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAuthor(Author author)
        {
            _context.Entry(author).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
        }
    }
}
