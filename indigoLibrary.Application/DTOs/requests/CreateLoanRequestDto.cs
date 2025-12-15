using indigoLibrary.Domain.Enums;

namespace indigoLibrary.Application.DTOs.requests
{
    public class CreateLoanRequestDto
    {
        public Guid Isbn { get; set; }
        public string UserId { get; set; } = string.Empty;
        public TypeUserEnum TypeUser { get; set; }
    }
}