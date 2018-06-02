using Microsoft.AspNetCore.Mvc.Rendering;
using Scrummy.Application.Web.MVC.ViewModels.Utility;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Repositories.Interfaces;

namespace Scrummy.Application.Web.MVC.Extensions.Entities
{
    public static class EntityMappingExtensions
    {
        public static string ToPresentationIdentity(this Identity id) => id.ToString();

        public static NavigationViewModel ToViewModel(this NavigationInfo ni) => new NavigationViewModel
        {
            Id = ni.Id.ToPresentationIdentity(),
            Text = ni.Name,
        };

        public static NavigationViewModel ToViewModel(this Document entity) => new NavigationViewModel
        {
            Id = entity.Id.ToPresentationIdentity(),
            Text = entity.Name,
        };

        public static NavigationViewModel ToViewModel(this Meeting entity) => new NavigationViewModel
        {
            Id = entity.Id.ToPresentationIdentity(),
            Text = entity.Name,
        };

        public static NavigationViewModel ToViewModel(this Person entity) => new NavigationViewModel
        {
            Id = entity.Id.ToPresentationIdentity(),
            Text = entity.DisplayName,
        };

        public static NavigationViewModel ToViewModel(this Project entity) => new NavigationViewModel
        {
            Id = entity.Id.ToPresentationIdentity(),
            Text = entity.Name,
        };

        public static NavigationViewModel ToViewModel(this Sprint entity) => new NavigationViewModel
        {
            Id = entity.Id.ToPresentationIdentity(),
            Text = entity.Name,
        };

        public static NavigationViewModel ToViewModel(this Team entity) => new NavigationViewModel
        {
            Id = entity.Id.ToPresentationIdentity(),
            Text = entity.Name,
        };

        public static NavigationViewModel ToViewModel(this WorkTask entity) => new NavigationViewModel
        {
            Id = entity.Id.ToPresentationIdentity(),
            Text = entity.Name,
        };

        public static SelectListItem ToSelectListItem(this NavigationInfo ni) => new SelectListItem
        {
            Value = ni.Id.ToPresentationIdentity(),
            Text = ni.Name,
        };

        public static SelectListItem ToSelectListItem(this Document entity) => new SelectListItem
        {
            Value = entity.Id.ToPresentationIdentity(),
            Text = entity.Name,
        };

        public static SelectListItem ToSelectListItem(this Meeting entity) => new SelectListItem
        {
            Value = entity.Id.ToPresentationIdentity(),
            Text = entity.Name,
        };

        public static SelectListItem ToSelectListItem(this Person entity) => new SelectListItem
        {
            Value = entity.Id.ToPresentationIdentity(),
            Text = entity.DisplayName,
        };

        public static SelectListItem ToSelectListItem(this Project entity) => new SelectListItem
        {
            Value = entity.Id.ToPresentationIdentity(),
            Text = entity.Name,
        };

        public static SelectListItem ToSelectListItem(this Sprint entity) => new SelectListItem
        {
            Value = entity.Id.ToPresentationIdentity(),
            Text = entity.Name,
        };

        public static SelectListItem ToSelectListItem(this Team entity) => new SelectListItem
        {
            Value = entity.Id.ToPresentationIdentity(),
            Text = entity.Name,
        };

        public static SelectListItem ToSelectListItem(this WorkTask entity) => new SelectListItem
        {
            Value = entity.Id.ToPresentationIdentity(),
            Text = entity.Name,
        };
    }
}
