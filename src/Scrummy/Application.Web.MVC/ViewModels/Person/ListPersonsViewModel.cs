using System.Collections.Generic;
using Scrummy.Application.Web.MVC.ViewModels.Utility;

namespace Scrummy.Application.Web.MVC.ViewModels.Person
{
    public class ListPersonsViewModel
    {
        public IEnumerable<NavigationViewModel> Persons { get; set; }
    }
}
