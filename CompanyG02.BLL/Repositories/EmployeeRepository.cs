using CompanyG02.BLL.Interfaces;
using CompanyG02.DAL.Contexts;
using CompanyG02.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyG02.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly CompanyDbContext dbContext;

        public EmployeeRepository(CompanyDbContext dbContext):base(dbContext) 
        {
            this.dbContext = dbContext;
        }

        public IQueryable<Employee> GetEmpByName(string name)
       =>dbContext.Employees.Where(E => E.Name.ToLower().Contains(name.ToLower()));

        public IQueryable<Employee> GetEmployeesByAdress(string Adress)
        {
        return dbContext.Employees.Where(e => e.Adress == Adress);
        }
        #region MyRegion

        // private readonly CompanyDbContext dbContext;

        // public EmployeeRepository(CompanyDbContext dbContext)
        // {
        //     this.dbContext = dbContext;
        // }
        // public int Add(Employee employee)
        // {
        //     dbContext.Employees.Add(employee);
        //     return dbContext.SaveChanges();
        // }

        // public int Delete(Employee employee)
        // {
        //     dbContext.Employees.Remove(employee);
        //     return dbContext.SaveChanges();
        // }

        // public Employee Get(int id)
        //=> dbContext.Employees.Find(id);

        // public IEnumerable<Employee> GetAll()
        //       => dbContext.Employees.ToList();


        // public int Update(Employee employee)
        // {
        //     dbContext.Employees.Update(employee);
        //     return dbContext.SaveChanges();
        // }
        #endregion
    }
}
