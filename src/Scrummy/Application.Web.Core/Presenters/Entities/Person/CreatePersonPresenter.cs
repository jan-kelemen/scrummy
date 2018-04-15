using System;
using System.Collections.Generic;
using Scrummy.Application.Web.Core.ViewModels;
using Scrummy.Domain.UseCases.Interfaces.Entities.Person;

namespace Scrummy.Application.Web.Core.Presenters.Entities.Person
{
    public class CreatePersonPresenter
    {
        private readonly Action<Message> _messageHandler;

        private readonly Action<string, string> _errorHandler;

        public CreatePersonPresenter(Action<Message> messageHandler, Action<string, string> errorHandler)
        {
            _messageHandler = messageHandler;
            _errorHandler = errorHandler;
        }

        public void Present(CreatePersonResponse response)
        {
            _messageHandler.Invoke(new Message
            {
                Type = MessageType.Success,
                Text = response.Message,
            });
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
