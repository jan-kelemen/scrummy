using Scrummy.Application.Web.Core.ViewModels.Entities.Person;
using Scrummy.Domain.UseCases.Interfaces.Entities.Person;

namespace Scrummy.Application.Web.Core.Mapping.Extensions
{
    public static class ViewModelMappingExtensions
    {
        public static CreatePersonRequest AsRequest(this CreatePersonViewModel vm)
        {
            return new CreatePersonRequest
            {
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                DisplayName = vm.DisplayName,
                Email = vm.Email,
            };
        }
    }
}
