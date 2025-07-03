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
        public Task<List<Child>> GetAllChildrenAsync()
        {
            return _context.Children.ToListAsync();
        }
        public int AddChild(Child child)
        {
            _context.Children.Add(child);
            _context.SaveChanges();
            return child.Id;
        }

        public async Task<int> AddChildAsync(Child child)
        {
            await _context.Children.AddAsync(child);
            await _context.SaveChangesAsync();
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

        public async Task DeleteChildAsync(int childId)
        {
            var entity = await _context.Children.FirstOrDefaultAsync(c => c.Id == childId);
            if (entity != null)
            {
                _context.Children.Remove(entity);
                await _context.SaveChangesAsync();
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

        public Task<Child?> GetChildByIdAsync(int childId)
        {
            return _context.Children
                .Include(c => c.Group)
                .ThenInclude(g => g.Teacher)
                .Include(c => c.Declarations)
                .FirstOrDefaultAsync(c => c.Id == childId);
        }
        public IQueryable<Child> GetChildrenByGroupId(int groupId)
        {
            return _context.Children
               .Where(c => c.GroupId == groupId)
               .Include(c => c.Group)
               .ThenInclude(g => g.Teacher);
        }

        public Task<List<Child>> GetChildrenByGroupIdAsync(int groupId)
        {
            return _context.Children
               .Where(c => c.GroupId == groupId)
               .Include(c => c.Group)
               .ThenInclude(g => g.Teacher)
               .ToListAsync();
        }

        public void UpdateChild(Child child)
        {
            _context.Children.Update(child);
            _context.SaveChanges();
        }

        public async Task UpdateChildAsync(Child child)
        {
            _context.Children.Update(child);
            await _context.SaveChangesAsync();
        }

    }
}
