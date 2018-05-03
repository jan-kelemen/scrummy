using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Scrummy.Application.Web.MVC.ViewModels.Utility;
using MeetingValidtion = Scrummy.Domain.Core.Entities.Meeting.Validation;

namespace Scrummy.Application.Web.MVC.ViewModels.Meeting
{
    public class CreateMeetingViewModel
    {
        public NavigationViewModel OrganizedBy { get; set; }

        public NavigationViewModel Project { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(MeetingValidtion.NameMaxLength,
            ErrorMessage = MeetingValidtion.NameIsInvalidMessage,
            MinimumLength = MeetingValidtion.NameMinLength)]
        public string Name { get; set; }

        public string Time { get; set; }

        public string Description { get; set; }

        public List<string> SelectedPersonIds { get; set; } = new List<string>();

        public SelectListItem[] Persons { get; set; } = new SelectListItem[0];
    }
}
