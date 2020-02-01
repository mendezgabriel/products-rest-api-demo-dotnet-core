using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Products.Service.Domain;

namespace Products.Service.Interfaces.Repository
{
    public interface IProductOptionRepository
    {
        /// <summary>
        /// Gets all product options that match the specified product identifier.
        /// </summary>
        /// <param name="productId">The identifier of the product to be used as a search criteria.</param>
        /// <returns>A <see cref="List{ProductOption}"/>.</returns>
        Task<List<ProductOption>> GetByProductAsync(Guid productId);

        /// <summary>
        /// Gets a product option that matches the specified <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The product option identifier.</param>
        /// <returns>A <see cref="ProductOption"/> if a match is found. Null otherwise.</returns>
        Task<ProductOption> GetByIdAsync(Guid id);

        /// <summary>
        /// Creates a new <see cref="ProductOption"/>.
        /// </summary>
        /// <param name="ProductOption">The product option to be created.</param>
        /// <returns>The created <see cref="ProductOption"/> object.</returns>
        Task<ProductOption> CreateAsync(ProductOption ProductOption);

        /// <summary>
        /// Updates a specified <see cref="ProductOption"/>.
        /// </summary>
        /// <param name="ProductOption">The product option to be updated.</param>
        /// <returns>True if the product option was updated. False otherwise.</returns>
        Task<bool> UpdateAsync(ProductOption ProductOption);

        /// <summary>
        /// Deletes a specified <see cref="ProductOption"/>.
        /// </summary>
        /// <param name="id">The identifier of the product option to be deleted.</param>
        /// <returns>True if the product option was deleted. False otherwise.</returns>
        Task<bool> DeleteAsync(Guid id);
    }
}
