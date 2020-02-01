using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Products.Service.Domain;
using Products.Service.Interfaces.Business;

namespace Products.Service.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/products/{productId}/options")]
    public class ProductOptionsController : ControllerBase
    {
        private readonly IProductOptionService _productOptionService;
        private readonly ILogger _logger;

        public ProductOptionsController(IProductOptionService productOptionService,
            ILogger logger)
        {
            _productOptionService = productOptionService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetByProductAsync(Guid productId)
        {
            _logger.Information($"Executing GET - '/products/{productId}/options'");

            if (!ModelState.IsValid)
            {
                return WebApi.Response.Error(new Error(ErrorCode.BadRequest, AppConstants.InvalidRequest));
            }

            try
            {
                var result = await _productOptionService.GetByProductAsync(productId);
                return result.IsSuccess()
                         ? WebApi.Response.Ok(result.Data)
                         : WebApi.Response.Error(result.Error);
            }
            catch (Exception ex)
            {
                _logger.Error("An error has occurred while getting product options. ProductId: {productId}, Details: {message}", productId, ex.Message);
            }

            return WebApi.Response.Fatal();
        }

        [HttpGet("{productOptionId}")]
        public async Task<IActionResult> GetByIdAsync(Guid productId, Guid productOptionId)
        {
            _logger.Information($"Executing GET - '/products/{productId}/options/{productOptionId}'");

            if (!ModelState.IsValid)
            {
                return WebApi.Response.Error(new Error(ErrorCode.BadRequest, AppConstants.InvalidRequest ));
            }

            try
            {
                var result = await _productOptionService.GetByIdAsync(productId, productOptionId);
                return result.IsSuccess()
                         ? WebApi.Response.Ok(result.Data)
                         : WebApi.Response.Error(result.Error);
            }
            catch (Exception ex)
            {
                _logger.Error("An error has occurred while getting product option by Id. ProductId: {productId}, ProductOptionId: {productOptionId}, Details: {message}", productId, productOptionId, ex.Message);
            }

            return WebApi.Response.Fatal();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Guid productId, [FromBody] ProductOption productOption)
        {
            _logger.Information($"Executing POST - '/products/{productId}/options'");

            if (!ModelState.IsValid)
            {
                return WebApi.Response.Error(new Error(ErrorCode.BadRequest, AppConstants.InvalidRequest));
            }

            try
            {
                productOption.ProductId = productId;
                var result = await _productOptionService.CreateAsync(productOption);
                return result.IsSuccess()
                        ? WebApi.Response.Ok(result.Data)
                        : WebApi.Response.Error(result.Error);
            }
            catch (Exception ex)
            {
                _logger.Error("An error has occurred while getting creating a product option. ProductId: {productId}, Details: {message}", productId, ex.Message);
            }

            return WebApi.Response.Fatal();
        }

        [HttpPut("{productOptionId}")]
        public async Task<IActionResult> UpdateAsync(Guid productId, Guid productOptionId, [FromBody] ProductOption productOption)
        {
            _logger.Information($"Executing PUT - '/products/{productId}/options/{productOptionId}'");

            if (!ModelState.IsValid)
            {
                return WebApi.Response.Error(new Error(ErrorCode.BadRequest, AppConstants.InvalidRequest));
            }

            try
            {
                productOption.ProductId = productId;
                productOption.Id = productOptionId;
                var result = await _productOptionService.UpdateAsync(productOption);
                return result.IsSuccess()
                        ? WebApi.Response.NoContent()
                        : WebApi.Response.Error(result.Error);
            }
            catch (Exception ex)
            {
                _logger.Error("An error has occurred while updating a product option. ProductId: {productId}, ProductOptionId: {productOptionId}, Details: {message}", productId, productOptionId, ex.Message);
            }

            return WebApi.Response.Fatal();
        }

        [HttpDelete("{productOptionId}")]
        public async Task<IActionResult> DeleteAsync(Guid productId, Guid productOptionId)
        {
            _logger.Information($"Executing DELETE - '/products/{productId}/options/{productOptionId}'");

            if (!ModelState.IsValid)
            {
                return WebApi.Response.Error(new Error(ErrorCode.BadRequest, AppConstants.InvalidRequest));
            }

            try
            {
                var result = await _productOptionService.DeleteAsync(productId, productOptionId);
                return result.IsSuccess()
                        ? WebApi.Response.NoContent()
                        : WebApi.Response.Error(result.Error);
            }
            catch (Exception ex)
            {
                _logger.Error("An error has occurred while deleting a product option. ProductId: {productId}, ProductOptionId: {productOptionId}, Details: {message}", productId, productOptionId, ex.Message);
            }

            return WebApi.Response.Fatal();
        }
    }
}
