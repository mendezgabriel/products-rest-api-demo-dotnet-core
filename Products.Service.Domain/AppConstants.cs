using System;
using System.Collections.Generic;
using System.Text;

namespace Products.Service.Domain
{
    /// <summary>
    /// Defines this app's constants.
    /// </summary>
    public static class AppConstants
    {
        public const string InvalidRequest = "The request is invalid.";
        public const string DownstreamFailure = "Downstream Failure(s).";
        public const string FailedToProcessRequest = "Failed to process request.";
        public const string ResourceNotFoundErrorMessage = "Requested resource cannot be found.";
        public const string DuplicateProductFoundErrorMessage = "A product with the name of '[name]' already exists.";
        public const string DuplicateProductOptionFoundErrorMessage = "A product option with the name of '[name]' already exists.";
    }
}
