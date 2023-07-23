using System.Reflection;
using MassTransit;

namespace CleanArchitecture.Infrastructure.MessageBroker;

public class CustomNameFormater : IEntityNameFormatter
{
    private readonly IEntityNameFormatter _original;
    private readonly MessageBrokerSettings _config;
    private readonly string? _appName;

    public CustomNameFormater(IEntityNameFormatter original, MessageBrokerSettings config)
    {
        _original = original;
        _config = config;
        _appName = Assembly.GetEntryAssembly()?.GetName().Name;
    }
    
    public string FormatEntityName<T>() =>
        string.IsNullOrWhiteSpace(_appName) ? _original.FormatEntityName<T>() : $"{_appName}-out";
}