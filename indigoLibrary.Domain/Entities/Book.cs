namespace indigoLibrary.Domain.Entities
{
    public class Book
    {
        public Guid Isbn { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public int AvailableAmount { get; private set; }

        protected Book() { }

        public Book(Guid isbn, string title, int availableAmount)
        {
            if (availableAmount < 0)
                throw new ArgumentException("The available amount can not be negative!");

            Isbn = isbn;
            Title = title;
            AvailableAmount = availableAmount;
        }

        public void DecreaseStock()
        {
            if (AvailableAmount <= 0)
                throw new InvalidOperationException("There is not books available!");

            AvailableAmount--;
        }

        public void IncreaseStock()
        {
            AvailableAmount++;
        }
    }
}