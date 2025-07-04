﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManager.Domain.Interfaces;
using SchoolManager.Domain.Model;

namespace SchoolManager.Infrastructure.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly Context _context;
        public GroupRepository(Context context)
        {
            _context = context;
        }
        public List<Group> GetAllGroups()
        {
            return _context.Groups.ToList();
        }
        public Group GetGroup(int id)
        {
            return _context.Groups.FirstOrDefault(g => g.Id == id);
        }

        public void UpdateGroup(Group group)
        {
            _context.Groups.Update(group);
            _context.SaveChanges();
        }
    }
}
