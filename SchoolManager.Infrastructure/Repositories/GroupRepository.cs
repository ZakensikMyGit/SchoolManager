using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
        public Task <List<Group>> GetAllGroupsAsync()
        {
            return _context.Groups
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<Group> GetGroupAsync(int id)
        {
            var group = await _context.Groups
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id == id);

            if (group == null)
                throw new InvalidOperationException($"Nie znaleziono id grupy: {id}.");

            return group;
        }

        public async Task UpdateGroupAsync(Group group)
        {
            _context.Groups.Update(group);
            await _context.SaveChangesAsync();
        }
    }
}
