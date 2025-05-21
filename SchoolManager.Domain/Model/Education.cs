using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Domain.Model
{
    public class Education
    {
        public int EducationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; } // np. "Studia", "Kurs", "Szkolenie"
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
