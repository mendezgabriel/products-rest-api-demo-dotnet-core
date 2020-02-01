using FluentAssertions;
using FluentAssertions.Execution;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Products.Service.Domain;
using Xunit;

namespace Products.Service.Business.Tests
{
    public partial class ProductServiceTests
    {
        [Fact]
        public async Task GetAsync_Should_LogEvents()
        {
            // Act
            await GetAsync();

            // Assert          
            _logger.ReceivedCalls().Count().Should().BeGreaterOrEqualTo(1);
        }

        [Fact]
        public async Task GetAsync_WhenSuccess_Should_ReturnSuccessfulResponse()
        {
            // Act
            var actual = await GetAsync();

            // Assert          
            using (new AssertionScope())
            {
                actual.IsSuccess().Should().BeTrue();
                actual.Error.Should().BeNull();
                actual.Data.Items.Should().BeOfType<List<Product>>();
                actual.Data.Items.Should().NotBeNullOrEmpty();
                actual.Data.Items.Should().BeEquivalentTo(_getResults);
            }
        }

        [Fact]
        public async Task GetAsync_WhenSuccessAndNoData_Should_ReturnSuccessfulEmptyResponse()
        {
            // Arrange
            _getResults.Clear();

            // Act
            var actual = await GetAsync();

            // Assert          
            using (new AssertionScope())
            {
                actual.IsSuccess().Should().BeTrue();
                actual.Error.Should().BeNull();
                actual.Data.Items.Should().BeOfType<List<Product>>();
                actual.Data.Items.Should().NotBeNull().And.BeEmpty();
            }
        }

        [Fact]
        public void GetAsync_WhenRepositoryThrowsException_Should_BubbleUpException()
        {
            // Arrange
            var exception = new Exception("Mocked Db exception");
            SetUpGetAsyncFailure(exception);

            // Act
            var ex = Assert.ThrowsAsync<Exception>(
                async () => await GetAsync());

            // Assert
            ex.Result.Should().BeEquivalentTo(exception);
        }

        private async Task<Result<ListResult<Product>>> GetAsync(string name = null) =>
            await _systemUnderTest.GetAsync(name);

        private void SetUpGetAsyncSuccess(List<Product> response) =>
            _productRepository.GetAsync(Arg.Any<string>(), Arg.Any<bool>())
                .Returns(response);

        private void SetUpGetAsyncFailure(Exception ex = null)
        {
            ex = ex ?? new Exception();
            _productRepository.GetAsync(Arg.Any<string>(), Arg.Any<bool>()).Throws(ex);
        }
    }
}
