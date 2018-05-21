using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Scrummy.Application.Web.MVC.ViewModels.Utility;

namespace Scrummy.Application.Web.MVC.ViewModels.Sprint
{
    public class ViewSprintViewModel
    {
        public class StoryViewModel : NavigationViewModel
        {
            private readonly string[] _status = {"ToDo", "InProgress", "Done"};

            public string StoryPoints { get; set; }

            public Dictionary<string, IList<NavigationViewModel>> Tasks { get; set; }

            public IEnumerable<Tuple<NavigationViewModel, NavigationViewModel, NavigationViewModel>> Transform()
            {
                var list = new List<Tuple<NavigationViewModel, NavigationViewModel, NavigationViewModel>>();
                var max = Tasks.Select(x => x.Value.Count).Any() ? Tasks.Select(x => x.Value.Count).Max() : 0;
                for (var i = 0; i < max; i++)
                    list.Add(new Tuple<NavigationViewModel, NavigationViewModel, NavigationViewModel>(
                        ViewModelOrNull(_status[0], i), ViewModelOrNull(_status[1], i), ViewModelOrNull(_status[2], i)));

                return list;
            }

            private NavigationViewModel ViewModelOrNull(string status, int index) =>
                Tasks.ContainsKey(status) && index < Tasks[status].Count ? Tasks[status][index] : null;
        }

        public string Id { get; set; }

        public NavigationViewModel Project { get; set; }

        public string Name { get; set; }

        [Display(Name = "Start date")]
        public string StartDate { get; set; }

        [Display(Name = "End date")]
        public string EndDate { get; set; }

        public string Goal { get; set; }

        public IEnumerable<StoryViewModel> Stories { get; set; } = new StoryViewModel[0];

        public bool CanDelete { get; set; }
    }
}
