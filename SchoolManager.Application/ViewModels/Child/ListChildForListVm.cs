using Microsoft.AspNetCore.Razor.TagHelpers;
using SchoolManager.Application.ViewModels.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Application.ViewModels.Child
{
    public class ListChildForListVm
    {
        public List<ChildForListVm> Children { get; set; }
        public int Count { get; set; }
    }
}
