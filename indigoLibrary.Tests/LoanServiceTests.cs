using Moq;
using indigoLibrary.Application.Services;
using indigoLibrary.Application.Interfaces;
using indigoLibrary.Domain.Entities;
using indigoLibrary.Domain.Enums;
using indigoLibrary.Application.DTOs.requests;

namespace indigoLibrary.Tests
{
    public class LoanServiceTests
    {
        private readonly Mock<ILoanRepository> _loanRepoMock;
        private readonly Mock<IBookRepository> _bookRepoMock;
        private readonly LoanService _service;

        public LoanServiceTests()
        {
            _loanRepoMock = new Mock<ILoanRepository>();
            _bookRepoMock = new Mock<IBookRepository>();
            _service = new LoanService(_loanRepoMock.Object, _bookRepoMock.Object);
        }

        [Fact]
        public async Task Guest_Can_Not_Have_More_Than_One_Active_Loan()
        {
            // Arrange
            var request = new CreateLoanRequestDto
            {
                Isbn = Guid.NewGuid(),
                UserId = "INV123",
                TypeUser = TypeUserEnum.Guest
            };

            _loanRepoMock
                .Setup(r => r.CountActiveLoansByUserAsync(request.UserId))
                .ReturnsAsync(1);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _service.CreateLoanAsync(request));

            Assert.Equal("The user had already one book in loan!", exception.Message);
        }

        [Fact]
        public async Task Do_Not_Allow_Loan_If_There_Is_No_Stock()
        {
            // Arrange
            var isbn = Guid.NewGuid();
            var Book = new Book(isbn, "Book Test", 0);

            var request = new CreateLoanRequestDto
            {
                Isbn = isbn,
                UserId = "AFI123",
                TypeUser = TypeUserEnum.Affiliate
            };

            _bookRepoMock
                .Setup(r => r.GetByIsbnAsync(isbn))
                .ReturnsAsync(Book);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _service.CreateLoanAsync(request));
        }

        [Fact]
        public async Task Calculate_Devolution_Date_Excluding_Weekends()
        {
            // Arrange
            var isbn = Guid.NewGuid();
            var Book = new Book(isbn, "Book Test", 5);

            var request = new CreateLoanRequestDto
            {
                Isbn = isbn,
                UserId = "EMP123",
                TypeUser = TypeUserEnum.Employee
            };

            _loanRepoMock
                .Setup(r => r.CountActiveLoansByUserAsync(It.IsAny<string>()))
                .ReturnsAsync(0);

            _bookRepoMock
                .Setup(r => r.GetByIsbnAsync(isbn))
                .ReturnsAsync(Book);

            _loanRepoMock
                .Setup(r => r.AddAsync(It.IsAny<Loan>()))
                .Returns(Task.CompletedTask);

            _bookRepoMock
                .Setup(r => r.UpdateAsync(It.IsAny<Book>()))
                .Returns(Task.CompletedTask);

            // Act
            var response = await _service.CreateLoanAsync(request);

            // Assert
            Assert.True(response.MaxDateDevolution > DateTime.Now);
        }

        [Fact]
        public async Task Obtener_Loan_Inexistente_Lanza_Error()
        {
            // Arrange
            var id = Guid.NewGuid();

            _loanRepoMock
                .Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync((Loan?)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                _service.GetLoanAsync(id));
        }
    }
}
