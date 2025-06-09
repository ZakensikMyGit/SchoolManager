﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Domain.Model
{
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
