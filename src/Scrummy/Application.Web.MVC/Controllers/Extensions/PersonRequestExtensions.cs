using Scrummy.Application.Web.MVC.ViewModels.Person;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.UseCases.Interfaces.Person;

namespace Scrummy.Application.Web.MVC.Controllers.Extensions
{
    public static class PersonRequestExtensions
    {
        public static CreatePersonRequest ToRequest(this RegisterPersonViewModel vm)
        {
            return new CreatePersonRequest
            {
                DisplayName = vm.DisplayName,
                Email = vm.Email,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                Password = vm.Password,
            };
        }

        public static EditPersonRequest ToRequest(this EditPersonViewModel vm, string userId)
        {
            return new EditPersonRequest(userId)
            {
                ForUserId = Identity.FromString(vm.Id),
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                DisplayName = vm.DisplayName,
                Email = vm.Email,
            };
        }


        public static ChangePasswordRequest ToRequest(this ChangePasswordViewModel vm, string userId)
        {
            return new ChangePasswordRequest(userId)
            {
                ForUserId = Identity.FromString(vm.Id),
                OldPassword = vm.OldPassword,
                NewPassword = vm.NewPassword,
            };
        }
    }
}