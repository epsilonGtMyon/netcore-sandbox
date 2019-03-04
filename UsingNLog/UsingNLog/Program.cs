using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace UsingNLog
{
    class Program
    {
        //ILoggerをインジェクションして使う

        private readonly ILogger<Program> _logger;

        public Program(
            ILogger<Program> logger)
        {
            _logger = logger;
        }

        static void Main(string[] args)
        {
            Startup sp = new Startup();
            IConfiguration configuration = sp.CreateConfiguration();

            IServiceCollection serviceCollection = new ServiceCollection();
            sp.ConfigureService(configuration, serviceCollection);
            IServiceProvider service = serviceCollection.BuildServiceProvider();

            Program program = ActivatorUtilities.GetServiceOrCreateInstance<Program>(service);
            program.Run();
        }

        private void Run()
        {
            _logger.LogInformation("開始します。");
            try
            {
                try
                {
                    throw new Exception("test");
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "例外");
                }

                _logger.LogInformation("終了します。");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "異常終了します。");
                throw;
            }

        }
    }
}
