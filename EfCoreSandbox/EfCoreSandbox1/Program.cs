using EfCoreSandbox1.App.Common.Dbs;
using EfCoreSandbox1.App.Common.Dbs.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace EfCoreSandbox1
{
    class Program
    {
        private const string connectionString = "Host=localhost;Database=ef_test_db;Username=test_user;Password=test_user";

        static void Main(string[] args)
        {
            IServiceProvider provider = CreateServiceProvider();

            Main01 m = provider.GetService<Main01>();
            m.Start();
        }

        private static IServiceProvider CreateServiceProvider()
        {
            ServiceCollection services = new ServiceCollection();

            services.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();
            });
            services.AddSingleton<MyDbTransactionInterceptor>();

            services.AddDbContextPool<MyDbContext>((sp, optionsBuilder) =>
            {
                optionsBuilder.EnableDetailedErrors();

                ILoggerFactory loggerFactory = sp.GetService<ILoggerFactory>();
                optionsBuilder.UseLoggerFactory(loggerFactory);

                MyDbTransactionInterceptor transactionInterceptor = sp.GetService<MyDbTransactionInterceptor>();
                optionsBuilder.AddInterceptors(transactionInterceptor);

                optionsBuilder.UseNpgsql(connectionString);
            });


            services.AddTransient<Main01>();

            return services.BuildServiceProvider();
        }
    }
}
