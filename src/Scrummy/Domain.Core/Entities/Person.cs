using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Validators.Entities;

namespace Scrummy.Domain.Core.Entities
{
    public class Person : Entity<Person>
    {
        private string _firstName;
        private string _lastName;
        private string _displayName;
        private string _email;

        public Person(
            Identity id,
            string firstName,
            string lastName,
            string displayName,
            string email) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            DisplayName = displayName;
            Email = email;
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                PersonValidator.CheckFirstName(this, value);
                _firstName = value;
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                PersonValidator.CheckLastName(this, value);
                _lastName = value;
            }
        }

        public string DisplayName
        {
            get => _displayName;
            set
            {
                PersonValidator.CheckDisplayName(this, value);
                _displayName = value;
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                PersonValidator.CheckEmail(this, value);
                _email = value;
            }
        }
    }
}
