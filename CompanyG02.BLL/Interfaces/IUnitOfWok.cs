using System;
using System.Threading.Tasks;

namespace CompanyG02.BLL.Interfaces
{
    public interface IUnitOfWok : IDisposable
    {
        IEmployeeRepository EmployeeRepository { get; set; }
        IDepartmentRepository DepartmentRepository { get; set; }
        IProjectRepository ProjectRepository { get; set; }
        IMyServiceRepository MyServiceRepository { get; set; }
        Task<int> Compelete();

    }
}
