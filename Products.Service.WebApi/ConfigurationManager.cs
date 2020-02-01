using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Products.Service.Domain;

namespace Products.Service.WebApi
{
    /// <summary>
    /// The configuration manager class
    /// </summary>
    public static class ConfigurationManager
    {
        private static bool HasBeenInitialized { get; set; }
        private static IConfigurationRoot _config;

        /// <summary>
        /// Initializes the configuration settings.
        /// </summary>
        public static void Initialize()
        {
            var basePath = Directory.GetCurrentDirectory();
            Initialize(basePath);
        }

        /// <summary>
        /// Initializes the configuration settings for the specified <paramref name="basePath"/>
        /// and <paramref name="environmentName"/>.
        /// </summary>
        public static void Initialize(string basePath, string environmentName = "local")
        {
            if (HasBeenInitialized)
                return;

            var builder = new ConfigurationBuilder();
            builder.SetBasePath(basePath)
                .AddJsonFile("local.settings.json", true, true)
                .AddJsonFile("appsettings.json", true, true);

            if (environmentName == "local")
                builder.AddJsonFile($"appsettings.{environmentName}.json", true, true);

            builder.AddEnvironmentVariables();

            _config = builder.Build();

            HasBeenInitialized = true;
        }

        /// <summary>
        /// Gets the application configuration from the settings file
        /// </summary>
        /// <returns> The Application configuration <see cref="AppConfiguration"/></returns>
        public static AppConfiguration Get()
        {
            var appConfiguration = new AppConfiguration();
            _config.GetSection("appConfiguration").Bind(appConfiguration);
            appConfiguration.ConnectionStrings = new ConnectionStrings
            {
                Database = _config.GetConnectionString("databaseConnectionString")
            };
            return appConfiguration;
        }


    }
}
