using indigoLibrary.Application.DTOs.requests;
using indigoLibrary.Application.DTOs.responses;

namespace indigoLibrary.Application.Interfaces
{
    public interface ILoanService
    {
        Task<CreateLoanResponseDto> CreateLoanAsync(CreateLoanRequestDto request);
        Task<LoanDetailDto> GetLoanAsync(Guid id);
    }
}
