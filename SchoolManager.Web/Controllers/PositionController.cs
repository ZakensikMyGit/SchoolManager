using Microsoft.AspNetCore.Mvc;
using SchoolManager.Application.Interfaces;
using SchoolManager.Application.ViewModels.Position;

namespace SchoolManager.Web.Controllers
{
    public class PositionController : Controller
    {
        private readonly IPositionService _positionService;

        public PositionController(IPositionService positionService)
        {
            _positionService = positionService;
        }

        [HttpGet]
        public IActionResult AddPosition()
        {
            return View(new NewPositionVm());
        }

        [HttpPost]
        public async Task<IActionResult> AddPosition(NewPositionVm model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _positionService.AddPositionAsync(model);
            return RedirectToAction(nameof(AddPosition));
        }
    }
}