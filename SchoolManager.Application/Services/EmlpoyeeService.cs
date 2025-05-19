using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManager.Application.Interfaces;

namespace SchoolManager.Application.Services
{
    public class EmlpoyeeService : IEmployeeService
    {
        public List<int> GetAllEmployees()
        {
            return new List<int> { 1, 2, 3 };
        }
    }
}
