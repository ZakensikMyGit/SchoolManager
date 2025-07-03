using SchoolManager.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Domain.Interfaces
{
    public interface IChildRepository
    {
        IQueryable<Child> GetAllChildren();
        Child GetChildById(int childId);
        int AddChild(Child child);
        void UpdateChild(Child child);
        void DeleteChild(int childId);
        IQueryable<Child> GetChildrenByGroupId(int groupId);

        Task<List<Child>> GetAllChildrenAsync();
        Task<Child?> GetChildByIdAsync(int childId);
        Task<int> AddChildAsync(Child child);
        Task UpdateChildAsync(Child child);
        Task DeleteChildAsync(int childId);
        Task<List<Child>> GetChildrenByGroupIdAsync(int groupId);

    }
}
