using indigoLibrary.Domain.Enums;

namespace indigoLibrary.Application.DTOs.responses
{
    public class LoanDetailDto
    {
        public Guid Id { get; set; }
        public Guid Isbn { get; set; }
        public string UserId { get; set; } = string.Empty;
        public TypeUserEnum TypeUser { get; set; }
        public DateTime MaxDevolutionDate { get; set; }
    }
}