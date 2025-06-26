using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Moq;
using SchoolManager.Application.Services;
using SchoolManager.Application.ViewModels.Schedule;
using SchoolManager.Domain.Interfaces;
using SchoolManager.Domain.Model;
using Xunit;

namespace SchoolManager.Test.Services
{
    public class ScheduleServiceTests
    {
        private readonly Mock<IScheduleRepository> _scheduleRepoMock = new();
        private readonly ScheduleService _service;

        public ScheduleServiceTests()
        {
            var mapper = new MapperConfiguration(cfg => { }).CreateMapper();
            _service = new ScheduleService(_scheduleRepoMock.Object, mapper);
        }

        [Fact]
        public void IsScheduleEntryValid_ReturnsTrue_WhenNoOverlap()
        {
            _scheduleRepoMock.Setup(r => r.GetByTeacher(1))
                .Returns(new List<ScheduleEntry>().AsQueryable());

            var entry = new NewScheduleEntryVm
            {
                EmployeeId = 1,
                StartTime = new DateTime(2024, 1, 1, 8, 0, 0),
                EndTime = new DateTime(2024, 1, 1, 10, 0, 0)
            };

            var result = _service.IsScheduleEntryValid(entry);

            Assert.True(result);
        }

        [Fact]
        public void IsScheduleEntryValid_ReturnsFalse_WhenOverlap()
        {
            var existing = new List<ScheduleEntry>
            {
                new ScheduleEntry
                {
                    StartTime = new DateTime(2024,1,1,8,0,0),
                    EndTime = new DateTime(2024,1,1,10,0,0)
                }
            }.AsQueryable();
            _scheduleRepoMock.Setup(r => r.GetByTeacher(1)).Returns(existing);

            var entry = new NewScheduleEntryVm
            {
                EmployeeId = 1,
                StartTime = new DateTime(2024, 1, 1, 9, 0, 0),
                EndTime = new DateTime(2024, 1, 1, 11, 0, 0)
            };

            var result = _service.IsScheduleEntryValid(entry);

            Assert.False(result);
        }

        [Fact]
        public void IsScheduleEntryValid_ReturnsTrue_WhenTouchingEdges()
        {
            var existing = new List<ScheduleEntry>
            {
                new ScheduleEntry
                {
                    StartTime = new DateTime(2024,1,1,8,0,0),
                    EndTime = new DateTime(2024,1,1,10,0,0)
                }
            }.AsQueryable();
            _scheduleRepoMock.Setup(r => r.GetByTeacher(1)).Returns(existing);

            var entry = new NewScheduleEntryVm
            {
                EmployeeId = 1,
                StartTime = new DateTime(2024, 1, 1, 10, 0, 0),
                EndTime = new DateTime(2024, 1, 1, 12, 0, 0)
            };

            var result = _service.IsScheduleEntryValid(entry);

            Assert.True(result);
        }

        [Fact]
        public void IsScheduleEntryValid_EditIgnoresSelf()
        {
            var existing = new List<ScheduleEntry>
            {
                new ScheduleEntry { Id = 1, StartTime = new DateTime(2024,1,1,8,0,0), EndTime = new DateTime(2024,1,1,10,0,0) },
                new ScheduleEntry { Id = 2, StartTime = new DateTime(2024,1,1,12,0,0), EndTime = new DateTime(2024,1,1,14,0,0) }
            }.AsQueryable();
            _scheduleRepoMock.Setup(r => r.GetByTeacher(1)).Returns(existing);

            var entry = new EditScheduleEntryVm
            {
                Id = 1,
                EmployeeId = 1,
                StartTime = new DateTime(2024, 1, 1, 8, 0, 0),
                EndTime = new DateTime(2024, 1, 1, 10, 0, 0)
            };

            var result = _service.IsScheduleEntryValid(entry);

            Assert.True(result);
        }

        [Fact]
        public void IsScheduleEntryValid_EditDetectsOverlap()
        {
            var existing = new List<ScheduleEntry>
            {
                new ScheduleEntry { Id = 1, StartTime = new DateTime(2024,1,1,8,0,0), EndTime = new DateTime(2024,1,1,10,0,0) },
                new ScheduleEntry { Id = 2, StartTime = new DateTime(2024,1,1,12,0,0), EndTime = new DateTime(2024,1,1,14,0,0) }
            }.AsQueryable();
            _scheduleRepoMock.Setup(r => r.GetByTeacher(1)).Returns(existing);

            var entry = new EditScheduleEntryVm
            {
                Id = 1,
                EmployeeId = 1,
                StartTime = new DateTime(2024, 1, 1, 13, 0, 0),
                EndTime = new DateTime(2024, 1, 1, 15, 0, 0)
            };

            var result = _service.IsScheduleEntryValid(entry);

            Assert.False(result);
        }
    }
}
