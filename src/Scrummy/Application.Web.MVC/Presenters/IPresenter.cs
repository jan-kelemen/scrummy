using System.Collections.Generic;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Domain.UseCases.Boundary.Responses;

namespace Scrummy.Application.Web.MVC.Presenters
{
    public interface IPresenter
    {
        string Present(ConfirmationResponse response);

        void PresentErrors(string message, IDictionary<string, string> errors);

        void PresentMessage(MessageType type, string message);
    }
}