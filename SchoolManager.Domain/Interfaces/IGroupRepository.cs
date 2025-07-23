using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManager.Domain.Model;

namespace SchoolManager.Domain.Interfaces
{
    public interface IGroupRepository
    {
        public Task<List<Group>> GetAllGroupsAsync();
        public Task<Group> GetGroupAsync(int groupId);
        public Task UpdateGroupAsync(Group group);
    }
}
