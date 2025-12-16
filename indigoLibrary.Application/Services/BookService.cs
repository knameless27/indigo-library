using indigoLibrary.Application.DTOs.responses;
using indigoLibrary.Application.DTOs.requests;
using indigoLibrary.Application.Interfaces;
using indigoLibrary.Domain.Entities;

namespace indigoLibrary.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository
                ?? throw new ArgumentNullException(nameof(bookRepository));
        }

        public async Task<CreateBookResponseDto> CreateAsync(CreateBookDto dto)
        {
            if (dto.AvailableAmount < 0)
                throw new InvalidOperationException("The available amount has to be greater than zero!");
            
            var alreadyExist = await _bookRepository.GetByIsbnAsync(dto.Isbn);
            
            if(alreadyExist != null)
                throw new InvalidOperationException("A book with this ISBN already exists!");

            var book = new Book(
                dto.Isbn,
                dto.Title,
                dto.AvailableAmount
            );

            await _bookRepository.AddAsync(book);

            return new CreateBookResponseDto
            {
                Title = book.Title
            };
        }
    }
}
