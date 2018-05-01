using System.Collections.Generic;
using Scrummy.Application.Web.MVC.ViewModels.Utility;

namespace Scrummy.Application.Web.MVC.ViewModels.Team
{
    public class ListTeamsViewModel
    {
        public IEnumerable<NavigationViewModel> Teams { get; set; }
    }
}
