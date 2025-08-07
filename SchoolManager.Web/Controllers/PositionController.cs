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
        public async Task<IActionResult> AddPosition()
        {
            var model = new AddPositionViewModel
            {
                Positions = await _positionService.GetAllAsync()
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> AddPosition(NewPositionVm model)
        {
            if (model == null)
            {
                return BadRequest("NewPositionVm nie może być null");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _positionService.AddPositionAsync(model);
            return RedirectToAction(nameof(AddPosition));
        }
    }
}