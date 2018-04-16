using Scrummy.Domain.Core.Entities.Artifacts;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Validators.Entities;

namespace Scrummy.Domain.Core.Entities
{
    public class Project : Entity<Project>
    {
        private string _name;

        private DefinitionOfDone _definitionOfDone;

        private Team _team;

        public Project(
            Identity id, 
            string name, 
            DefinitionOfDone definitionOfDone, 
            Team team) : base(id)
        {
            definitionOfDone.ProjectIdentity = id;
            team.ProjectIdentity = id;
        }

        public string Name
        {
            get => _name;
            set
            {
                ProjectValidator.CheckName(Id, value);
                _name = value;
            }
        }

        public DefinitionOfDone DefinitionOfDone
        {
            get => _definitionOfDone;
            set
            {
                //TODO: Check for null
                _definitionOfDone = value;
            }
        }

        public Team Team
        {
            get => _team;
            set
            {
                //TODO: Check for null
                _team = value;
            }
        }
    }
}
