using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Products.Service.Domain;

namespace Products.Service.Repositories.Mappers
{
    /// <summary>
    /// Allows the configuration of entities mapping from the data access layer to the domain layer.
    /// </summary>
    public static class EntitiesMapper
    {
        private static bool HasBeenInitialized { get; set; }
        private static readonly object Instancelock = new object();

        /// <summary>
        /// Initializes the configuration for the auto-mapping of entities using <see cref="AutoMapper"/>.
        /// </summary>
        public static void Initialize()
        {
            lock (Instancelock)
            {
                if (HasBeenInitialized)
                    return;

                Mapper.Initialize(config =>
                {
                    // Add any custom mapping here between the repository entities model
                    // and the domain business model (and viceversa).

                    config.CreateMap<Product, Entities.Product>()
                        .ForMember(d => d.Id, o => o.MapFrom(s => s.Id));

                    config.CreateMap<Entities.ProductOption, ProductOption>()
                       .ForMember(d => d.Id, o => o.MapFrom(s => s.Id));
                });

                HasBeenInitialized = true;
            }
        }
    }
}
