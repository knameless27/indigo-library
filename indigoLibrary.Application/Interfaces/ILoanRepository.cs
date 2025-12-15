using indigoLibrary.Domain.Entities;

namespace indigoLibrary.Application.Interfaces
{
    public interface ILoanRepository
    {
        Task AddAsync(Loan Loan);
        Task<Loan?> GetByIdAsync(Guid id);
        Task<int> CountActiveLoansByUserAsync(string userId);
    }
}