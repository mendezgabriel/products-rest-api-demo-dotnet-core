using AutoFixture;
using NSubstitute;
using Serilog;
using System;
using System.Collections.Generic;
using Products.Service.Domain;
using Products.Service.Interfaces.Business;
using Products.Service.WebApi.Controllers;
using Xunit;

namespace Products.Service.WebApi.Tests
{
    public partial class ProductControllerTests
    {
        private readonly Fixture _fixture = new Fixture();
        private readonly ILogger _logger = Substitute.For<ILogger>();
        private readonly IProductService _productService = Substitute.For<IProductService>();
        private readonly ProductsController _systemUnderTest;
        private readonly Result<ListResult<Product>> _getProductsResult;
        private readonly ListResult<Product> _productsList;

        public ProductControllerTests()
        {
            // Arrange
            _systemUnderTest = new ProductsController(_productService, _logger);

            _productsList = _fixture.Create<ListResult<Product>>();
            _getProductsResult = Result<ListResult<Product>>.Success(_productsList);

            SetUpGetAsyncSuccess(_getProductsResult);
        }
        

    }
}
