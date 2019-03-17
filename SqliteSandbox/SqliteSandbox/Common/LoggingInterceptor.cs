using NPoco;
using System.Data.Common;

namespace SqliteSandbox.Common
{
    public class LoggingInterceptor : IExecutingInterceptor
    {
        public void OnExecutingCommand(IDatabase database, DbCommand cmd)
        {
            System.Diagnostics.Debug.WriteLine($@"OnExecutingCommand
{((Database)database).FormatCommand(cmd)}
");
        }


        public void OnExecutedCommand(IDatabase database, DbCommand cmd)
        {
            //noop
        }
    }
}
