using System;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Domain.UseCases.Interfaces.Person;

namespace Scrummy.Application.Web.MVC.Presenters.Person
{
    public class RegisterPersonPresenter : BasePresenter
    {
        public RegisterPersonPresenter(Action<MessageType, string> messageHandler, Action<string, string> errorHandler)
            : base(messageHandler, errorHandler)
        {
        }

        public string Present(CreatePersonResponse response)
        {
            PresentMessage(MessageType.Success, response.Message);
            return response.Id.ToString();
        } 
    }
}
