using System;
using System.Collections.Generic;
using System.Text;

namespace Products.Service.Domain
{
    /// <summary>
    /// Defines base properties for a common named entity.
    /// </summary>
    /// <typeparam name="TIdentifier">The type of the unique identifier property.</typeparam>
    public abstract class NamedEntity<TIdentifier> : Entity<TIdentifier>
    {
        /// <summary>
        /// The entity's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The entity's description.
        /// </summary>
        public string Description { get; set; }
    }
}
