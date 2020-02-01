using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Products.Service.Domain;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Http;

namespace Products.Service.WebApi.Tests
{
    public partial class ProductControllerTests
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
                actual.StatusCode.Should().Be(StatusCodes.Status200OK);
                actual.Value.Should().BeOfType<ListResult<Product>>();
                actual.Value.Should().Be(_getProductsResult.Data);
            }
        }

        [Fact]
        public async Task GetAsync_PassingABadModel_Should_ReturnBadRequest()
        {
            // Arrange
            _systemUnderTest.ModelState.AddModelError("Simulated", "Model error");

            // Act
            var actual = await GetAsync();

            // Assert
            var expected = new ErrorResponse(new Error(ErrorCode.BadRequest, AppConstants.InvalidRequest));
            using (new AssertionScope())
            {
                actual.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
                actual.Value.Should().BeOfType<ErrorResponse>();
                actual.Value.Should().BeEquivalentTo(expected);
            }
        }

        [Fact]
        public async Task GetAsync_When_AnExceptionOccurs_Should_Return500StatusCode_And_LogError()
        {
            // Arrange
            var exception = new Exception("Unexpected exception");
            SetUpGetByAsyncFailure(exception);

            // Act
            var actual = await GetAsync();

            // Assert
            var expected = new ErrorResponse(new Error(ErrorCode.DownstreamFailure, AppConstants.FailedToProcessRequest));
            using (new AssertionScope())
            {
                actual.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
                actual.Value.Should().BeOfType<ErrorResponse>();
                actual.Value.Should().BeEquivalentTo(expected);
                _logger.Received().Error(Arg.Any<string>(), Arg.Any<string>(), exception.Message);
            }
        }

        private async Task<ObjectResult> GetAsync(string name = null) =>
            await _systemUnderTest.GetAsync(name) as ObjectResult;

        private void SetUpGetAsyncSuccess(Result<ListResult<Product>> fakeResult = null)
        {
            fakeResult = fakeResult ?? _getProductsResult;
            _productService.GetAsync(Arg.Any<string>()).Returns(fakeResult);
        }

        private void SetUpGetByAsyncFailure(Exception ex = null)
        {
            ex = ex ?? new Exception();
            _productService.GetAsync(Arg.Any<string>()).Throws(ex);
        }

        private void SetUpGetByAsyncFailure(Error error = null)
        {
            error = error ?? new Error(ErrorCode.DownstreamFailure, AppConstants.DownstreamFailure);
            _productService.GetAsync(Arg.Any<string>()).Returns(Result<ListResult<Product>>.Failed(error));
        }
    }
}
