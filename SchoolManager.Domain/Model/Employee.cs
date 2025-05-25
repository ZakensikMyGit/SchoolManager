using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Domain.Model
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public double WorkingHours { get; set; }
        public DateTime EmploymentDate { get; set; }
        public bool IsActive { get; set; }
        public int PositionId { get; set; }

        public virtual Position Position { get; set; }
        public virtual ICollection<Education> Educations { get; set; }

    }
}
