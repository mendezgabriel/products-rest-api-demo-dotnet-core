using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Products.Service.Domain;

namespace Products.Service.WebApi
{
    public class ErrorResponse
    {
        public Error Error { get; set; }

        public ErrorResponse(Error error)
        {
            Error = error;
        }
    }
}
