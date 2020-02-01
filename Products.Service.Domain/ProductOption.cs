using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Products.Service.Domain
{
    /// <summary>
    /// The configuration in which a product is available.
    /// </summary>
    public class ProductOption : NamedEntity<Guid>
    {
        [JsonIgnore]
        public Guid ProductId { get; set; }
    }
}
