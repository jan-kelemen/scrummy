using System;
using System.Collections.Generic;
using Scrummy.Application.Web.MVC.Utility;

namespace Scrummy.Application.Web.MVC.Presenters
{
    public abstract class BasePresenter
    {
        protected readonly Action<MessageType, string> MessageHandler;

        protected readonly Action<string, string> ErrorHandler;

        protected BasePresenter(Action<MessageType, string> messageHandler, Action<string, string> errorHandler)
        {
            MessageHandler = messageHandler;
            ErrorHandler = errorHandler;
        }

        public virtual void PresentErrors(string message, IDictionary<string, string> errors)
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                MessageHandler.Invoke(MessageType.Error, message);
            }
            foreach (var error in errors)
            {
                ErrorHandler.Invoke(error.Key, error.Value);
            }
        }

        public virtual void PresentMessage(MessageType type, string message)
        {
            MessageHandler.Invoke(type, message);
        }
    }
}
