using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Products.Service.Domain;

namespace Products.Service.WebApi.Logging
{
    [ExcludeFromCodeCoverage]
    public static class Logger
    {
        private static readonly AppConfiguration AppConfiguration = ConfigurationManager.Get();
        private const string DefaultLogOutputTemplate =
            "{Timestamp:yyyy-MM-dd HH:mm:ss,fff} [{ThreadId}] {Level} [CorrelationId:{CorrelationId}] [Machine:{MachineName}] {Message}{NewLine}{Exception}";

        public static ILogger CreateInstance()
        {
            var logLevel = (AppConfiguration.Logging.Level ?? "Debug").ToEnum<LogEventLevel>();
            var appIdentifier = AppConfiguration.Application.Identifier;
            var env = AppConfiguration.Environment;
            var template = AppConfiguration.Logging.OutputTemplate ?? DefaultLogOutputTemplate;

            var config = new LoggerConfiguration()
                .MinimumLevel.Is(logLevel)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("ThreadId", Environment.CurrentManagedThreadId)
                .Enrich.WithProperty("MachineName", Environment.MachineName)
                .Enrich.WithProperty("ApplicationIdentifier", appIdentifier)
                .Enrich.WithProperty("Environment", env);

            // Add any logging output here with a sink. For example: Application Insights, File or Azure storage

#if DEBUG
            config = config.WriteTo.Trace(logLevel, template);
            config = config.WriteTo.Console(logLevel, template);
#endif

            return config.CreateLogger();
        }

        private static TEnum ToEnum<TEnum>(this string value) where TEnum : struct
        {
            return (TEnum)Enum.Parse(typeof(TEnum), value);
        }
    }
}
