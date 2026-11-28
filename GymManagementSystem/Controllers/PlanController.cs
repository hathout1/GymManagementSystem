using GymManagementSystem.Services.Interfaces;
using GymManagementSystem.ViewModels.Plan;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.Controllers
{
    public class PlanController : Controller
    {
        private readonly IPlanService _planService;

        public PlanController(IPlanService planService)
        {
            _planService = planService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var plans = await _planService.GetAllPlansSync(ct);
            return View(plans);
        }

        [HttpGet]
        public async Task<IActionResult> Edit (int id, CancellationToken ct)
        {
            var result = await _planService.GetPlanToUpdateAsync(id, ct);
            if(result is null)
            {
                TempData["ErrorMessage"] = "This Plan can't be edited!";
                return RedirectToAction(nameof(Index));
            }
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int id, UpdatedPlanViewModel updatedPlan, CancellationToken ct)
        {
            if(!ModelState.IsValid)
            {
                return View(updatedPlan);
            }

            var result = await _planService.UpdatePlanAsync(id,updatedPlan ,ct);
            if (!result)
            {
                ModelState.AddModelError(string.Empty, "Failed To Update Plan!");
                return View(updatedPlan);
            }

            TempData["SuccessMessage"] = "Plan Updated Successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> ToggleStatus(int id, CancellationToken ct)
        {
            var result = await _planService.ToggleActivationAsync(id,ct);
            if(result)
                TempData["SuccessMessage"] = "Plan Status Changed Successfully.";
            else
                TempData["ErrorMessage"] = "This Plan Status can't be Changed!";

            return RedirectToAction(nameof(Index));
        }
    }
}
