using System.ComponentModel.DataAnnotations.Schema;

namespace EfCoreSandbox1.App.Common.Dbs.Entity
{

    [Table("emp")]
    public class Emp : AbstractEntity
    {
        [Column("emp_code")]
        public string EmpCode { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("family_name")]
        public string FamilyName { get; set; }

        [Column("dept_code")]
        public string DeptCode { get; set; }
    }
}
