using Serilog.Core;
using Serilog.Events;

namespace CleanArchitecture.Web.Extensions;

internal class SourceContextEnricher : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        if (logEvent.Properties.TryGetValue("SourceContext", out var property))
        {
            var scalarValue = property as ScalarValue;
            var value = scalarValue?.Value as string;

            if (value?.Contains('.') ?? false)
            {
                var lastElement = value.Split(".", StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
                if (!string.IsNullOrWhiteSpace(lastElement))
                {
                    logEvent.AddOrUpdateProperty(
                        new LogEventProperty("SourceContext", new ScalarValue(lastElement)));
                }
            }
        }
    }
}