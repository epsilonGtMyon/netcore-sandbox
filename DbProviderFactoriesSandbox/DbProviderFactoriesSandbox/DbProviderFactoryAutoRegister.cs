using System.Collections.Generic;
using System.Data.Common;
using Microsoft.Extensions.Configuration;

namespace DbProviderFactoriesSandbox
{
    public class DbProviderFactoryAutoRegister
    {
        public static void Register(IConfiguration configuration)
        {
            IEnumerable<IConfigurationSection> section = configuration.GetSection("DbProviderFactories").GetChildren();

            foreach (IConfigurationSection child in section)
            {
                string key = child.Key;

                string invariant = child["invariant"];
                string type = child["type"];
                
                DbProviderFactories.RegisterFactory(invariant, type);
            }
        }
    }
}
