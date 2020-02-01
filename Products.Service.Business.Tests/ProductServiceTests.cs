using System;
using NSubstitute;
using Serilog;
using AutoFixture;
using Products.Service.Interfaces.Repository;
using Products.Service.Domain;
using System.Collections.Generic;

namespace Products.Service.Business.Tests
{
    public partial class ProductServiceTests
    {
        private readonly Fixture _fixture = new Fixture();
        private readonly ILogger _logger = Substitute.For<ILogger>();
        private readonly IProductRepository _productRepository = Substitute.For<IProductRepository>();
        private readonly ProductService _systemUnderTest;
        private readonly List<Product> _getResults;

        public ProductServiceTests()
        {
            // Arrange
            _systemUnderTest = new ProductService(_logger, _productRepository);

            _getResults = _fixture.Create<List<Product>>();

            SetUpGetAsyncSuccess(_getResults);
        }
    }
}
