using System;
using Scrummy.Application.Web.MVC.Presenters.WorkTask.Comment;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Utility;
using Scrummy.Application.Web.MVC.ViewModels.WorkTask.Comment;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Repositories;

namespace Scrummy.Application.Web.MVC.Presenters.Implementation.WorkTask.Comment
{
    internal class EditCommentPresenter : Presenter, IEditCommentPresenter
    {
        public EditCommentPresenter(
            Action<MessageType, string> messageHandler, 
            Action<string, string> errorHandler, 
            IRepositoryProvider repositoryProvider) 
            : base(messageHandler, errorHandler, repositoryProvider)
        {
        }

        public EditCommentViewModel GetInitialViewModel(string workTaskId, string commentId)
        {
            var task = RepositoryProvider.WorkTask.Read(Identity.FromString(workTaskId));
            var comment = RepositoryProvider.WorkTask.ReadComment(task.Id, Identity.FromString(commentId));

            return new EditCommentViewModel
            {
                WorkTask = new NavigationViewModel
                {
                    Id = task.Id.ToString(),
                    Text = task.Name,
                },
                CommentId = comment.Id.ToString(),
                Content = comment.Content,
            };
        }
    }
}
