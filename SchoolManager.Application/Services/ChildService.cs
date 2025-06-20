﻿using AutoMapper;
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

        public Child GetChild(int ChildId)
        {
            return _childRepository.GetChildById(ChildId);
        }
    }
}
