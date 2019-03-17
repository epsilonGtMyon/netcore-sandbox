using NPoco;
using SqliteSandbox.Common;
using SqliteSandbox.Common.Entity;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace SqliteSandbox
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DbProviderFactories.RegisterFactory("System.Data.SQLite", "System.Data.SQLite.SQLiteFactory, System.Data.SQLite");
            Program p = new Program();
            p.Start();
        }

        private void Start()
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SQLite");

            using (DbConnection con = factory.CreateConnection())
            {
                con.ConnectionString = @"Data Source=..\mydb.db;Version=3";
                con.Open();
                using (Database db = new Database(con, DatabaseType.Resolve("SQLite", "")))
                {
                    db.Interceptors.Add(new LoggingInterceptor());

                    db.BeginTransaction();
                    try
                    {
                        AppLog appLog = new AppLog
                        {
                            LogText = "log:" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
                        };
                        db.Insert(appLog);


                       List<Juchu> juchus =  db.Query<Juchu>()
                                            .Where(x => x.JuchuNo == "X0001")
                                            .OrderBy(x => x.JuchuEdno)
                                            .ToList();

                        juchus.ForEach(x => System.Diagnostics.Debug.WriteLine(x.ToString()));

                        db.CompleteTransaction();
                    }
                    catch
                    {
                        db.AbortTransaction();

                        throw;
                    }
                    
                }
            }
        }
    }
}
