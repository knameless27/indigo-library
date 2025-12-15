using indigoLibrary.Domain.Enums;


namespace indigoLibrary.Domain.Entities
{
    public class Loan
    {
        public Guid Id { get; private set; }
        public Guid Isbn { get; private set; }
        public string UserId { get; private set; } = string.Empty;
        public TypeUserEnum TypeUser { get; private set; }
        public DateTime DateLoan { get; private set; }
        public DateTime MaxDevolutionDate { get; private set; }
        public StatusLoanEnum Status { get; private set; }

        protected Loan() { }

        public Loan(
            Guid isbn,
            string userId,
            TypeUserEnum typeUser,
            DateTime dateLoan,
            DateTime maxDevolutionDate)
        {
            Id = Guid.NewGuid();
            Isbn = isbn;
            UserId = userId;
            TypeUser = typeUser;
            DateLoan = dateLoan;
            MaxDevolutionDate = maxDevolutionDate;
            Status = StatusLoanEnum.Active;
        }

        public void CheckAsReturned()
        {
            if (Status == StatusLoanEnum.Returned)
                throw new InvalidOperationException("The loan was already returned!");

            Status = StatusLoanEnum.Returned;
        }
    }
}
