using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Domain.Model
{
    public class Group
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int TeacherId { get; set; }
        public int CurrentChildrenCount { get; set; } = 0;
        public int MaxChildrenCount { get; set; } = 25;
        public virtual ICollection<Child> Children { get; set; }

        public virtual Teacher Teacher { get; set; }
    }
}
