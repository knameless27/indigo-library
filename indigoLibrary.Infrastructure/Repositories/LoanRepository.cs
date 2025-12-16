using indigoLibrary.Application.Interfaces;
using indigoLibrary.Domain.Entities;
using indigoLibrary.Domain.Enums;
using indigoLibrary.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace indigoLibrary.Infrastructure.Repositories
{
    public class LoanRepository(IndigoLibraryDbContext context) : ILoanRepository
    {
        private readonly IndigoLibraryDbContext _context = context;

        public async Task AddAsync(Loan Loan)
        {
            await _context.Loans.AddAsync(Loan);
            await _context.SaveChangesAsync();
        }


        public async Task<Loan?> GetByIdAsync(Guid id)
        {
            return await _context.Loans.FirstOrDefaultAsync(p => p.Id == id);
        }


        public async Task<int> CountActiveLoansByUserAsync(string userId)
        {
            return await _context.Loans.CountAsync(p =>
            p.UserId == userId &&
            p.Status == StatusLoanEnum.Active);
        }
    }


    public class BookRepository(IndigoLibraryDbContext context) : IBookRepository
    {
        private readonly IndigoLibraryDbContext _context = context;

        public async Task<Book?> GetByIsbnAsync(Guid isbn)
        {
            return await _context.Books.FirstOrDefaultAsync(l => l.Isbn == isbn);
        }


        public async Task UpdateAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }
    }
}