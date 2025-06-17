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
            throw new NotImplementedException();
        }

        public void DeleteChild(int childId)
        {
            throw new NotImplementedException();
        }


        public Child GetChildById(int childId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Child> GetChildrenByGroupId(int groupId)
        {
            throw new NotImplementedException();
        }

        public void UpdateChild(Child child)
        {
            throw new NotImplementedException();
        }
    }
}
