using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Scrummy.Application.Web.MVC.Presenters.Sprint;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Sprint;
using Scrummy.Application.Web.MVC.ViewModels.Utility;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Repositories;

namespace Scrummy.Application.Web.MVC.Presenters.Implementation.Sprint
{
    internal class EndSprintPresenter : Presenter, IEndSprintPresenter
    {
        private readonly SelectListItem[] _statuses = {
            new SelectListItem
            {
                Value = "Backlog", Text = "Backlog"
            },
            new SelectListItem
            {
                Value = "Done", Text = "Done"
            }
        };

        public EndSprintPresenter(
            Action<MessageType, string> messageHandler,
            Action<string, string> errorHandler, 
            IRepositoryProvider repositoryProvider) 
            : base(messageHandler, errorHandler, repositoryProvider)
        {
        }

        public EndSprintViewModel GetInitialViewModel(string sprintId)
        {
            var id = Identity.FromString(sprintId);
            var sprint = RepositoryProvider.Sprint.Read(id);
            var backlog = RepositoryProvider.Sprint.ReadSprintBacklog(id);
            var stories = GetStories(backlog.Stories);

            return new EndSprintViewModel
            {
                Sprint = new NavigationViewModel
                {
                    Id = sprint.Id.ToString(),
                    Text = sprint.Name,
                },
                Ids = stories.Item1,
                Names = stories.Item2,
                Decisions = stories.Item3,
                Statuses = _statuses,
            };
        }

        private Tuple<string[], string[], string[]> GetStories(IEnumerable<Identity> stories)
        {
            var ids = new List<string>();
            var names = new List<string>();
            var decisions = new List<string>();

            foreach (var s in stories)
            {
                var story = RepositoryProvider.WorkTask.Read(s);
                ids.Add(s.ToString());
                names.Add(story.Name);
                decisions.Add("Done");
            }

            return new Tuple<string[], string[], string[]>(ids.ToArray(), names.ToArray(), decisions.ToArray());
        }

        public EndSprintViewModel Present(EndSprintViewModel vm)
        {
            vm.Statuses = _statuses;
            return vm;
        }
    }
}
