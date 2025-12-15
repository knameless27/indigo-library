using indigoLibrary.Domain.Enums;


namespace indigoLibrary.Domain.Entities
{
    public class User
    {
        public string Id { get; private set; } = string.Empty;
        public TypeUserEnum TypeUser { get; private set; }

        protected User() { }

        public User(string id, TypeUserEnum typeUser)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("The id is required!");

            if (id.Length > 10)
                throw new ArgumentException("The id can not be longest than 10 characters!");

            Id = id;
            TypeUser = typeUser;
        }
    }
}
