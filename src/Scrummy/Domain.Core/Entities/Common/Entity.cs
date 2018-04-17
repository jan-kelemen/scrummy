using Scrummy.Domain.Core.Exceptions;
using Scrummy.Domain.Core.Utilities;
using Scrummy.Domain.Core.Validators;

namespace Scrummy.Domain.Core.Entities.Common
{
    /// <summary>
    /// <c>Entity</c> is a base class for all entities in the domain model.
    /// </summary>
    /// <typeparam name="T">Type of the entity.</typeparam>
    public abstract class Entity<T> where T : class
    {
        /// <summary>
        /// Creates the entity.
        /// </summary>
        /// <param name="id">Entity identity.</param>
        protected Entity(Identity id)
        {
            Id = id;
        }

        /// <summary>
        /// Identity of the entity.
        /// </summary>
        public Identity Id { get; }

        /// <summary>
        /// Checks if a entity has blank identity.
        /// </summary>
        /// <returns><c>true</c> if a entity has blank identity.</returns>
        public bool HasBlankIdentity() => Id.IsBlankIdentity();

        /// <summary>
        /// Checks if entities have same identity.
        /// </summary>
        /// <param name="other">other entity.</param>
        /// <returns><c>true</c> if entities have same identity.</returns>
        public bool HasSameIdentityAs(T other) => Equals(other);

        protected EntityValidationException CreateEntityValidationException(string errorKey, string message, params object[] values)
            => ExceptionUtility.CreateEntityValidationException<T>(Id, errorKey, message, values);

        protected EntityReferenceNullException CreateEntityReferenceNullException(string errorKey)
            => ExceptionUtility.CreateEntityReferenceNullException<T>(Id, errorKey);

        protected EntityIdentityIsInvalidException CreateEntityIdentityIsInvalidException(string errorKey)
            => ExceptionUtility.CreatEntityIdentityIsInvalidException<T>(Id, errorKey);

        protected TR CheckReferenceNotNull<TR>(TR reference, string errorKey)
        {
            if (!ReferenceValidator.ValidateReferenceIsNotNull(reference))
                throw CreateEntityReferenceNullException(errorKey);
            return reference;
        }

        protected Identity CheckIdentityIsNotBlank(Identity id, string errorKey)
        {
            if (Id.IsBlankIdentity())
                throw CreateEntityIdentityIsInvalidException(errorKey);
            return id;
        }

        /// <inheritdoc />
        public sealed override bool Equals(object obj)
        {
            if (!(obj is Entity<T> other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (GetType() != other.GetType())
            {
                return false;
            }

            return Id == other.Id;
        }

        /// <inheritdoc />
        public sealed override int GetHashCode() => Id.GetHashCode();

        /// <inheritdoc />
        public override string ToString() => $"ID={Id}";
    }
}
