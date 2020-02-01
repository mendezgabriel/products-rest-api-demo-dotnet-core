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
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger _logger;

        public ProductsController(IProductService productService,
            ILogger logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]string name)
        {
            _logger.Information($"Executing GET - '/products?name={name}'");

            if (!ModelState.IsValid)
            {
                return WebApi.Response.Error(new Error(ErrorCode.BadRequest, AppConstants.InvalidRequest));
            }

            try
            {
                name = string.IsNullOrWhiteSpace(name) ? name : name.Trim();
                var result = await _productService.GetAsync(name);
                return result.IsSuccess()
                        ? WebApi.Response.Ok(result.Data)
                        : WebApi.Response.Error(result.Error);
            }
            catch (Exception ex)
            {
                _logger.Error("An error has occurred while getting products. Name: {name}, Details: {message}", name, ex.Message);
            }

            return WebApi.Response.Fatal();
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetByIdAsync(Guid productId)
        {
            _logger.Information($"Executing GET - '/products/{productId}'");

            if (!ModelState.IsValid)
            {
                return WebApi.Response.Error(new Error(ErrorCode.BadRequest, AppConstants.InvalidRequest ));
            }

            try
            {
                var result = await _productService.GetByIdAsync(productId);
                return result.IsSuccess()
                         ? WebApi.Response.Ok(result.Data)
                         : WebApi.Response.Error(result.Error);
            }
            catch (Exception ex)
            {
                _logger.Error("An error has occurred while getting product by Id. ProductId: {productId}. Details: {message}", productId, ex.Message);
            }

            return WebApi.Response.Fatal();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] Product product)
        {
            _logger.Information($"Executing POST - '/products'");

            if (!ModelState.IsValid)
            {
                return WebApi.Response.Error(new Error(ErrorCode.BadRequest, AppConstants.InvalidRequest));
            }

            try
            {
                var result = await _productService.CreateAsync(product);
                return result.IsSuccess()
                        ? WebApi.Response.Ok(result.Data)
                        : WebApi.Response.Error(result.Error);
            }
            catch (Exception ex)
            {
                _logger.Error("An error has occurred while creating a new product. Details: {message}", ex.Message);
            }

            return WebApi.Response.Fatal();
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateAsync(Guid productId, [FromBody] Product product)
        {
            _logger.Information($"Executing PUT - '/products/{productId}'");

            if (!ModelState.IsValid)
            {
                return WebApi.Response.Error(new Error(ErrorCode.BadRequest, AppConstants.InvalidRequest));
            }

            try
            {
                product.Id = productId;
                var result = await _productService.UpdateAsync(product);
                return result.IsSuccess()
                        ? WebApi.Response.NoContent()
                        : WebApi.Response.Error(result.Error);
            }
            catch (Exception ex)
            {
                _logger.Error("An error has occurred while getting updating a product. ProductId: {productId}. Details: {message}", productId, ex.Message);
            }

            return WebApi.Response.Fatal();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteAsync(Guid productId)
        {
            _logger.Information($"Executing DELETE - '/products/{productId}'");

            if (!ModelState.IsValid)
            {
                return WebApi.Response.Error(new Error(ErrorCode.BadRequest, AppConstants.InvalidRequest));
            }

            try
            {
                var result = await _productService.DeleteAsync(productId);
                return result.IsSuccess()
                        ? WebApi.Response.NoContent()
                        : WebApi.Response.Error(result.Error);
            }
            catch (Exception ex)
            {
                _logger.Error("An error has occurred while deleting a product. ProductId: {productId}. Details: {message}", productId, ex.Message);
            }

            return WebApi.Response.Fatal();
        }
    }
}
