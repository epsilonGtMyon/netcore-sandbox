using EfCoreSandbox1.App.Common.Dbs;
using EfCoreSandbox1.App.Common.Dbs.Entity;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace EfCoreSandbox1
{
    public class Main01
    {
        private readonly ILogger<Main01> _logger;
        private readonly MyDbContext _dbContext;

        public Main01(ILogger<Main01> logger, MyDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public void Start()
        {
            _logger.LogInformation("開始");

            //Do01();
            //Do02();
        }

        private void Do01()
        {
            using (IDbContextTransaction tran = _dbContext.Database.BeginTransaction())
            {

                //List<Emp> emps; _dbContext.Emps.Where(x => x.EmpId == "001").ToList();
                Emp emp = _dbContext.Emps.Find("E0001");

                emp.FirstName = "へんこー";

                _dbContext.SaveChanges();

                tran.Commit();
            }
        }

        private void Do02()
        {
            using (IDbContextTransaction tran = _dbContext.Database.BeginTransaction())
            {
                _dbContext.Emps.RemoveRange();


                _dbContext.Emps.Add(new Emp
                {
                    EmpCode = "E001"
                });

                _dbContext.Emps.Add(new Emp
                {
                    EmpCode = "E002"
                });

                _dbContext.SaveChanges();

                tran.Commit();
            }

        }
    }
}
