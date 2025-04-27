using CompanyG02.DAL.Models;
using System.Linq;

namespace CompanyG02.BLL.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        public IQueryable<Employee> GetEmployeesByAdress(string Adress);
        public IQueryable<Employee> GetEmpByName(string Name);
        #region MyRegion



        #endregion
    }
}
