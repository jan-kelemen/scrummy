using Scrummy.Domain.Repositories.Interfaces.Entities;

namespace Scrummy.Domain.Repositories
{
    public interface IRepositoryFactoryProvider
    {
        IPersonRepository Person { get; }

        IProjectRepository Project { get; }
    }
}
