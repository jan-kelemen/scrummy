﻿using Scrummy.Domain.Core.Entities.Common;

namespace Scrummy.Domain.Repositories.Interfaces
{
    /// <summary>
    /// Base interface for all repositories.
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Generates a new identity for the entity.
        /// </summary>
        /// <returns>New identity.</returns>
        Identity GenerateNewIdentity();
    }
}
