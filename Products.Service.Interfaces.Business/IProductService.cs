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
    public interface IProductService
    {
        /// <summary>
        /// Gets all products that match the specified name if provided. Otherwise,
        /// returns a list of all available products.
        /// </summary>
        /// <param name="name">The name of the product to be used as a search criteria.</param>
        /// <returns>A <see cref="ListResult{Product}"/>.</returns>
        Task<Result<ListResult<Product>>> GetAsync(string name);

        /// <summary>
        /// Gets a product that matches the specified <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <returns>A <see cref="Product"/> if a match is found. Null otherwise.</returns>
        Task<Result<Product>> GetByIdAsync(Guid id);

        /// <summary>
        /// Creates a new <see cref="Product"/>.
        /// </summary>
        /// <param name="product">The product to be created.</param>
        /// <returns>The created <see cref="Product"/> object.</returns>
        Task<Result<Product>> CreateAsync(Product product);

        /// <summary>
        /// Updates a specified <see cref="Product"/>.
        /// </summary>
        /// <param name="product">The product to be updated.</param>
        /// <returns>True if the product was updated. False otherwise.</returns>
        Task<Result<bool>> UpdateAsync(Product product);

        /// <summary>
        /// Deletes a specified <see cref="Product"/>.
        /// </summary>
        /// <param name="id">The identifier of the product to be deleted.</param>
        /// <returns>True if the product was deleted. False otherwise.</returns>
        Task<Result<bool>> DeleteAsync(Guid id);
    }
}
