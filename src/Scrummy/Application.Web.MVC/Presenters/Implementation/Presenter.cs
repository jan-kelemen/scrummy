﻿using System;
using System.Collections.Generic;
using Scrummy.Application.Web.MVC.Extensions.Entities;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases.Boundary.Responses;

namespace Scrummy.Application.Web.MVC.Presenters.Implementation
{
    internal class Presenter : IPresenter
    {
        protected readonly Action<MessageType, string> MessageHandler;

        protected readonly Action<string, string> ErrorHandler;

        protected readonly IRepositoryProvider RepositoryProvider;

        public Presenter(Action<MessageType, string> messageHandler, Action<string, string> errorHandler, IRepositoryProvider repositoryProvider)
        {
            MessageHandler = messageHandler;
            ErrorHandler = errorHandler;
            RepositoryProvider = repositoryProvider;
        }

        public virtual string Present(ConfirmationResponse response)
        {
            PresentMessage(MessageType.Success, response.Message);
            return response.Id.ToPresentationIdentity();
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
