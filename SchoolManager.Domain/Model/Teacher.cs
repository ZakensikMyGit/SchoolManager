using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Domain.Model
{
    public class Teacher : Employee
    {
        public string TypeTeacher { get; set; }
        public bool IsDirector { get; set; }
        public int PensumHours { get; set; }
        public virtual Group Group { get; set; }

    }
}
