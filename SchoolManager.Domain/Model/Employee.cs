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
        public static int MaxWorkingHours = 40;
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? GroupId { get; set; }
        public double WorkingHours { get; set; } = 1;
        // public DateTime DateOfBirth { get; set; }
        //public string PhoneNumber { get; set; }
        //public string Email { get; set; }
        public DateTime EmploymentDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; }
        public int? PositionId { get; set; }

        public virtual Position Position { get; set; }
        public virtual ICollection<Education> Educations { get; set; }
        public string FullName => $"{FirstName} {LastName}";
    }
}
