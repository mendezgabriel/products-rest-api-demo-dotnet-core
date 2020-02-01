using System;
using System.Collections.Generic;
using System.Text;

namespace Products.Service.Domain
{
    /// <summary>
    /// Provides easy support for returning an application error.
    /// </summary>
    public class Error
    {
        /// <summary>
        /// An enumeration of type <see cref="ErrorCode"/> identifying the error.
        /// </summary>
        public ErrorCode Code { get; set; }

        /// <summary>
        /// The message that goes along with the <see cref="Code"/>.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="errorCode">An enumeration of type <see cref="ErrorCode"/> identifying the error.</param>
        /// <param name="errorMessage">The message that goes along with the <paramref name="errorCode"/>.</param>
        public Error(ErrorCode errorCode, string errorMessage)
        {
            Code = errorCode;
            Message = errorMessage;
        }
    }
}
