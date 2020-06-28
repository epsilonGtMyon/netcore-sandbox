using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System;
using System.Data.Common;
using System.Diagnostics;

namespace EfCoreSandbox1.App.Common.Dbs.Interceptors
{
    public class MyDbTransactionInterceptor : DbTransactionInterceptor
    {
        private readonly ILogger<MyDbTransactionInterceptor> _logger;

        public MyDbTransactionInterceptor(ILogger<MyDbTransactionInterceptor> logger)
        {
            _logger = logger;
        }

        public override void TransactionCommitted(DbTransaction transaction, TransactionEndEventData eventData)
        {
            Debug.WriteLine(Environment.StackTrace);

            _logger.LogInformation("TransactionCommitted: {0}", eventData.TransactionId);
            base.TransactionCommitted(transaction, eventData);
        }

        public override InterceptionResult TransactionCommitting(DbTransaction transaction, TransactionEventData eventData, InterceptionResult result)
        {
            _logger.LogInformation("TransactionCommitting: {0}", eventData.TransactionId);
            return base.TransactionCommitting(transaction, eventData, result);
        }

        public override InterceptionResult TransactionRollingBack(DbTransaction transaction, TransactionEventData eventData, InterceptionResult result)
        {
            _logger.LogInformation("TransactionRollingBack: {0}", eventData.TransactionId);
            return base.TransactionRollingBack(transaction, eventData, result);
        }

        public override DbTransaction TransactionStarted(DbConnection connection, TransactionEndEventData eventData, DbTransaction result)
        {
            //Debug.WriteLine(Environment.StackTrace);

            _logger.LogInformation("TransactionStarted: {0}", eventData.TransactionId);
            return base.TransactionStarted(connection, eventData, result);
        }

        public override DbTransaction TransactionUsed(DbConnection connection, TransactionEventData eventData, DbTransaction result)
        {
            _logger.LogInformation("TransactionUsed: {0}", eventData.TransactionId);
            return base.TransactionUsed(connection, eventData, result);
        }
    }
}
