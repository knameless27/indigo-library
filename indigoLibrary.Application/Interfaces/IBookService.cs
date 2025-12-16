using indigoLibrary.Application.DTOs.requests;
using indigoLibrary.Application.DTOs.responses;

namespace indigoLibrary.Application.Interfaces
{
    public interface IBookService
    {
        Task<CreateBookResponseDto> CreateAsync(CreateBookDto dto);
    }
}
