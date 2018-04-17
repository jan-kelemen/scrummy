using System;
using System.Collections.Generic;
using Scrummy.Application.Web.Core.ViewModels.Entities.Person;
using Scrummy.Domain.UseCases.Interfaces.Entities.Person;

namespace Scrummy.Application.Web.Core.Presenters.Entities.Person
{
    public class ViewPersonPresenter
    {
        private readonly Action<MessageType, string> _messageHandler;

        private readonly Action<string, string> _errorHandler;

        public ViewPersonPresenter(Action<MessageType, string> messageHandler, Action<string, string> errorHandler)
        {
            _messageHandler = messageHandler;
            _errorHandler = errorHandler;
        }

        public ViewPersonViewModel Present(ViewPersonResponse response)
        {
            return new ViewPersonViewModel
            {
                Id = response.Id,
                FirstName = response.FirstName,
                LastName = response.LastName,
                DisplayName = response.DisplayName,
                Email = response.Email,
            };
        }

        public void Present(IDictionary<string, string> errors)
        {
            foreach (var error in errors)
            {
                _errorHandler.Invoke(error.Key, error.Value);
            }
        }
    }
}
