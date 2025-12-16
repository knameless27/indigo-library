using indigoLibrary.Domain.Entities;

namespace indigoLibrary.Application.Interfaces
{
    public interface IBookRepository
    {
        Task AddAsync(Book book);
        Task<Book?> GetByIsbnAsync(Guid isbn);
        Task UpdateAsync(Book book);
    }
}