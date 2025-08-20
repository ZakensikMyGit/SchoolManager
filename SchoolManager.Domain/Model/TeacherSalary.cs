using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Domain.Model
{
    public class TeacherSalary
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }
        public virtual ICollection<MotivationalAllowance>? AllowancesHistory { get; set; }
    }
}
