using Serilog;

namespace CleanArchitecture.Web.Extensions;

internal static class ConfigureHostBuilderExtensions
{
    internal static ConfigureHostBuilder AddSerilog(this ConfigureHostBuilder hostBuilder)
    {
        hostBuilder.UseSerilog((context, configuration) =>
        {
            configuration.ReadFrom.Configuration(context.Configuration);
            configuration.Enrich.With(new SourceContextEnricher());
        });
        return hostBuilder;
    }
}