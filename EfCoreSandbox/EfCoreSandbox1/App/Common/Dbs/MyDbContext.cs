using EfCoreSandbox1.App.Common.Dbs.Entity;
using Microsoft.EntityFrameworkCore;

namespace EfCoreSandbox1.App.Common.Dbs
{
    public class MyDbContext : DbContext
    {
        private readonly DbContextOptions<MyDbContext> _options;

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
            _options = options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Emp>()
                .HasKey(c => new { c.EmpCode });

            modelBuilder.Entity<Dept>()
                .HasKey(c => new { c.DeptCode });
        }

        public DbSet<Emp> Emps { get; set; }

        public DbSet<Dept> Depts { get; set; }
    }
}
