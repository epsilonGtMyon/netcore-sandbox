using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HostedServiceSandbox.Service
{
    /// <summary>
    /// BackgroundServiceを使う
    /// </summary>
    public class MyHostedService02 : BackgroundService
    {
        private readonly ILogger<MyHostedService02> _logger;
        private readonly IHostApplicationLifetime _appLifetime;

        public MyHostedService02(
                ILogger<MyHostedService02> logger,
                IHostApplicationLifetime appLifetime
            )
        {
            _logger = logger;
            _appLifetime = appLifetime;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                for (int i = 1; i <= 5; i++)
                {
                    _logger.LogInformation($"{i}回目");
                    await Task.Delay(1000, stoppingToken);
                }
            }
            finally
            {
                _appLifetime.StopApplication();
            }
        }
    }
}
