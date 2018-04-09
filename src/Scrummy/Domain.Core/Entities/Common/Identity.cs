
using System;

namespace Scrummy.Domain.Core.Entities.Common
{
    /// <summary>
    /// <c>Identity</c> is a value type which represents the unique identity of the entity.
    /// </summary>
    ///
    /// <remarks>
    /// Entities should always be compared by their identity, not by values. Blank identities always compare to false.
    /// </remarks>
    public struct Identity : IEquatable<Identity>
    {
        /// <summary>
        /// Identity for temporary and non-persisted objects.
        /// </summary>
        public static readonly Identity BlankIdentity = new Identity(string.Empty);

        /// <summary>
        /// Factory method for creating identity of objects from string.
        /// </summary>
        /// <param name="value">identity value</param>
        /// <returns>Unique entity identity.</returns>
        /// <remarks>Usage of this method isn't recommended.</remarks>
        public static Identity FromString(string value) => new Identity(value);

        private Identity(string id)
        {
            Id = id;
        }

        /// <summary>
        /// Unique identifer.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Checks if the identity is blank.
        /// </summary>
        /// <returns><c>true</c> if the identity is blank identity.</returns>
        public bool IsBlankIdentity() => Id == string.Empty;

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return obj is Identity identity && Equals(identity);
        }

        /// <inheritdoc />
        public bool Equals(Identity other) => !IsBlankIdentity() && !other.IsBlankIdentity() && (Id == other.Id);

        /// <inheritdoc />
        public override int GetHashCode() => Id.GetHashCode();

        /// <inheritdoc />
        public override string ToString() => Id;

        public static bool operator ==(Identity lhs, Identity rhs) => lhs.Equals(rhs);

        public static bool operator !=(Identity lhs, Identity rhs) => !(lhs == rhs);
    }
}