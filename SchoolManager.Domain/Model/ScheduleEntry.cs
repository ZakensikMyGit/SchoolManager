using SchoolManager.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Domain.Model
{
    public class ScheduleEntry
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int PositionId { get; set; }
        public int GroupId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public ScheduleEntryTypeEnum EntryType { get; set; }
        public string Description { get; set; }//NOTATKI

        public Employee Employee { get; set; }
        public Position Position { get; set; }
        public Group Group { get; set; }
    }
}
