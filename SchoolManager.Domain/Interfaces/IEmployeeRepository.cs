using SchoolManager.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        IQueryable<Employee> GetAllActiveEmployees();
        Employee GetEmployee(int employeeId);
        int AddEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(int EmployeeId);
    }
}
