using Microsoft.Extensions.Configuration;
using System;
using System.Data.Common;

namespace DbProviderFactoriesSandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            Startup startup = new Startup();
            IConfiguration configuration = startup.CreateConfiguration();
            DbProviderFactoryAutoRegister.Register(configuration);


            Program pg = new Program();
            pg.Run();
        }

        private void Run()
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory("Npgsql");
            DbConnection conn = factory.CreateConnection();

            //「Npgsql.NpgsqlConnection」取得できている
            System.Diagnostics.Debug.WriteLine(conn.GetType().FullName);
        }
    }
}
