using Microsoft.AspNetCore.Mvc;
using SchoolManager.Web.Services;

namespace SchoolManager.Web.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            //widok dla tej akcji
            // tabela z pracownikami, przyciski do dodawania, edycji i usuwania pracowników
            //filtrowanie pracowników po stanowisku
            //przygotowanie danych do widoku
            //przekazać filtrów do serwisu
            //serwis musi przygotować dane do widoku
            // serwis musi zwrócić dane do controlera w odpowienim formacie
            // kontroler musi przekazać dane do widoku i serwisu w odpowiednim formacie

            var model = employeeService.GetAllEmployeesForList(); // Przykładowe wywołanie serwisu, aby pobrać pracowników o danym stanowisku
            return View(model);
        }

        [HttpGet] //gdy przyjcie żądania GET do tej akcji, zwraca widok (pusty formularz)
        public IActionResult AddEmployee()
        {
            return View();
        }

        [HttpPost] // gdy przyjcie żądania POST do tej akcji, wykonuje się logika dodawania pracownika. 
        public IActionResult AddEmployee(EmployeeModel model)
        {
            var id = employeeService.AddEmployee(model); // Przykładowe wywołanie serwisu, aby dodać pracownika
            return View();
        }

        [HttpGet]
        public IActionResult AddNewGroupForTeacher(int teacherIid)
        {

            return View();
        }
        [HttpPost]
        public IActionResult AddNewGroupForTeacher(GroupModel model)
        {

            return View();
        }

        public IActionResult ViewEmployee(int employeeId)
        {
            var employeeModel = employeeService.GetEmployeeDetails(employeeId); // Przykładowe wywołanie serwisu, aby pobrać pracownika o danym ID
            return View(employeeModel);

        }
    }
}
