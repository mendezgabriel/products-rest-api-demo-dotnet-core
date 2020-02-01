using System;
using System.Collections.Generic;
using System.Text;

namespace Products.Service.Domain
{
    public class AppConfiguration
    {
        public Application Application { get; set; }

        public string Environment { get; set; }

        public Logging Logging { get; set; }

        public ConnectionStrings ConnectionStrings { get; set; }
    }

    public class Application
    {
        public string Identifier { get; set; }
    }

    public class Logging
    {
        public string Level { get; set; }

        public string OutputTemplate { get; set; }
    }

    public class ConnectionStrings
    {
        public string Database { get; set; }
    }
}
