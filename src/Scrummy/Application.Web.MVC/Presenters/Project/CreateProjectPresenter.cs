﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Project;
using Scrummy.Domain.Repositories;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Interfaces.Project;

namespace Scrummy.Application.Web.MVC.Presenters.Project
{
    public class CreateProjectPresenter : BasePresenter
    {
        private readonly ITeamRepository _teamRepository;

        public CreateProjectPresenter(Action<MessageType, string> messageHandler, Action<string, string> errorHandler, IRepositoryProvider repositoryProvider) 
            : base(messageHandler, errorHandler)
        {
            _teamRepository = repositoryProvider.Team;
        }

        public CreateProjectViewModel GetInitialViewModel()
        {
            return new CreateProjectViewModel
            {
                DefinitionOfDone = new List<string>(),
                Teams = _teamRepository.ListAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                }).ToArray()
            };
        }

        public string Present(CreateProjectResponse response)
        {
            PresentMessage(MessageType.Success, response.Message);
            return response.Id.ToString();
        }
    }
}
