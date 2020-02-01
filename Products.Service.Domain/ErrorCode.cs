using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Products.Service.Domain
{
    [JsonConverter(typeof(StringEnumConverter))]
    /// <summary>
    /// Defines the possible error codes used in the system.
    /// </summary>
    public enum ErrorCode
    {
        /// <summary>
        /// Operation is not authorized.
        /// </summary>
        Unauthorized,

        /// <summary>
        /// Request is invalid.
        /// </summary>
        InvalidRequest,

        /// <summary>
        /// Request can't be processed.
        /// </summary>
        BadRequest,

        /// <summary>
        /// An unhandled/unexpected failure has occurred.
        /// </summary>
        DownstreamFailure,

        /// <summary>
        /// Not enough privileges to execute the operation.
        /// </summary>
        Forbidden,

        /// <summary>
        /// Product could not be found.
        /// </summary>
        ProductNotFound,

        /// <summary>
        /// Product duplicate was found.
        /// </summary>
        DuplicateProductFound,

        /// <summary>
        /// Product option could not be found.
        /// </summary>
        ProductOptionNotFound,

        /// <summary>
        /// Product option duplicate was found.
        /// </summary>
        DuplicateProductOptionFound
    }
}
