using System.Collections.Generic;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Entities.Enumerations;
using Scrummy.Domain.Core.Validators;

namespace Scrummy.Domain.Core.Entities
{
    public class Document : Entity<Document>
    {
        public class Validation
        {
            public const string NameErrorKey = nameof(Name);
            public const int NameMinLength = 1;
            public const int NameMaxLength = 200;
            public const string NameIsInvalidMessage = "Document name is invalid.";

            public static bool ValidateName(string name) =>
                TextValidator.ValidateThatContentIsBetweenSpecifiedLength(name, NameMinLength, NameMaxLength);
        }

        private string _name;
        private List<string> _links;

        public Document(
            Identity id,
            DocumentKind kind,
            string name,
            Identity projectId,
            IEnumerable<string> links,
            string content) : base(id)
        {
            Name = name;
            Kind = kind;
            Project = projectId;
            Links = links;
            Content = content;
        }

        public string Name
        {
            get => _name;
            set => _name = CheckName(value);
        }

        public Identity Project { get; }

        public DocumentKind Kind { get; }

        public IEnumerable<string> Links
        {
            get => _links;
            set => _links = new List<string>(value);
        }

        public string Content { get; set; }

        private string CheckName(string name)
        {
            if (!Validation.ValidateName(name))
                throw CreateEntityValidationException(Validation.NameErrorKey, Validation.NameIsInvalidMessage);
            return name;
        }
    }
}
