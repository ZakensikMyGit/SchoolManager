using System.ComponentModel;
using System.Runtime.Serialization;

namespace SchoolManager.Web.Models
{
    public class PreschoolGroup
    {
        [DisplayName("Id")]
        public int Id { get; set; }
        [DisplayName("Grupa")]
        public string GroupName { get; set; } = string.Empty;
        [DisplayName("Ilość dzieci")]
        public int CurrentChildrenCount { get; set; } = 0;
        [DisplayName("Wychowawca")]
        public Teacher? Teacher { get; set; } = null;
        [DisplayName("Ilość dzieci maksymalnie")]
        public int MaxChildrenCount { get; set; } = 25;
    }
}
