using System;
using System.Collections.Generic;
using System.Text;

namespace Products.Service.Domain
{
    /// <summary>
    /// Defines the result of a listing operation.
    /// </summary>
    /// <typeparam name="TResult">The type of the result items.</typeparam>
    public class ListResult<TResult> where TResult: class
    {
        /// <summary>
        /// The items to be included in the result.
        /// </summary>
        public IList<TResult> Items { get; set; } = new List<TResult>();
    }
}
