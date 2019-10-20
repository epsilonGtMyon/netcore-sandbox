using HostedServiceSandbox.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace HostedServiceSandbox
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();
            await host.RunAsync();
        }


        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    //IHostedServiceを実装しているクラスを登録する。

                    //services.AddHostedService<MyHostedService01>();
                    services.AddHostedService<MyHostedService02>();
                })
                .ConfigureAppConfiguration((hostBuilderContext, configurationBuilder) =>
                {

                })
                ;
        }
    }
}
