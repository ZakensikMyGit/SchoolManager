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
        void DeleteEmployee(int EmployeeId);
        int AddEmployee(Employee employee);
        IQueryable<Employee> GetEmployeesByPositionId(int positionId);
        Employee GetEmployeeById(int employeeId);
        public IQueryable<Position> GetAllPositions();
    }
}
