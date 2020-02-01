using System;
using System.Collections.Generic;
using System.Text;

namespace Products.Service.Domain
{
    /// <summary>
    /// Defines base properties for a common entity.
    /// </summary>
    /// <typeparam name="TIdentifier">The type of the unique identifier property.</typeparam>
    public abstract class Entity<TIdentifier>
    {
        /// <summary>
        /// The entity's unique identifier.
        /// </summary>
        public TIdentifier Id { get; set; }
    }
}
