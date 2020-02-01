using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Products.Service.Domain;

namespace Products.Service.WebApi
{
    public static class Response
    {
        public static IActionResult Error(Error error) => ToObjectResult(MapErrorToStatus(error), new ErrorResponse(error));

        public static IActionResult Ok<TData>(TData data) => new OkObjectResult(data);

        public static IActionResult Created<TData>(TData data) => ToObjectResult(HttpStatusCode.Created, data);

        public static IActionResult NoContent() => new NoContentResult();

        public static IActionResult Fatal() => ToObjectResult(HttpStatusCode.InternalServerError,
            new ErrorResponse(new Error(ErrorCode.DownstreamFailure, AppConstants.FailedToProcessRequest)));


        private static ObjectResult ToObjectResult<TData>(HttpStatusCode statusCode, TData data)
        {
            var result = new ObjectResult(data)
            {
                StatusCode = (int)statusCode               
            };

            return result;
        }

        private static readonly Dictionary<ErrorCode, HttpStatusCode> ErrorMap = new Dictionary<ErrorCode, HttpStatusCode>
        {
            { ErrorCode.DownstreamFailure, HttpStatusCode.InternalServerError },
            { ErrorCode.BadRequest, HttpStatusCode.BadRequest },
            { ErrorCode.InvalidRequest, HttpStatusCode.BadRequest },
            { ErrorCode.DuplicateProductFound, HttpStatusCode.BadRequest },
            { ErrorCode.DuplicateProductOptionFound, HttpStatusCode.BadRequest },
            { ErrorCode.Forbidden, HttpStatusCode.Forbidden },
            { ErrorCode.Unauthorized, HttpStatusCode.Unauthorized },
            { ErrorCode.ProductNotFound, HttpStatusCode.NotFound },
            { ErrorCode.ProductOptionNotFound, HttpStatusCode.NotFound }
        };

        private static HttpStatusCode MapErrorToStatus(Error error)
        {
            if (!ErrorMap.ContainsKey(error.Code))
                throw new NotSupportedException("Unsupported Error Code.");

            return ErrorMap[error.Code];
        }
    }
}
