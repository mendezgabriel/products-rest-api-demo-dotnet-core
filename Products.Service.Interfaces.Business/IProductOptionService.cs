using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Products.Service.Domain;

namespace Products.Service.Interfaces.Business
{
    /// <summary>
    /// Defines the business contracts for managing a <see cref="Product"/>.
    /// </summary>
    public interface IProductOptionService
    {
        /// <summary>
        /// Gets all products that match the specified name if provided. Otherwise,
        /// returns a list of all available products.
        /// </summary>
        /// <param name="name">The name of the product to be used as a search criteria.</param>
        /// <returns>A <see cref="ListResult{ProductOption}"/>.</returns>
        Task<Result<ListResult<ProductOption>>> GetByProductAsync(Guid productId);

        /// <summary>
        /// Gets a product that matches the specified <paramref name="id"/>.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="id">The product option identifier.</param>
        /// <returns>A <see cref="Product"/> if a match is found. Null otherwise.</returns>
        Task<Result<ProductOption>> GetByIdAsync(Guid productId, Guid id);

        /// <summary>
        /// Creates a new <see cref="ProductOption"/>.
        /// </summary>
        /// <param name="product">The product option to be created.</param>
        /// <returns>The created <see cref="ProductOption"/> object.</returns>
        Task<Result<ProductOption>> CreateAsync(ProductOption productOption);

        /// <summary>
        /// Updates a specified <see cref="ProductOption"/>.
        /// </summary>
        /// <param name="product">The product option to be updated.</param>
        /// <returns>True if the product option was updated. False otherwise.</returns>
        Task<Result<bool>> UpdateAsync(ProductOption productOption);

        /// <summary>
        /// Deletes a specified <see cref="ProductOption"/>.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="id">The identifier of the product option to be deleted.</param>
        /// <returns></returns>
        Task<Result<bool>> DeleteAsync(Guid productId, Guid id);
    }
}
