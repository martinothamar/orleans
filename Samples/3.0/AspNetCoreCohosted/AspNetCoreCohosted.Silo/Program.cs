using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Orleans.Configuration;
using Orleans.Hosting;

namespace AspNetCoreCohosted.Silo
{
    public class Program
    {
        public static Task Main(string[] args)
        {
            return CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseOrleans((context, builder) =>
                {
                    builder
                        .Configure<SiloMessagingOptions>(opts =>
                        {
                            opts.PropagateActivityId = true;
                        })
                        .UseLocalhostClustering()
                        .AddMemoryGrainStorageAsDefault();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
