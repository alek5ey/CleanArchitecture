namespace CleanArchitecture.Web.Extensions;

public static class ApplicationConfigurationExtensions
{
    public static ConfigurationManager AddConfigurationSources(this ConfigurationManager configurationManager, string[] args)
    {
        configurationManager
            .AddJsonFile("appsettings.json", false, true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true, true)
            .AddCommandLine(args)
            .AddEnvironmentVariables();

        return configurationManager;
    }
}