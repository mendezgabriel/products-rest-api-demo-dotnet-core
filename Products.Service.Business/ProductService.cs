using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Products.Service.Domain;
using Products.Service.Interfaces.Business;
using Products.Service.Interfaces.Repository;

namespace Products.Service.Business
{
    /// <inheritdoc />
    public class ProductService : IProductService
    {
        private readonly ILogger _logger;
        private readonly IProductRepository _productRepository;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="logger">The application logger.</param>
        /// <param name="productRepository">The product repository.</param>
        public ProductService(ILogger logger,
            IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        /// <inheritdoc />
        public async Task<Result<ListResult<Product>>> GetAsync(string name)
        {
            _logger.Debug("Getting products by name. Name: {name}", name);

            var products = await _productRepository.GetAsync(name);

            return Result<ListResult<Product>>.Success(new ListResult<Product>
            {
                Items = products
            });
        }

        /// <inheritdoc />
        public async Task<Result<Product>> CreateAsync(Product product)
        {
            _logger.Debug("Creating a product. Product: {product}", JsonConvert.SerializeObject(product));

            var duplicateProduct = (await _productRepository.GetAsync(product.Name, true)).FirstOrDefault();
            if(duplicateProduct != null)
            {
                return Result<Product>.Failed(ErrorCode.DuplicateProductFound,
                    AppConstants.DuplicateProductFoundErrorMessage.Replace("[name]", duplicateProduct.Name));
            }

            var createdProduct = await _productRepository.CreateAsync(product);
            return Result<Product>.Success(createdProduct);
        }

        /// <inheritdoc />
        public async Task<Result<bool>> DeleteAsync(Guid id)
        {
            _logger.Debug("Deleting a product. ProductId: {productId}", id);

            var product = await _productRepository.GetByIdAsync(id);
            if(product == null)
            {
               return Result<bool>.Failed(ErrorCode.ProductNotFound, AppConstants.ResourceNotFoundErrorMessage);
            }

            var isSuccess = await _productRepository.DeleteAsync(id);
            return isSuccess
                 ? Result<bool>.Success(true)
                 : Result<bool>.Failed(ErrorCode.DownstreamFailure, AppConstants.DownstreamFailure);
        }

        /// <inheritdoc />
        public async Task<Result<Product>> GetByIdAsync(Guid id)
        {
            _logger.Debug("Getting product by Id. ProductId: {productId}", id);

            var product = await _productRepository.GetByIdAsync(id);

            return product != null
                ? Result<Product>.Success(product)
                : Result<Product>.Failed(ErrorCode.ProductNotFound, AppConstants.ResourceNotFoundErrorMessage);
        }

        /// <inheritdoc />
        public async Task<Result<bool>> UpdateAsync(Product product)
        {
            _logger.Debug("Updating a product. Product: {product}", JsonConvert.SerializeObject(product));

            var productFromRepo = await _productRepository.GetByIdAsync(product.Id);
            if (productFromRepo == null)
            {
                return Result<bool>.Failed(ErrorCode.ProductNotFound, AppConstants.ResourceNotFoundErrorMessage);
            }

            var isSuccess = await _productRepository.UpdateAsync(product);
            return isSuccess 
                ? Result<bool>.Success(true)
                : Result<bool>.Failed(ErrorCode.DownstreamFailure, AppConstants.DownstreamFailure);
        }
    }
}
