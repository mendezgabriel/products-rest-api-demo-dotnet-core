using System;
using System.Collections.Generic;
using System.Text;

namespace Products.Service.Domain
{
    /// <summary>
    /// An item that can be purchased or sold.
    /// </summary>
    public class Product : NamedEntity<Guid>
    {
        /// <summary>
        /// The product's price.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// The price of delivering the product to the shipping address.
        /// </summary>
        public decimal DeliveryPrice { get; set; }
    }
}
