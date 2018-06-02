using System;
using System.Linq;
using Scrummy.Application.Web.MVC.Extensions.Entities;
using Scrummy.Application.Web.MVC.Presenters.WorkTask;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Utility;
using Scrummy.Application.Web.MVC.ViewModels.WorkTask;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases.Interfaces.WorkTask;

namespace Scrummy.Application.Web.MVC.Presenters.Implementation.WorkTask
{
    internal class ViewWorkTaskPresenter : Presenter, IViewWorkTaskPresenter
    {
        public ViewWorkTaskPresenter(
            Action<MessageType, string> messageHandler, 
            Action<string, string> errorHandler, 
            IRepositoryProvider repositoryProvider) 
            : base(messageHandler, errorHandler, repositoryProvider)
        {
        }

        public ViewWorkTaskViewModel Present(ViewWorkTaskResponse response)
        {
            var project = RepositoryProvider.Project.Read(response.ProjectId);
            return new ViewWorkTaskViewModel
            {
                Id = response.Id.ToPresentationIdentity(),
                Name = response.Name,
                Project = project.ToViewModel(),
                ParentTask = GetTaskOrNull(response.ParentTask),
                Type = response.Type.ToString(),
                ChildTasks = response.ChildTasks.Select(GetTaskOrNull),
                StoryPoints = response.StoryPoints,
                Description = response.Description,
                Comments = RepositoryProvider.WorkTask.ReadCommentsOfTask(response.Id).Select(x =>
                {
                    var author = RepositoryProvider.Person.Read(x.AuthorId);

                    return new ViewWorkTaskViewModel.Comment
                    {
                        Author = author.ToViewModel(),
                        Id = x.Id.ToPresentationIdentity(),
                        Content = x.Content,
                    };
                }),
                Steps = response.Steps.ToList(),
                CanEdit = response.CanEdit,
                CanEditParent = response.CanEditParent,
                Documents = response.Documents.Select(x => RepositoryProvider.Document.Read(x).ToViewModel()),
            };
        }

        private NavigationViewModel GetTaskOrNull(Identity id)
        {
            if (id.IsBlankIdentity()) return null;

            return RepositoryProvider.WorkTask.Read(id).ToViewModel();
        }
    }
}
