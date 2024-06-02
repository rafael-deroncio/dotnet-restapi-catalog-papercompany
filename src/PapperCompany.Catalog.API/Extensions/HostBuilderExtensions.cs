using PapperCompany.Catalog.Core.Configurations.Logger;
using Serilog;
using Serilog.Exceptions;

namespace PapperCompany.Catalog.API.Extensions;

public static class HostBuilderExtensions
{
    public static IHostBuilder UseSerilog(this IHostBuilder hostBuilder)
    {
        hostBuilder.UseSerilog((context, config) =>
            config.MinimumLevel.Information()
                  .MinimumLevel.ControlledBy(LoggingLevelSwitcher._instance)
                //   .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Error)
                //   .MinimumLevel.Override("System", Serilog.Events.LogEventLevel.Error)
                  .Enrich.With(new CustomEnricher(context.Configuration["AppDetails:Name"]))
                  .Enrich.WithProperty("Application", context.Configuration["ApplicationName"])
                  .Enrich.WithProperty("Envioroment", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"))
                  .Enrich.FromLogContext()
                  .Enrich.WithExceptionDetails()
                  .WriteTo.Console()
                  );
        return hostBuilder;
    }
}
