namespace Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities
{
    internal class Person : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DisplayName { get; set; }

        public string Email { get; set; }
    }
}
