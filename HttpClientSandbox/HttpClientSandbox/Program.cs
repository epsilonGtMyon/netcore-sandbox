using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HttpClientSandbox
{
	class Program
	{

		private readonly ILogger<Program> _logger;

		private readonly Sandbox01 _sandbox01;

		public Program(
			ILogger<Program> logger,
			Sandbox01 sandbox01
			)
		{
			_logger = logger;
			_sandbox01 = sandbox01;
		}

		static async Task Main(string[] args)
		{
			Startup sp = new Startup();
			IConfiguration configuration = sp.CreateConfiguration();

			IServiceCollection serviceCollection = new ServiceCollection();
			sp.ConfigureService(configuration, serviceCollection);
			IServiceProvider service = serviceCollection.BuildServiceProvider();

			Program program = ActivatorUtilities.GetServiceOrCreateInstance<Program>(service);
			await program.Run();
		}
		private async Task Run()
		{
			_logger.LogInformation("開始します。");
			try
			{
				try
				{
					await _sandbox01.Execute();
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