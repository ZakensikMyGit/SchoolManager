using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManager.Domain.Model;

namespace SchoolManager.Infrastructure.Repositories
{
    public class EmployeeRepository
    {
        private readonly Context _context;
        public EmployeeRepository(Context context)
        {
            _context = context;
        }

        public void DeleteEmployee(int EmployeeId)
        {
            var employee = _context.Employees.Find(EmployeeId);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
        }

        public int AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return employee.Id;
        }

        public IQueryable<Employee> GetEmployeesByPositionId(int positionId)
        {
            var employees = _context.Employees.Where(e => e.PositionId == positionId);
            return employees;
        }
        public Employee GetEmployeeById(int employeeId)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Id == employeeId);
            return employee;
        }

        public IQueryable<Position> GetAllPositions()
        {
            var positions = _context.Positions;
            return positions;
        }
    }
}
