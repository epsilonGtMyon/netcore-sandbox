using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfCoreSandbox1.App.Common.Dbs.Entity
{
    public abstract class AbstractEntity
    {

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}
