using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Scrummy.Application.Web.MVC.ViewModels.Utility;

namespace Scrummy.Application.Web.MVC.ViewModels.Person
{
    public class ViewPersonViewModel
    {
        public string Id { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "Display name")]
        public string DisplayName { get; set; }

        public string Email { get; set; }

        public bool IsSameAsPersonWhoRequested { get; set; }

        public IEnumerable<NavigationViewModel> CurrentTeams { get; set; }
    }
}
