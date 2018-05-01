using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scrummy.Application.Web.MVC.Presenters.Project;
using Scrummy.Application.Web.MVC.ViewModels.Project;
using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases;
using Scrummy.Domain.UseCases.Interfaces.Factories;

namespace Scrummy.Application.Web.MVC.Controllers
{
    [Authorize]
    public class ProjectController : BaseController
    {
        private readonly IProjectUseCaseFactory _projectUseCaseFactory;
        private readonly IRepositoryProvider _repositoryProvider;

        public ProjectController(IUseCaseFactoryProvider useCaseFactoryProvider, IRepositoryProvider repositoryProvider)
        {
            _projectUseCaseFactory = useCaseFactoryProvider.Project;
            _repositoryProvider = repositoryProvider;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var presenter = new CreateProjectPresenter(MessageHandler, ErrorHandler, _repositoryProvider);
            return View(presenter.GetInitialViewModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(CreateProjectViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var presenter = new CreateProjectPresenter(MessageHandler, ErrorHandler, _repositoryProvider);
            var uc = _projectUseCaseFactory.Create;


            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public IActionResult List()
        {
            var presenter = new ListProjectsPresenter(MessageHandler, ErrorHandler, _repositoryProvider);

            return View(presenter.Present());
        }
    }
}
