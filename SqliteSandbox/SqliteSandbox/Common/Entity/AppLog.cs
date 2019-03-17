using NPoco;

namespace SqliteSandbox.Common.Entity
{
    [TableName("app_log")]
    [PrimaryKey("id", AutoIncrement = true)]
    public class AppLog
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("log_text")]
        public string LogText { get; set; }
    }
}
