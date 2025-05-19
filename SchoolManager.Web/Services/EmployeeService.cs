using SchoolManager.Web.Interfaces;
using SchoolManager.Web.Models;

namespace SchoolManager.Web.Services
{
    public class EmployeeService : IEmployeeService
    {
        private List<Teacher> teachers = new List<Teacher>
        {
            new Teacher {Id = 1, Name = "Anna", LastName = "Nowak", Position = "Nauczyciel", Type = "Wychowawca", Education = new List<string>{"Pedagogika wczesnoszkolna"}, IsDirector = false },
            new Teacher {Id = 2, Name = "Katarzyna", LastName = "Kowalska", Position = "Nauczyciel", Type = "Wychowawca", Education = new List<string>{"Pedagogika wczesnoszkolna"}, IsDirector = false },
        };

        private List<Employee> employees = new List<Employee>
        {
            new Employee {Id = 3, Name = "Tomasz", LastName = "Guzik", Position = "Konserwator"},
            new Employee {Id = 4, Name = "Barbara", LastName = "Leśna", Position = "Pomoc nauczyciela"},
        };
        public List<Employee> GetAllEmployees()
        {
            var all = new List<Employee>();
            //all.AddRange(teachers);
            all.AddRange(employees);
            return all;
        }

        public List<Teacher> GetAllTeachers()
        {
            var list = new List<Teacher>(teachers);
            return list;
        }

        public Employee GetEmployeeById(int id)
        {
            return GetAllEmployees().FirstOrDefault(x => x.Id == id);
        }

        public Teacher GetTeacherById(int id)
        {
            return GetAllTeachers().FirstOrDefault(x => x.Id == id); ;
        }
    }
}
