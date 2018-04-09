namespace Scrummy.Domain.Core.Entities.Common
{
    /// <summary>
    /// <c>Entity</c> is a base class for all entities in the domain model.
    /// </summary>
    /// <typeparam name="T">Type of the entity.</typeparam>
    public abstract class Entity<T> where T : class
    {
        private readonly Identity _id;

        /// <summary>
        /// Creates the entity.
        /// </summary>
        /// <param name="id">Entity identity.</param>
        protected Entity(Identity id)
        {
            _id = id;
        }

        /// <summary>
        /// Identity of the entity.
        /// </summary>
        public Identity Id => _id;

        /// <summary>
        /// Checks if a entity has blank identity.
        /// </summary>
        /// <returns><c>true</c> if a entity has blank identity.</returns>
        public bool HasBlankIdentity() => _id.IsBlankIdentity();

        /// <summary>
        /// Checks if entities have same identity.
        /// </summary>
        /// <param name="other">other entity.</param>
        /// <returns><c>true</c> if entities have same identity.</returns>
        public bool HasSameIdentityAs(T other) => Equals(other);

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

            return _id == other._id;
        }

        /// <inheritdoc />
        public sealed override int GetHashCode() => _id.GetHashCode();

        /// <inheritdoc />
        public override string ToString() => $"ID={_id}";
    }
}
