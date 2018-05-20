using System;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases.Interfaces.Sprint;

namespace Scrummy.Application.Web.MVC.Presenters.Sprint
{
    public class ChangeTaskStatusPresenter : BasePresenter
    {
        public ChangeTaskStatusPresenter(
            Action<MessageType, string> messageHandler, 
            Action<string, string> errorHandler, 
            IRepositoryProvider repositoryProvider) 
            : base(messageHandler, errorHandler, repositoryProvider)
        {
        }

        public string Present(ChangeTaskStatusResponse response)
        {
            PresentMessage(MessageType.Success, response.Message);
            return response.ProjectId.ToString();
        }
    }
}
