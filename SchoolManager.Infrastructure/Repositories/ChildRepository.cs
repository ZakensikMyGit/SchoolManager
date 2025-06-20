using Microsoft.EntityFrameworkCore;
using SchoolManager.Domain.Interfaces;
using SchoolManager.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Infrastructure.Repositories
{
    public class ChildRepository : IChildRepository
    {
        private readonly Context _context;
        public ChildRepository(Context context)
        {
            _context = context;
        }
        public IQueryable<Child> GetAllChildren()
        {
            return _context.Children;
        }
        public int AddChild(Child child)
        {
            _context.Children.Add(child);
            _context.SaveChanges();
            return child.Id;
        }

        public void DeleteChild(int childId)
        {
            var entity = _context.Children.FirstOrDefault(c => c.Id == childId);
            if (entity != null)
            {
                _context.Children.Remove(entity);
                _context.SaveChanges();
            }
        }

        public Child GetChildById(int childId)
        {
            return _context.Children
                .Include(c => c.Group)
                .ThenInclude(g => g.Teacher)
                .Include(c => c.Declarations)
                .FirstOrDefault(c => c.Id == childId);
        }

        public IQueryable<Child> GetChildrenByGroupId(int groupId)
        {
            return _context.Children
               .Where(c => c.GroupId == groupId)
               .Include(c => c.Group)
               .ThenInclude(g => g.Teacher);
        }

        public void UpdateChild(Child child)
        {
            _context.Children.Update(child);
            _context.SaveChanges();
        }
    }
}
