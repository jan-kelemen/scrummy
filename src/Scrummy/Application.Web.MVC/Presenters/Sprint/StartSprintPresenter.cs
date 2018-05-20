using System;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases.Interfaces.Sprint;

namespace Scrummy.Application.Web.MVC.Presenters.Sprint
{
    public class EndSprintPresenter : BasePresenter
    {
        public EndSprintPresenter(
            Action<MessageType, string> messageHandler, 
            Action<string, string> errorHandler, 
            IRepositoryProvider repositoryProvider) 
            : base(messageHandler, errorHandler, repositoryProvider)
        {
        }

        public string Present(EndSprintResponse response)
        {
            PresentMessage(MessageType.Success, response.Message);
            return response.Id.ToString();
        }
    }
}
