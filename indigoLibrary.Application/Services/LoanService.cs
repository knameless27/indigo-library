using indigoLibrary.Application.DTOs.requests;
using indigoLibrary.Application.DTOs.responses;
using indigoLibrary.Application.Interfaces;
using indigoLibrary.Domain.Entities;
using indigoLibrary.Domain.Enums;

namespace indigoLibrary.Application.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IBookRepository _bookRepository;

        public LoanService(
            ILoanRepository loanRepository,
            IBookRepository bookRepository)
        {
            _loanRepository = loanRepository
                ?? throw new ArgumentNullException(nameof(loanRepository));

            _bookRepository = bookRepository
                ?? throw new ArgumentNullException(nameof(bookRepository));
        }

        public async Task<CreateLoanResponseDto> CreateLoanAsync(CreateLoanRequestDto request)
        {
            if (request.TypeUser == TypeUserEnum.Guest)
            {
                var activeLoans =
                    await _loanRepository.CountActiveLoansByUserAsync(request.UserId);

                if (activeLoans >= 1)
                    throw new InvalidOperationException(
                        "The user had already one book in loan!");
            }

            var book = await _bookRepository.GetByIsbnAsync(request.Isbn)
                ?? throw new InvalidOperationException("The book doesn't exist!");

            book.DecreaseStock();

            var dateLoan = DateTime.Now;
            var MaxDevolutionDate = CalculateDateDevolution(
                dateLoan,
                request.TypeUser);

            var loan = new Loan(
                request.Isbn,
                request.UserId,
                request.TypeUser,
                dateLoan,
                MaxDevolutionDate);

            await _loanRepository.AddAsync(loan);
            await _bookRepository.UpdateAsync(book);

            return new CreateLoanResponseDto
            {
                Id = loan.Id,
                MaxDevolutionDate = MaxDevolutionDate
            };
        }

        public async Task<LoanDetailDto> GetLoanAsync(Guid id)
        {
            var loan = await _loanRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("The loan doesn't exist!");

            return new LoanDetailDto
            {
                Id = loan.Id,
                Isbn = loan.Isbn,
                UserId = loan.UserId,
                TypeUser = loan.TypeUser,
                MaxDevolutionDate = loan.MaxDevolutionDate
            };
        }

        private static DateTime CalculateDateDevolution(
            DateTime initDate,
            TypeUserEnum typeUser)
        {
            int workDays = typeUser switch
            {
                TypeUserEnum.Affiliate => 10,
                TypeUserEnum.Employee => 8,
                TypeUserEnum.Guest => 7,
                _ => throw new ArgumentOutOfRangeException()
            };

            var date = initDate;
            int countedDays = 0;

            while (countedDays < workDays)
            {
                if (date.DayOfWeek != DayOfWeek.Saturday &&
                    date.DayOfWeek != DayOfWeek.Sunday)
                {
                    countedDays++;
                }

                if (countedDays < workDays)
                    date = date.AddDays(1);
            }

            return date;
        }
    }
}
