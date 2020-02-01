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
    public class ProductOptionService : IProductOptionService
    {
        private readonly ILogger _logger;
        private readonly IProductOptionRepository _productOptionRepository;
        private readonly IProductService _productService;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="logger">The application logger.</param>
        /// <param name="productOptionRepository">The product option repository.</param>
        /// <param name="productService">The product business service.</param>
        public ProductOptionService(ILogger logger,
            IProductOptionRepository productOptionRepository,
            IProductService productService)
        {
             _logger = logger;
            _productOptionRepository = productOptionRepository;
            _productService = productService;
        }

        /// <inheritdoc />
        public async Task<Result<ListResult<ProductOption>>> GetByProductAsync(Guid productId)
        {
            _logger.Debug("Getting product options by product. ProductId: {productId}", productId);

            var productResult = await _productService.GetByIdAsync(productId);
            if(!productResult.IsSuccess())
            {
                return Result<ListResult<ProductOption>>.Failed(productResult.Error);
            }

            var productOptions = await _productOptionRepository.GetByProductAsync(productId);

            return Result<ListResult<ProductOption>>.Success(new ListResult<ProductOption>
            {
                Items = productOptions
            });
        }

        /// <inheritdoc />
        public async Task<Result<ProductOption>> GetByIdAsync(Guid productId, Guid id)
        {
            _logger.Debug("Getting product options by Id. ProductId: {productId}, ProductOptionId: {productOptionId}", productId, id);

            var productResult = await _productService.GetByIdAsync(productId);
            if (!productResult.IsSuccess())
            {
                return Result<ProductOption>.Failed(productResult.Error);
            }

            var productOption = await _productOptionRepository.GetByIdAsync(id);

            return productOption != null
                ? Result<ProductOption>.Success(productOption)
                : Result<ProductOption>.Failed(ErrorCode.ProductOptionNotFound, AppConstants.ResourceNotFoundErrorMessage);
        }

        /// <inheritdoc />
        public async Task<Result<ProductOption>> CreateAsync(ProductOption productOption)
        {
            _logger.Debug("Creating a product option. ProductOption: {productOption}", JsonConvert.SerializeObject(productOption));

            var duplicateOptionResult = await GetByProductAsync(productOption.ProductId);
            if (!duplicateOptionResult.IsSuccess())
            {
                return Result<ProductOption>.Failed(duplicateOptionResult.Error);
            }

            var duplicateOption = duplicateOptionResult.Data.Items
                .FirstOrDefault(x => x.Name.Equals(productOption.Name, StringComparison.InvariantCultureIgnoreCase));
            if(duplicateOption != null)
            {
                return Result<ProductOption>.Failed(ErrorCode.DuplicateProductOptionFound,
                    AppConstants.DuplicateProductOptionFoundErrorMessage.Replace("[name]", productOption.Name));
            }

            var createdProductOption = await _productOptionRepository.CreateAsync(productOption);
            return Result<ProductOption>.Success(createdProductOption);
        }

        /// <inheritdoc />
        public async Task<Result<bool>> UpdateAsync(ProductOption productOption)
        {
            _logger.Debug("Updating a product option. ProductOption: {productOption}", JsonConvert.SerializeObject(productOption));

            var productOptionResult = await GetByIdAsync(productOption.ProductId, productOption.Id);
            if (!productOptionResult.IsSuccess())
            {
                return Result<bool>.Failed(productOptionResult.Error);
            }

            var isSuccess = await _productOptionRepository.UpdateAsync(productOption);
            return isSuccess
                ? Result<bool>.Success(true)
                : Result<bool>.Failed(ErrorCode.DownstreamFailure, AppConstants.DownstreamFailure);
        }

        /// <inheritdoc />
        public async Task<Result<bool>> DeleteAsync(Guid productId, Guid id)
        {
            _logger.Debug("Deleting a product option. ProductId: {productId}, ProductOptionId: {productOptionId}", productId, id);

            var productOptionResult = await GetByIdAsync(productId, id);
            if (!productOptionResult.IsSuccess())
            {
                return Result<bool>.Failed(productOptionResult.Error);
            }

            var isSuccess = await _productOptionRepository.DeleteAsync(id);
            return isSuccess
                 ? Result<bool>.Success(true)
                 : Result<bool>.Failed(ErrorCode.DownstreamFailure, AppConstants.DownstreamFailure);
        }
    }
}
