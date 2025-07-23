using SchoolManager.Application.ViewModels.Employee;
using SchoolManager.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<ListEmployeeForListVm> GetAllEmployeesForListAsync();
        Task<List<Employee>> GetAllActiveEmployeeAsync();
        Task<Employee?> GetEmployeeAsync(int employeeId);
        Task<EmployeeDetailsVm> GetEmployeeDetailsAsync(int employeeId);
        Task<int> AddEmployeeAsync(NewEmployeeVm model);
        Task<NewEmployeeVm> GetEmployeeForEditAsync(int employeeId);
        Task DeleteEmployeeAsync(int employeeId);

    }
}
