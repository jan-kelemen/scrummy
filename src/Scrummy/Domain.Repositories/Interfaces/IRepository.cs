﻿using System.Collections.Generic;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Repositories.Interfaces.DTO;

namespace Scrummy.Domain.Repositories.Interfaces
{
    /// <summary>
    /// Base interface for all repositories.
    /// </summary>
    public interface IRepository<T>
    {
        /// <summary>
        /// Generates a new identity for the entity.
        /// </summary>
        /// <returns>New identity.</returns>
        Identity GenerateNewIdentity();

        Identity Create(T entity);

        T Read(Identity id);

        void Update(T entity);

        void Delete(Identity id);

        bool Exists(Identity id);

        IEnumerable<NavigationInfo> ListAll();
    }
}
