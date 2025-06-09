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
        private readonly IPositionRepository _positionRepository;
        private readonly IMapper _mapper;
        public EmlpoyeeService(IEmployeeRepository employeeRepo, IPositionRepository positionRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepo;
            _positionRepository = positionRepository;
            _mapper = mapper;
        }

        public int AddEmployee(NewEmployeeVm model)
        {
            if (model.Id == 0)
            {
                var employeeEntity = _mapper.Map<Employee>(model);
                employeeEntity.IsActive = true;
                var employeeId = _employeeRepository.AddEmployee(employeeEntity);
                return employeeId;
            }
            else
            {
                var employee = _employeeRepository.GetEmployee(model.Id);
                if (employee != null)
                {
                    _mapper.Map(model, employee);
                    employee.PositionId = model.PositionId;
                    _employeeRepository.UpdateEmployee(employee);

                    return employee.Id;
                }
                return 0;
            }
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

        public List<Position> GetAllPositions()
        {
            return _positionRepository.GetAllPositions();
        }

        public NewEmployeeVm GetEmployeeForEdit(int id)
        {
            var employee = _employeeRepository.GetEmployee(id);
            var employeeVm = _mapper.Map<NewEmployeeVm>(employee);
            return employeeVm;
        }
    }
}
