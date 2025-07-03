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
        public async Task<IActionResult> Index()
        {
            var model = await _childService.GetAllChildrenForListAsync();
            return View(model);
        }

    }
}
