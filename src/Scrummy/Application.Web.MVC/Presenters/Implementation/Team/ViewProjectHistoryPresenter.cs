﻿using System;
using System.Globalization;
using System.Linq;
using Scrummy.Application.Web.MVC.Extensions.Entities;
using Scrummy.Application.Web.MVC.Presenters.Team;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Team;
using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases.Interfaces.Team;

namespace Scrummy.Application.Web.MVC.Presenters.Implementation.Team
{
    internal class ViewProjectHistoryPresenter : Presenter, IViewProjectHistoryPresenter
    {
        public ViewProjectHistoryPresenter(
            Action<MessageType, string> messageHandler, 
            Action<string, string> errorHandler, 
            IRepositoryProvider repositoryProvider) 
            : base(messageHandler, errorHandler, repositoryProvider)
        {
        }

        public ViewProjectHistoryViewModel Present(ViewProjectHistoryResponse response)
        {
            return new ViewProjectHistoryViewModel
            {
                Team = response.Team.ToViewModel(),
                Projects = response.Projects.Select(x =>
                {
                    var project = RepositoryProvider.Project.Read(x.Id);

                    return new ViewProjectHistoryViewModel.Project
                    {
                        Id = project.Id.ToPresentationIdentity(),
                        Text = project.Name,
                        To = x.To.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                        From = x.From.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                    };
                }),
            };
        }
    }
}
