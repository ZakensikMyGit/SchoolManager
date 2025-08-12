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
using SchoolManager.Domain.Enums;
using SchoolManager.Domain.Interfaces;
using SchoolManager.Domain.Model;

namespace SchoolManager.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private const string Message = "Id parametru jest niepoprawne.";
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPositionRepository _positionRepository;
        public readonly IScheduleRepository _scheduleRepository;
        private readonly IMapper _mapper;
        public EmployeeService(IEmployeeRepository employeeRepo, IPositionRepository positionRepository, IScheduleRepository scheduleRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepo;
            _positionRepository = positionRepository;
            _scheduleRepository = scheduleRepository;
            _mapper = mapper;
        }

        public async Task<int> AddEmployeeAsync(NewEmployeeVm model)
        {
            return model.Id == 0
                ? await CreateEmployeeAsync(model)
                : await EditEmployeeAsync(model);
        }

        private async Task<int> CreateEmployeeAsync(NewEmployeeVm model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model), "Problem z pobraniem danych.");

            Position? position = null;
            if (model.PositionId.HasValue && model.PositionId.Value != 0)
            {
                position = await _positionRepository.GetPositionByIdAsync(model.PositionId.Value);
            }
            var isTeacher = IsTeacherPosition(position);

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
            employeeEntity.EmploymentDate = DateTime.SpecifyKind(employeeEntity.EmploymentDate, DateTimeKind.Utc);

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
            await UpdateTeacherGroupAsync(employeeId, model.PositionId, model.Group);

            return employeeId;
        }

        private async Task<int> EditEmployeeAsync(NewEmployeeVm model)
        {
            var employee = await _employeeRepository.GetEmployeeAsync(model.Id);
            if (employee == null)
                return 0;

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
            employee.EmploymentDate = DateTime.SpecifyKind(employee.EmploymentDate, DateTimeKind.Utc);
            await _employeeRepository.UpdateEmployeeAsync(employee);
            await UpdateTeacherGroupAsync(employee.Id, model.PositionId, model.Group);
            return employee.Id;
        }

        // Change the UpdateTeacherGroupAsync method signature to accept GroupEnum? group
        private async Task UpdateTeacherGroupAsync(int teacherId, int? positionId, GroupEnum? group)
        {
            if (!group.HasValue || !positionId.HasValue || teacherId <= 0)
            {
                return;
            }

            var position = await _positionRepository.GetPositionByIdAsync(positionId.Value);
            if (position == null)
                return;

            if (!IsTeacherPosition(position))
                return;

            var teacher = await _employeeRepository.GetEmployeeAsync(teacherId) as Teacher;
            if (teacher == null)
                return;

            teacher.Group = group;
            await _employeeRepository.UpdateEmployeeAsync(teacher);
        }

        private bool IsTeacherPosition(Position position)
        {
            if (position == null)
                return false;

            var cat = position.Category?.ToLowerInvariant() ?? "";
            var name = position.Name?.ToLowerInvariant() ?? "";

            return cat.Contains("teacher")
                || cat.Contains("nauczyciel")
                || name.Contains("teacher")
                || name.Contains("nauczyciel");
        }

        public Task<List<Employee>> GetAllActiveEmployeeAsync()
        {
            return _employeeRepository.GetAllActiveEmployeesAsync();
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
        public Task<Employee?> GetEmployeeAsync(int employeeId)
        {
            if (employeeId <= 0)
                throw new ArgumentOutOfRangeException(
                        nameof(employeeId),
                        employeeId,
                        Message
                    );
            return _employeeRepository.GetEmployeeAsync(employeeId);
        }
        public async Task<EmployeeDetailsVm> GetEmployeeDetailsAsync(int employeeId)
        {
            if (employeeId <= 0)
                throw new ArgumentOutOfRangeException(
                        nameof(employeeId),
                        employeeId,
                        Message
                    );
            var employee = await _employeeRepository.GetEmployeeAsync(employeeId);
            var employeeVm = _mapper.Map<EmployeeDetailsVm>(employee);

            if (employee != null)
            {
                employeeVm.DateOfEmployment = employee.EmploymentDate;

                employeeVm.Education = employee.Educations != null && employee.Educations.Any()
                    ? string.Join(", ", employee.Educations.Select(e => e.Name))
                    : string.Empty;

                Position? position = null;
                if (employee.PositionId.HasValue && employee.PositionId.Value != 0)
                {
                    position = await _positionRepository.GetPositionByIdAsync(employee.PositionId.Value);
                }

                if (employee.Group.HasValue)
                {
                    employeeVm.GroupName = employee.Group.Value.ToString();
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
                        employeeVm.PositionName = position?.Name;
                        employeeVm.PensumHours = 25;
                    }
                }
                else
                {
                    employeeVm.IsDirector = false;
                    employeeVm.PositionName = position?.Name;
                    employeeVm.PensumHours = 40;
                }
            }

            return employeeVm;
        }
        public async Task<List<Position>> GetAllPositionsAsync()
        {
            return await _positionRepository.GetAllPositionsAsync();
        }

        public async Task<NewEmployeeVm> GetEmployeeForEditAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(
                        nameof(id),
                        id,
                        "Id parametru jest niepoprawne."
                    );
            var employee = await _employeeRepository.GetEmployeeAsync(id);
            var employeeVm = _mapper.Map<NewEmployeeVm>(employee);
            if (employee != null && employee.Educations != null && employee.Educations.Any())
            {
                employeeVm.Education = string.Join(", ", employee.Educations.Select(e => e.Name));
            }
            return employeeVm;
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(
                        nameof(id),
                        id,
                        "Id parametru jest niepoprawne."
                    );

            var schedules = await _scheduleRepository.GetByTeacherAsync(id);
            foreach (var entry in schedules)
            {
                await _scheduleRepository.DeleteScheduleEntryAsync(entry.Id);
            }

            await _employeeRepository.DeleteEmployeeAsync(id);
        }
    }
}
