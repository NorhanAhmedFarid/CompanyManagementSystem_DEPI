using CompanyG02.BLL.Interfaces;
using CompanyG02.DAL.Contexts;
using CompanyG02.DAL.Models;

namespace CompanyG02.BLL.Repositories
{
    public class MyServiceRepository : GenericRepository<MyService>, IMyServiceRepository
    {
        public MyServiceRepository(CompanyDbContext dbContext) : base(dbContext)
        {

        }
    }
}
