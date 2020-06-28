using System.ComponentModel.DataAnnotations.Schema;

namespace EfCoreSandbox1.App.Common.Dbs.Entity
{
    [Table("dept")]
    public class Dept : AbstractEntity
    {
        [Column("dept_code")]
        public string DeptCode { get; set; }

        [Column("dept_name")]
        public string DeptName { get; set; }
    }
}
