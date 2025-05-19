using System.ComponentModel;

namespace SchoolManager.Web.Models
{
    public class Employee
    {
        [DisplayName("Identyfiktor")]
        public int Id { get; set; }
        [DisplayName("Imię")]
        public string Name { get; set; } = string.Empty;
        [DisplayName("Nazwisko")]
        public string LastName { get; set; } = string.Empty;
        [DisplayName("Zatrudniony")]
        public bool IsActive { get; set; } = true;
    }
}
