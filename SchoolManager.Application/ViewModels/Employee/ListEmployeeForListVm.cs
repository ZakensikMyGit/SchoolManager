using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Application.ViewModels.Employee
{
    public class ListEmployeeForListVm
    {
        public List<EmployeeForListVm> Employees { get; set; }
        public int Count { get; set; }
    }
}
