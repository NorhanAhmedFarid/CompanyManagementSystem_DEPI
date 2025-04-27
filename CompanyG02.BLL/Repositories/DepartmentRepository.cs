using CompanyG02.BLL.Interfaces;
using CompanyG02.DAL.Contexts;
using CompanyG02.DAL.Models;

namespace CompanyG02.BLL.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(CompanyDbContext dbContext) : base(dbContext)
        {

        }
        #region MyRegion

        //DI
        //private readonly CompanyDbContext dbContext;
        //public DepartmentRepository(CompanyDbContext dbContext) //Ask CLR For Object
        //{
        //   // dbContext = new CompanyDbContext(); //New Object
        //   this.dbContext = dbContext;
        //}
        //public int Add(Department department)
        //{
        //    dbContext.Departments.Add(department);
        //    return dbContext.SaveChanges();
        //}

        //public int Delete(Department department)
        //{

        //    dbContext.Departments.Remove(department);
        //    return dbContext.SaveChanges();
        //}

        //public Department Get(int id) 
        //    => dbContext.Find<Department>(id);

        //public IEnumerable<Department> GetAll()
        //           => dbContext.Departments.ToList();


        //public int Update(Department department)
        //{
        //    dbContext.Departments.Update(department);
        //    return dbContext.SaveChanges();
        //}
        #endregion

    }
}
