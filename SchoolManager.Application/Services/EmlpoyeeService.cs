using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using SchoolManager.Application.Interfaces;
using SchoolManager.Application.ViewModels.Employee;
using SchoolManager.Domain.Interfaces;
using SchoolManager.Domain.Model;

namespace SchoolManager.Application.Services
{
    public class EmlpoyeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public EmlpoyeeService(IEmployeeRepository employeeRepo, IMapper mapper)
        {
            _employeeRepository = employeeRepo;
            _mapper = mapper;
        }
        public int AddEmployee(NewEmployeeVm model)
        {
            return model.Id;
        }

        public IQueryable<Employee> GetAllActiveEmployee()
        {
            throw new NotImplementedException();
        }

        public ListEmployeeForListVm GetAllEmployeesForList()
        {
            var employees = _employeeRepository.GetAllActiveEmployees()
                .ProjectTo<EmployeeForListVm>(_mapper.ConfigurationProvider).ToList();
            
            var employeeList = new ListEmployeeForListVm
            {
                Employees = employees,
                Count = employees.Count
            };

            return employeeList;
        }

        public Employee GetEmployee(int EmployeeId)
        {
            throw new NotImplementedException();
        }

        public EmployeeDetailsVm GetEmployeeDetails(int employeeId)
        {
            var employee = _employeeRepository.GetEmployee(employeeId);
            var employeeVm = _mapper.Map<EmployeeDetailsVm>(employee);

            
            return employeeVm;
        }
    }
}
