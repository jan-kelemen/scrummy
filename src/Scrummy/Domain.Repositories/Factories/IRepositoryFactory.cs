using Scrummy.Domain.Repositories.Interfaces.Entities;

namespace Scrummy.Domain.Repositories.Factories
{
    public interface IRepositoryFactory
    {
        IPersonRepository PersonRepository { get; }

        IProjectRepository ProjectRepository { get; }
    }
}
