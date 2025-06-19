using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Moq;
using SchoolManager.Application.Mapping;
using SchoolManager.Application.Services;
using SchoolManager.Application.ViewModels.Child;
using SchoolManager.Domain.Interfaces;
using SchoolManager.Domain.Model;
using Xunit;

namespace SchoolManager.Test.Services
{
    public class ChildServiceTests
    {
        private ChildService CreateService(Mock<IChildRepository> repo)
        {
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()).CreateMapper();
            return new ChildService(repo.Object, new Mock<IGroupRepository>().Object, mapper);
        }

        [Fact]
        public void DeleteChild_CallsRepository()
        {
            var repo = new Mock<IChildRepository>();
            var service = CreateService(repo);

            service.DeleteChild(3);

            repo.Verify(r => r.DeleteChild(3), Times.Once);
        }

        [Fact]
        public void GetChild_ReturnsChild()
        {
            var repo = new Mock<IChildRepository>();
            var child = new Child { Id = 10 };
            repo.Setup(r => r.GetChildById(10)).Returns(child);
            var service = CreateService(repo);

            var result = service.GetChild(10);

            Assert.Same(child, result);
        }

        [Fact]
        public void GetAllChildrenForListByGroupId_ReturnsMappedList()
        {
            var repo = new Mock<IChildRepository>();
            var group = new Group { Id = 1, GroupName = "A", TeacherId = 2, Teacher = new Teacher { Id = 2, FirstName = "T", LastName = "L" } };
            var children = new List<Child>
            {
                new Child { Id = 1, FirstName="c", LastName="d", GroupId = 1, Group = group }
            }.AsQueryable();
            repo.Setup(r => r.GetChildrenByGroupId(1)).Returns(children);

            var service = CreateService(repo);

            var result = service.GetAllChildrenForListByGroupId(1);

            Assert.Single(result.Children);
            Assert.Equal("A", result.Children[0].GroupName);
        }
    }
}