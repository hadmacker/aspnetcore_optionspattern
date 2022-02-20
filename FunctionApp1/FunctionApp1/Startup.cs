using Microsoft.Azure.WebJobs;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using FunctionApp1.Options;
using Microsoft.Extensions.Configuration;
using Serilog;
using FunctionApp1;

[assembly: FunctionsStartup(typeof(Startup))]
namespace FunctionApp1
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            //[Azure Functions Configuration](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-6.0)
            builder.Services.AddOptions<NamedOptions>("NamedOptions1")
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.GetSection("NamedOptions1").Bind(settings);
                });
            builder.Services.AddOptions<NamedOptions>("NamedOptions2")
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.GetSection("NamedOptions2").Bind(settings);
                });

            //[Serilog.net](https://serilog.net/)
            var logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
            _ = builder.Services.AddLogging(lb => lb.AddSerilog(logger));
        }
    }
}
