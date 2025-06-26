using AutoMapper;
using AutoMapper.QueryableExtensions;
using SchoolManager.Application.Interfaces;
using SchoolManager.Application.ViewModels.Child;
using SchoolManager.Domain.Interfaces;
using SchoolManager.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Application.Services
{
    public class ChildService : IChildService
    {
        private readonly IChildRepository _childRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;
        public ChildService(IChildRepository childRepository, IGroupRepository groupRepository, IMapper mapper)
        {
            _childRepository = childRepository;
            _groupRepository = groupRepository;
            _mapper = mapper;
        }
        public void DeleteChild(int childId)
        {
           _childRepository.DeleteChild(childId);
        }
        public Task DeleteChildAsync(int childId)
        {
            return _childRepository.DeleteChildAsync(childId);
        }

        public ListChildForListVm GetAllChildrenForList()
        {
            var children = _childRepository.GetAllChildren()
                .ProjectTo<ChildForListVm>(_mapper.ConfigurationProvider)
                .ToList();

            var childList = new ListChildForListVm
            {
                Children = children,
                Count = children.Count
            };
            return childList;
        }
        public async Task<ListChildForListVm> GetAllChildrenForListAsync()
        {
            var children = await _childRepository.GetAllChildrenAsync();
            var childVms = children.AsQueryable()
                .ProjectTo<ChildForListVm>(_mapper.ConfigurationProvider)
                .ToList();

            return new ListChildForListVm
            {
                Children = childVms,
                Count = childVms.Count
            };
        }
        public ListChildForListVm GetAllChildrenForListByGroupId(int groupId)
        {
            var children = _childRepository.GetChildrenByGroupId(groupId)
                .ProjectTo<ChildForListVm>(_mapper.ConfigurationProvider)
                .ToList();

            return new ListChildForListVm
            {
                Children = children,
                Count = children.Count
            };
        }
        public async Task<ListChildForListVm> GetAllChildrenForListByGroupIdAsync(int groupId)
        {
            var children = await _childRepository.GetChildrenByGroupIdAsync(groupId);
            var childVms = children.AsQueryable()
                .ProjectTo<ChildForListVm>(_mapper.ConfigurationProvider)
                .ToList();

            return new ListChildForListVm
            {
                Children = childVms,
                Count = childVms.Count
            };
        }
        public Child GetChild(int ChildId)
        {
            return _childRepository.GetChildById(ChildId);
        }
        public Task<Child?> GetChildAsync(int childId)
        {
            return _childRepository.GetChildByIdAsync(childId);
        }
    }
}
