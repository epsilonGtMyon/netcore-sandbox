using Dapper;
using MyLocalization;
using MyLocalizationUsage.MyLocalization.Dto;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace MyLocalizationUsage.MyLocalization
{
    /// <summary>
    /// MyLocalizedStringsのプロバイダ
    /// 
    /// データベースからローカライズ用の文字列の一覧を取得するようにした。
    /// </summary>
    public class MyLocalizedStringsProvider : IMyLocalizedStringsProvider
    {
        private const string connectionString = "Host=localhost;Database=test_db;Username=test_user;Password=test_user";

        public IMyLocalizedStrings GetLocalizedStrings(Type resourceSource)
        {
            using IDbConnection con = new NpgsqlConnection(connectionString);
            con.Open();
            using IDbTransaction tran = con.BeginTransaction();


            string sql = @"
SELECT
   key_name as Key
  ,ja       as Ja
  ,en       as En
FROM
   localization_resource
ORDER BY
  key
";
            IEnumerable<LocalizationRecord> records = con.Query<LocalizationRecord>(sql, param: null, tran);
            return MyLocalizedStrings.FromEnumerable(records);

        }
    }
}
