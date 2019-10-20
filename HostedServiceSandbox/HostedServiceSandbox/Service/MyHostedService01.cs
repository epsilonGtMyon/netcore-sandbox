using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HostedServiceSandbox.Service
{
    /// <summary>
    /// IHostedServiceを直接実装したもの
    /// </summary>
    public class MyHostedService01 : IHostedService
    {
        private readonly ILogger<MyHostedService01> _logger;
        private readonly IHostApplicationLifetime _appLifetime;
        private readonly IHostEnvironment _hostEnv;

        private readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();

        public MyHostedService01(
                ILogger<MyHostedService01> logger,
                IHostApplicationLifetime appLifetime,
                IHostEnvironment hostEnv
            )
        {
            _logger = logger;
            _appLifetime = appLifetime;
            _hostEnv = hostEnv;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("StartAsync!");

            _appLifetime.ApplicationStarted.Register(async x =>
            {
                MyHostedService01 self = (MyHostedService01)x;


                try
                {
                    await self.RunMain(self._stoppingCts.Token);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "例外");
                }
                finally
                {
                    self._appLifetime.StopApplication();
                }
            }, this);


            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("StopAsync!");
            _stoppingCts.Cancel();
            return Task.CompletedTask;
        }

        //-------------------------------------

        private async Task RunMain(CancellationToken stoppingToken)
        {
            for (int i = 1; i <= 5; i++)
            {
                _logger.LogInformation($"{i}回目");
                await Task.Delay(1000);
            }
        }
    }

}
