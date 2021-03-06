﻿using System;
using System.Collections.Generic;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;

namespace Scrummy.Domain.UseCases.Interfaces.Person
{
    public class ViewPersonRequest : AuthorizedIdRequest
    {
        public ViewPersonRequest(string userId) : base(userId)
        {
        }

        public DateTime CurrentTime { get; set; } = DateTime.Now;

        protected override void ValidateCore()
        {
            if (Id.IsBlankIdentity())
            {
                AddError("", "Idenitity is invalid.");
            }
        }
    }

    public class ViewPersonResponse : BaseResponse
    {
        public ViewPersonResponse() : base(null)
        {
        }

        public bool IsSameAsPersonWhoRequested { get; set; }

        public Identity Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DisplayName { get; set; }

        public string Email { get; set; }

        public IEnumerable<Identity> CurrentTeams { get; set; }
    }

    public interface IViewPersonUseCase
    {
        ViewPersonResponse Execute(ViewPersonRequest request);
    }
}
