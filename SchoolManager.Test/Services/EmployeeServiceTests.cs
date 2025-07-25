using AutoMapper;
using Moq;
using SchoolManager.Application.Interfaces;
using SchoolManager.Application.Services;
using SchoolManager.Application.ViewModels.Schedule;
using SchoolManager.Domain.Interfaces;
using SchoolManager.Domain.Model;
using SchoolManager.Test.Services;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SchoolManager.Test.Services
{
    public class EmployeeServiceTests
    {
        private EmployeeService CreateService(Mock<IEmployeeRepository> repo)
        {
            var mapper = new MapperConfiguration(cfg => { }).CreateMapper();
            return new EmployeeService(
                repo.Object,
                new Mock<IPositionRepository>().Object,
                new Mock<IGroupRepository>().Object,
                new Mock<IScheduleRepository>().Object,
                mapper);
        }

        [Fact]
        public async Task GetAllActiveEmployee_ReturnsAllActiveEmployeesAsync()
        {
            var repo = new Mock<IEmployeeRepository>();
            var employees = new List<Employee> { new Employee { Id = 1 }, new Employee { Id = 2 } };
            repo.Setup(r => r.GetAllActiveEmployeesAsync()).ReturnsAsync(employees);
            var service = CreateService(repo);

            var result = await service.GetAllActiveEmployeeAsync();

            Assert.Equal(2, result.Count);
            Assert.Equal(1, result[0].Id);
        }

        [Fact]
        public async Task GetEmployee_ReturnsEmployeeAsync()
        {
            var repo = new Mock<IEmployeeRepository>();
            var employee = new Employee { Id = 5 };
            repo.Setup(r => r.GetEmployeeAsync(5)).ReturnsAsync(employee);
            var service = CreateService(repo);

            var result = await service.GetEmployeeAsync(5);

            Assert.Same(employee, result);
        }
    }
}
