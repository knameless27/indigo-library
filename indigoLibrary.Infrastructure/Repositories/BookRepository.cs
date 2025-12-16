using indigoLibrary.Application.Interfaces;
using indigoLibrary.Domain.Entities;
using indigoLibrary.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace indigoLibrary.Infrastructure.Repositories
{
    public class BookRepository(IndigoLibraryDbContext context) : IBookRepository
    {
        private readonly IndigoLibraryDbContext _context = context;

        public async Task<Book?> GetByIsbnAsync(Guid isbn)
        {
            return await _context.Books.FirstOrDefaultAsync(l => l.Isbn == isbn);
        }

        public async Task AddAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }
    }
}