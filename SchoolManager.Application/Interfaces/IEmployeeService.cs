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
        ListEmployeeForListVm GetAllEmployeesForList();
        IQueryable<Employee> GetAllActiveEmployee();
        Employee GetEmployee(int EmployeeId);
        EmployeeDetailsVm GetEmployeeDetails(int employeeId);
        int AddEmployee(NewEmployeeVm model);
        NewEmployeeVm GetEmployeeForEdit(int employeeId);
        void DeleteEmployee(int employeeId);

        Task<ListEmployeeForListVm> GetAllEmployeesForListAsync();
        Task<List<Employee>> GetAllActiveEmployeeAsync();
        Task<Employee?> GetEmployeeAsync(int employeeId);
        Task<EmployeeDetailsVm> GetEmployeeDetailsAsync(int employeeId);
        Task<int> AddEmployeeAsync(NewEmployeeVm model);
        Task<NewEmployeeVm> GetEmployeeForEditAsync(int employeeId);
        Task DeleteEmployeeAsync(int employeeId);
    }
}
}
