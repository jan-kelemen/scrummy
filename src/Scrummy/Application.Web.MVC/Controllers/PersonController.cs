using System;
using Microsoft.AspNetCore.Mvc;
using Scrummy.Application.Web.Core.Mapping.Extensions;
using Scrummy.Application.Web.Core.Presenters.Entities.Person;
using Scrummy.Application.Web.Core.ViewModels;
using Scrummy.Application.Web.Core.ViewModels.Entities.Person;
using Scrummy.Domain.UseCases;
using Scrummy.Domain.UseCases.Exceptions.Boundary;
using Scrummy.Domain.UseCases.Interfaces.Entities.Person;

namespace Scrummy.Application.Web.MVC.Controllers
{
    public class PersonController : Controller
    {
        private readonly IUseCaseFactoryProvider _useCaseFactoryProvider;

        public PersonController(IUseCaseFactoryProvider useCaseFactoryProvider)
        {
            _useCaseFactoryProvider = useCaseFactoryProvider;
        }

        [HttpGet]
        public IActionResult Index(string id)
        {
            var request = new ViewPersonRequest {Id = id,};
            var presenter = new ViewPersonPresenter(MessageHandler, ErrorHandler);
            var uc = _useCaseFactoryProvider.Person.View;
            try
            {
                var response = uc.Execute(request);
                var vm = presenter.Present(response);

                return View(vm);
            }
            catch (InvalidRequestException ire)
            {
                presenter.Present(request.Errors);
                return NotFound();
            }
            catch (Exception)
            {
                MessageHandler(new Message
                {
                    Type = MessageType.Error,
                    Text = "Unknown error.",
                });
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreatePersonViewModel());
        }

        [HttpPost]
        public IActionResult Create(CreatePersonViewModel vm)
        {
            var request = vm.AsRequest();
            var presenter = new CreatePersonPresenter(MessageHandler, ErrorHandler);
            var uc = _useCaseFactoryProvider.Person.Create;
            try
            {
                var response = uc.Execute(request);
                presenter.Present(response);
                return RedirectToAction(nameof(Index), new {response.Id});
            }
            catch (InvalidRequestException ire)
            {
                presenter.Present(request.Errors);
                return View(vm);
            }
            catch (Exception)
            {
                MessageHandler(new Message
                {
                    Type = MessageType.Error,
                    Text = "Unknown error.",
                });
                return View(vm);
            }
        }

        private void ErrorHandler(string s, string s1)
        {
            ModelState.AddModelError(s, s1);
        }

        private void MessageHandler(Message message)
        {
            TempData["Status"] = message.Type;
            TempData["Message"] = message.Text;
        }
    }
}