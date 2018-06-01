using Scrummy.Application.Web.MVC.ViewModels.WorkTask.Comment;

namespace Scrummy.Application.Web.MVC.Presenters.WorkTask.Comment
{
    public interface IEditCommentPresenter
    {
        EditCommentViewModel GetInitialViewModel(string workTaskId, string commentId);
    }
}