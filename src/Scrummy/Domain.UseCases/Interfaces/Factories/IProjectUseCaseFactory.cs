using Scrummy.Domain.UseCases.Interfaces.Project;

namespace Scrummy.Domain.UseCases.Interfaces.Factories
{
    public interface IProjectUseCaseFactory
    {
        ICreateProjectUseCase Create { get; }

        IViewProjectUseCase View { get; }
    }
}
