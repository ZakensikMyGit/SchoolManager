using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPositionRepository _positionRepository;
        public readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;
        public EmployeeService(IEmployeeRepository employeeRepo, IPositionRepository positionRepository,IGroupRepository groupRepository ,IMapper mapper)
        {
            _employeeRepository = employeeRepo;
            _positionRepository = positionRepository;
            _groupRepository = groupRepository;
            _mapper = mapper;
        }

        public int AddEmployee(NewEmployeeVm model)
        {
            if (model.Id == 0)
            {
                var position = _positionRepository.GetPositionById(model.PositionId);
                bool isTeacher = position != null &&
                                 ((position.Category?.IndexOf("teacher", StringComparison.OrdinalIgnoreCase) >= 0) ||
                                  (position.Category?.IndexOf("nauczyciel", StringComparison.OrdinalIgnoreCase) >= 0) ||
                                  (position.Name?.IndexOf("teacher", StringComparison.OrdinalIgnoreCase) >= 0) ||
                                  (position.Name?.IndexOf("nauczyciel", StringComparison.OrdinalIgnoreCase) >= 0));

                Employee employeeEntity;
                if (isTeacher)
                {
                    var teacher = _mapper.Map<Teacher>(model);
                    teacher.TypeTeacher = "wychowawca";
                    teacher.IsDirector = false;
                    teacher.PensumHours = 25;
                    employeeEntity = teacher;
                }
                else
                {
                    employeeEntity = _mapper.Map<Employee>(model);
                }

                employeeEntity.WorkingHours = 1;
                employeeEntity.IsActive = true;

                if (!string.IsNullOrWhiteSpace(model.Education))
                {
                    employeeEntity.Educations = new List<Education>
                    {
                        new Education
                        {
                            Name = model.Education,
                            Description = string.Empty,
                            Type = string.Empty
                        }
                    };
                }
                var employeeId = _employeeRepository.AddEmployee(employeeEntity);

                UpdateTeacherGroup(employeeId, model.PositionId, model.GroupId);
                return employeeId;
            }
            else
            {
                var employee = _employeeRepository.GetEmployee(model.Id);
                if (employee != null)
                {
                    _mapper.Map(model, employee);
                    employee.PositionId = model.PositionId;

                    if (!string.IsNullOrWhiteSpace(model.Education))
                    {
                        var existing = employee.Educations?.FirstOrDefault();
                        if (existing == null)
                        {
                            employee.Educations = new List<Education>
                            {
                                new Education
                                {
                                    Name = model.Education,
                                    Description = string.Empty,
                                    Type = string.Empty
                                }
                            };
                        }
                        else
                        {
                            existing.Name = model.Education;
                        }
                    }
                    else if (employee.Educations != null && employee.Educations.Any())
                    {
                        employee.Educations.Clear();
                    }

                    _employeeRepository.UpdateEmployee(employee);

                    UpdateTeacherGroup(employee.Id, model.PositionId, model.GroupId);
                    return employee.Id;
                }
                return 0;
            }
        }
        public async Task<int> AddEmployeeAsync(NewEmployeeVm model)
        {
            if (model.Id == 0)
            {
                var position = _positionRepository.GetPositionById(model.PositionId);
                bool isTeacher = position != null &&
                                 ((position.Category?.IndexOf("teacher", StringComparison.OrdinalIgnoreCase) >= 0) ||
                                  (position.Category?.IndexOf("nauczyciel", StringComparison.OrdinalIgnoreCase) >= 0) ||
                                  (position.Name?.IndexOf("teacher", StringComparison.OrdinalIgnoreCase) >= 0) ||
                                  (position.Name?.IndexOf("nauczyciel", StringComparison.OrdinalIgnoreCase) >= 0));

                Employee employeeEntity;
                if (isTeacher)
                {
                    var teacher = _mapper.Map<Teacher>(model);
                    teacher.TypeTeacher = "wychowawca";
                    teacher.IsDirector = false;
                    teacher.PensumHours = 25;
                    employeeEntity = teacher;
                }
                else
                {
                    employeeEntity = _mapper.Map<Employee>(model);
                }

                employeeEntity.WorkingHours = 1;
                employeeEntity.IsActive = true;

                if (!string.IsNullOrWhiteSpace(model.Education))
                {
                    employeeEntity.Educations = new List<Education>
                    {
                        new Education
                        {
                            Name = model.Education,
                            Description = string.Empty,
                            Type = string.Empty
                        }
                    };
                }
                var employeeId = await _employeeRepository.AddEmployeeAsync(employeeEntity);

                UpdateTeacherGroup(employeeId, model.PositionId, model.GroupId);
                return employeeId;
            }
            else
            {
                var employee = await _employeeRepository.GetEmployeeAsync(model.Id);
                if (employee != null)
                {
                    _mapper.Map(model, employee);
                    employee.PositionId = model.PositionId;

                    if (!string.IsNullOrWhiteSpace(model.Education))
                    {
                        var existing = employee.Educations?.FirstOrDefault();
                        if (existing == null)
                        {
                            employee.Educations = new List<Education>
                            {
                                new Education
                                {
                                    Name = model.Education,
                                    Description = string.Empty,
                                    Type = string.Empty
                                }
                            };
                        }
                        else
                        {
                            existing.Name = model.Education;
                        }
                    }
                    else if (employee.Educations != null && employee.Educations.Any())
                    {
                        employee.Educations.Clear();
                    }

                    await _employeeRepository.UpdateEmployeeAsync(employee);

                    UpdateTeacherGroup(employee.Id, model.PositionId, model.GroupId);
                    return employee.Id;
                }
                return 0;
            }
        }
        private void UpdateTeacherGroup(int teacherId, int positionId, int? groupId)
        {
            if (!groupId.HasValue)
            {
                return;
            }

            var position = _positionRepository.GetPositionById(positionId);
            if (position == null)
            {
                return;
            }

            bool isTeacher = (position.Category?.IndexOf("teacher", StringComparison.OrdinalIgnoreCase) >= 0
                               || position.Category?.IndexOf("nauczyciel", StringComparison.OrdinalIgnoreCase) >= 0
                               || position.Name?.IndexOf("teacher", StringComparison.OrdinalIgnoreCase) >= 0
                               || position.Name?.IndexOf("nauczyciel", StringComparison.OrdinalIgnoreCase) >= 0);

            if (!isTeacher)
            {
                return;
            }

            var group = _groupRepository.GetGroup(groupId.Value);
            if (group == null)
            {
                return;
            }

            group.TeacherId = teacherId;
            _groupRepository.UpdateGroup(group);
        }

        public IQueryable<Employee> GetAllActiveEmployee()
        {
            return _employeeRepository.GetAllActiveEmployees();
        }
        public Task<List<Employee>> GetAllActiveEmployeeAsync()
        {
            return _employeeRepository.GetAllActiveEmployeesAsync();
        }
        public ListEmployeeForListVm GetAllEmployeesForList()
        {
            var employees = _employeeRepository.GetAllActiveEmployees()
                .ProjectTo<EmployeeForListVm>(_mapper.ConfigurationProvider)
                .ToList();

            foreach (var empVm in employees)
            {
                var employeeEntity = _employeeRepository.GetEmployee(empVm.Id);
                if (employeeEntity is Teacher teacher && teacher.IsDirector)
                {
                    empVm.PositionName = "Dyrektor";
                }
            }

            var employeeList = new ListEmployeeForListVm
            {
                Employees = employees,
                Count = employees.Count
            };

            return employeeList;
        }
        public async Task<ListEmployeeForListVm> GetAllEmployeesForListAsync()
        {
            var employees = await _employeeRepository.GetAllActiveEmployeesAsync();
            var employeeVms = employees.AsQueryable()
                .ProjectTo<EmployeeForListVm>(_mapper.ConfigurationProvider)
                .ToList();

            foreach (var empVm in employeeVms)
            {
                var employeeEntity = employees.FirstOrDefault(e => e.Id == empVm.Id);
                if (employeeEntity is Teacher teacher && teacher.IsDirector)
                {
                    empVm.PositionName = "Dyrektor";
                }
            }

            return new ListEmployeeForListVm
            {
                Employees = employeeVms,
                Count = employeeVms.Count
            };
        }
        public Employee GetEmployee(int EmployeeId)
        {
            return _employeeRepository.GetEmployee(EmployeeId);
        }
        public Task<Employee?> GetEmployeeAsync(int employeeId)
        {
            return _employeeRepository.GetEmployeeAsync(employeeId);
        }
        public EmployeeDetailsVm GetEmployeeDetails(int employeeId)
        {
            var employee = _employeeRepository.GetEmployee(employeeId);
            var employeeVm = _mapper.Map<EmployeeDetailsVm>(employee);

            employeeVm.DateOfEmployment = employee.EmploymentDate;

            employeeVm.Education = employee.Educations != null && employee.Educations.Any()
                ? string.Join(", ", employee.Educations.Select(e => e.Name))
                : string.Empty;

            var position = _positionRepository.GetPositionById(employee.PositionId);

            var group = _groupRepository.GetAllGroups()
                .FirstOrDefault(g => g.TeacherId == employee.Id);
            if (group != null)
            {
                employeeVm.GroupName = group.GroupName;
            }

            if (employee is Teacher teacher)
            {
                employeeVm.IsDirector = teacher.IsDirector;
                if (teacher.IsDirector)
                {
                    employeeVm.PositionName = "Dyrektor";
                    employeeVm.PensumHours = 40;
                }
                else
                {
                    employeeVm.PositionName = position.Name;
                    employeeVm.PensumHours = 25;
                }
            }
            else
            {
                employeeVm.IsDirector = false;
                employeeVm.PositionName = position.Name;
                employeeVm.PensumHours = 40;
            }

            return employeeVm;
        }
        public async Task<EmployeeDetailsVm> GetEmployeeDetailsAsync(int employeeId)
        {
            var employee = await _employeeRepository.GetEmployeeAsync(employeeId);
            var employeeVm = _mapper.Map<EmployeeDetailsVm>(employee);

            if (employee != null)
            {
                employeeVm.DateOfEmployment = employee.EmploymentDate;

                employeeVm.Education = employee.Educations != null && employee.Educations.Any()
                    ? string.Join(", ", employee.Educations.Select(e => e.Name))
                    : string.Empty;

                var position = _positionRepository.GetPositionById(employee.PositionId);

                var group = _groupRepository.GetAllGroups()
                    .FirstOrDefault(g => g.TeacherId == employee.Id);
                if (group != null)
                {
                    employeeVm.GroupName = group.GroupName;
                }

                if (employee is Teacher teacher)
                {
                    employeeVm.IsDirector = teacher.IsDirector;
                    if (teacher.IsDirector)
                    {
                        employeeVm.PositionName = "Dyrektor";
                        employeeVm.PensumHours = 40;
                    }
                    else
                    {
                        employeeVm.PositionName = position.Name;
                        employeeVm.PensumHours = 25;
                    }
                }
                else
                {
                    employeeVm.IsDirector = false;
                    employeeVm.PositionName = position.Name;
                    employeeVm.PensumHours = 40;
                }
            }

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
            if (employee.Educations != null && employee.Educations.Any())
            {
                employeeVm.Education = string.Join(", ", employee.Educations.Select(e => e.Name));
            }
            return employeeVm;
        }

        public async Task<NewEmployeeVm> GetEmployeeForEditAsync(int id)
        {
            var employee = await _employeeRepository.GetEmployeeAsync(id);
            var employeeVm = _mapper.Map<NewEmployeeVm>(employee);
            if (employee != null && employee.Educations != null && employee.Educations.Any())
            {
                employeeVm.Education = string.Join(", ", employee.Educations.Select(e => e.Name));
            }
            return employeeVm;
        }

        public void DeleteEmployee(int id)
        {
            _employeeRepository.DeleteEmployee(id);
        }

        public Task DeleteEmployeeAsync(int id)
        {
            return _employeeRepository.DeleteEmployeeAsync(id);
        }
    }
}
