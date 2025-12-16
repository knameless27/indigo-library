namespace indigoLibrary.Application.DTOs.requests
{
    public class CreateBookDto
    {
        public Guid Isbn { get; set; }
        public string Title { get; set; } = string.Empty;
        public int AvailableAmount { get; set; }
    }
}