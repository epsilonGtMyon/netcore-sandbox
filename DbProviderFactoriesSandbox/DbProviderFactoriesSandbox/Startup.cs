using Microsoft.Extensions.Configuration;
using System.IO;

namespace DbProviderFactoriesSandbox
{
    public class Startup
    {
        public IConfiguration CreateConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("dbproviderfactories.json")
                .Build();
        }
    }
}
