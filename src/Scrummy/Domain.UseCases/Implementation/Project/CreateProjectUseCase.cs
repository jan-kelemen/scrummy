using Scrummy.Domain.UseCases.Interfaces.Project;

namespace Scrummy.Domain.UseCases.Implementation.Project
{
    internal class CreateProjectUseCase : ICreateProjectUseCase
    {
        public CreateProjectResponse Execute(CreateProjectRequest request)
        {
            return new CreateProjectResponse("");
        }
    }
}
