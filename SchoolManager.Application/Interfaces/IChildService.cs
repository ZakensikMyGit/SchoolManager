using SchoolManager.Application.ViewModels.Child;
using SchoolManager.Application.ViewModels.Employee;
using SchoolManager.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Application.Interfaces
{
    public interface IChildService
    {
        ListChildForListVm GetAllChildrenForList();
        Child GetChild(int ChildId);
        void DeleteChild(int childId);
        ListChildForListVm GetAllChildrenForListByGroupId(int groupId);
        //ChildDetailsVm GetChildDetails(int childId);
        //int AddChild(NewChildVm model);
        //NewChildVm GetChildForEdit(int childId);
    }
}
