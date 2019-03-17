using NPoco;
using System.Text;

namespace SqliteSandbox.Common.Entity
{
    [TableName("juchu")]
    [PrimaryKey("juchu_no,juchu_edno", AutoIncrement = false)]
    public class Juchu
    {
        [Column("juchu_no")]
        public string JuchuNo { get; set; }

        [Column("juchu_edno")]
        public int JuchuEdno { get; set; }

        [Column("product_name")]
        public string ProductName { get; set; }

        [Column("product_count")]
        public int? ProductCount { get; set; }


        public override string ToString()
        {
            StringBuilder buf = new StringBuilder();
            buf.Append("Juchu[");
            buf.Append(nameof(JuchuNo)).Append("=").Append(JuchuNo).Append(", ");
            buf.Append(nameof(JuchuEdno)).Append("=").Append(JuchuEdno).Append(", ");
            buf.Append(nameof(ProductName)).Append("=").Append(ProductName).Append(", ");
            buf.Append(nameof(ProductCount)).Append("=").Append(ProductCount);
            buf.Append("]");
            return buf.ToString();
        }
    }
}
