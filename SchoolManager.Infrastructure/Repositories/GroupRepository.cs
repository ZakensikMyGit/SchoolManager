using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManager.Domain.Model;

namespace SchoolManager.Infrastructure.Repositories
{
    public class GroupRepository
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
    }
}
