using System;
using System.Linq;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Person;
using Scrummy.Application.Web.MVC.ViewModels.Utility;
using Scrummy.Domain.Repositories;
using Scrummy.Domain.Repositories.Interfaces;

namespace Scrummy.Application.Web.MVC.Presenters.Person
{
    public class ListPersonsPresenter : BasePresenter
    {
        private readonly IPersonRepository _personRepository;

        public ListPersonsPresenter(Action<MessageType, string> messageHandler, Action<string, string> errorHandler, IRepositoryProvider repositoryProvider) 
            : base(messageHandler, errorHandler)
        {
            _personRepository = repositoryProvider.Person;
        }

        public ListPersonsViewModel Present()
        {
            return new ListPersonsViewModel
            {
                Persons = _personRepository.ListAll().Select(x => new NavigationViewModel
                {
                    Id = x.Id.ToString(),
                    Text = x.Name
                })
            };
        }
    }
}
