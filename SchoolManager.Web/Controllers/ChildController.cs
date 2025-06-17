using Microsoft.AspNetCore.Mvc;
using SchoolManager.Application.Interfaces;
using SchoolManager.Domain.Interfaces;

namespace SchoolManager.Web.Controllers
{
    public class ChildController : Controller
    {
        private readonly IChildService _childService;
        private readonly IGroupRepository _groupRepository;
        public ChildController(
            IChildService childService,
            IGroupRepository groupRepository)
        {
            _childService = childService;
            _groupRepository = groupRepository;
        }
        public IActionResult Index()
        {
            var model = _childService.GetAllChildrenForList();
            return View(model);
        }

    }
}
