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
        List<Group> GetAllGroups();
        Group GetGroup(int groupId);
    }
}
