using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SandboxWebApp
{
    /// <summary>
    /// The Program file.
    /// </summary>
    public sealed class Program
    {
        /// <summary>
        /// The Main method.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Creates the host builder.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>The web host builder.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder
                .UseStartup<Startup>();
            }).ConfigureLogging((hostContext, loggingProvider) =>
            {
                loggingProvider.ClearProviders();
                loggingProvider.AddConsole();
                loggingProvider.AddAzureWebAppDiagnostics();
            });
        }
    }
}
