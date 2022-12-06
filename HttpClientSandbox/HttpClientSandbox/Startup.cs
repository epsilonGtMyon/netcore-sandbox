using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HttpClientSandbox
{
	internal class Startup
	{
		public IConfiguration CreateConfiguration()
		{
			return new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: true)
				.Build();
		}

		public void ConfigureService(IConfiguration configuration, IServiceCollection service)
		{
			service.AddSingleton(typeof(IConfiguration), configuration);
			service.AddSingleton<HttpClientProvider>();


			service.AddTransient<Sandbox01>();

			service.AddLogging(builder =>
			{
				builder.SetMinimumLevel(LogLevel.Trace);
				builder.AddConsole();
				builder.AddDebug();
			});

		}
	}
}
