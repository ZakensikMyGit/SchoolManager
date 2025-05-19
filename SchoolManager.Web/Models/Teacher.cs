using System.ComponentModel;
using System.Runtime.Serialization;

namespace SchoolManager.Web.Models
{
    public class Teacher : Employee
    {
        [DisplayName("Typ nauczyciela")]
        public string Type { get; set; } = string.Empty;
        [DisplayName("Wykształcenie")]
        public List<string> Education { get; set; } = new List<string>();
        [IgnoreDataMember]
        public bool IsDirector { get; set; } = false;

    }
}
