using System;
using System.Collections.Generic;
using System.Text;

namespace Products.Service.Domain
{
    /// <summary>
    /// Represents a resulting response object that will be returned to the caller after an operation
    /// was executed.
    /// </summary>
    /// <typeparam name="TData">The type of the resulting object to return.</typeparam>
    public class Result<TData>
    {
        private Result(TData data) => Data = data;

        private Result(Error error) => Error = error;

        /// <summary>
        /// The data to return when there's no errors.
        /// </summary>
        public TData Data { get; }

        /// <summary>
        /// The error to return if the operation failed.
        /// </summary>
        public Error Error { get; }

        /// <summary>
        /// Determined if the result was successful or not.
        /// </summary>
        /// <returns>True if <see cref="Error"/> is null. False otherwise.</returns>
        public bool IsSuccess() => Error == null;

        /// <summary>
        /// Creates a successful response that should return the specified <typeparamref name="TData"/>.
        /// </summary>
        /// <param name="data">The resulting object to return.</param>
        /// <returns></returns>
        public static Result<TData> Success(TData data) => new Result<TData>(data);

        /// <summary>
        /// Creates a failed response that should return the specified <see cref="Error"/>.
        /// </summary>
        /// <param name="error">The resulting error to return.</param>
        /// <returns></returns>
        public static Result<TData> Failed(Error error) => new Result<TData>(error);

        /// <summary>
        /// Creates a failed response that should return the specified <paramref name="errorCode"/> and
        /// <paramref name="errorMessage"/>.
        /// </summary>
        /// <param name="errorCode">The type of error to return according to the <see cref="ErrorCode"/>
        /// enumeration.</param>
        /// <param name="errorMessage">The error message associated.</param>
        /// <returns></returns>
        public static Result<TData> Failed(ErrorCode errorCode, string errorMessage) =>
            Failed(new Error(errorCode, errorMessage));
    }
}
