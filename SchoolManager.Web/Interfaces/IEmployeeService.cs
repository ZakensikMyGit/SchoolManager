using SchoolManager.Web.Models;

namespace SchoolManager.Web.Interfaces
{
    public interface IEmployeeService
    {
        List<Employee> GetAllEmployees();
        List<Teacher> GetAllTeachers();
        Employee GetEmployeeById(int id);
        Teacher GetTeacherById(int id);
    }
}
