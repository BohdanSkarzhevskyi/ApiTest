using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;

namespace ApiTest.Utils.Extensions
{
    public static class RegisterSerilogServices
    {
        /// <summary>
        /// Extension to register Serilog service
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSerilogServices(this IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console()
                .CreateLogger();

            AppDomain.CurrentDomain.ProcessExit += (s, e) => Log.CloseAndFlush();

            return services.AddSingleton(Log.Logger);
        }
    }
}
