using indigoLibrary.Application.DTOs.requests;
using indigoLibrary.Application.DTOs.responses;
using indigoLibrary.Application.Interfaces;
using indigoLibrary.Domain.Entities;
using indigoLibrary.Domain.Enums;

namespace indigoLibrary.Application.Services
{
    public class LoanService
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IBookRepository _bookRepository;
        public async Task<CreateLoanResponseDto> CreateLoanAsync(CreateLoanRequestDto request)
        {
            if (request.TypeUser == TypeUserEnum.Guest)
            {
                var activeLoans = await _loanRepository.CountActiveLoansByUserAsync(request.UserId);


                if (activeLoans >= 1)
                    throw new InvalidOperationException("The user had already one book in loan!");
            }


            var book = await _bookRepository.GetByIsbnAsync(request.Isbn)
            ?? throw new InvalidOperationException("The book doesn't exist!");


            book.DecreaseStock();


            var dateLoan = DateTime.Now;
            var maxDateDevolution = CalculateDateDevolution(
            dateLoan,
            request.TypeUser);


            var Loan = new Loan(
            request.Isbn,
            request.UserId,
            request.TypeUser,
            dateLoan,
            maxDateDevolution);


            await _loanRepository.AddAsync(Loan);
            await _bookRepository.UpdateAsync(book);


            return new CreateLoanResponseDto
            {
                Id = Loan.Id,
                MaxDateDevolution = maxDateDevolution
            };
        }


        public async Task<LoanDetailDto> GetLoanAsync(Guid id)
        {
            var Loan = await _loanRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("The loan doesn't exist!");


            return new LoanDetailDto
            {
                Id = Loan.Id,
                Isbn = Loan.Isbn,
                UserId = Loan.UserId,
                TypeUser = Loan.TypeUser,
                MaxDevolutionDate = Loan.MaxDevolutionDate
            };
        }


        private static DateTime CalculateDateDevolution(DateTime initDate, TypeUserEnum TypeUser)
        {
            int workDays = TypeUser switch
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