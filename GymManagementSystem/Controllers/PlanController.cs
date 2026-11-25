using GymManagementSystem.Entities;
using GymManagementSystem.Repositories.Classes;
using GymManagementSystem.Repositories.Interfaces;
using GymManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.Controllers
{
    public class PlanController : Controller
    {
        private readonly IPlanRepository _planRepo;
        private readonly IMembershipRepository _membershipRepo;

        public PlanController(IPlanRepository planRepo, IMembershipRepository membershipRepo)
        {
            _planRepo = planRepo;
            _membershipRepo = membershipRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var plans = await _planRepo.GetAllAsync(ct);
            var plansViewModel = plans.Select(P => new PlanViewModel()
            {
                Id = P.Id,
                Name = P.Name,
                Description = P.Description,
                DurationDays = P.DurationDays,
                Price = P.Price,
                IsActive = P.IsActive
            });
            return View(plansViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit (int id, CancellationToken ct)
        {
            var plan = await _planRepo.GetByIdAsync(id, ct);
            if(plan is null || !plan.IsActive)
            {
                TempData["ErrorMessage"] = "This Plan can't be edited!";
                return RedirectToAction(nameof(Index));
            }

            var hasCurrentMemberships = await _membershipRepo.AnyAsync(M => M.PlanId == id && M.EndDate > DateTime.Now, ct);
            if(hasCurrentMemberships)
            {
                TempData["ErrorMessage"] = "This Plan can't be edited!";
                return RedirectToAction(nameof(Index));
            }
            var viewModel = new UpdatedPlanViewModel()
            {
                PlanName = plan.Name,
                Description = plan.Description,
                DurationDays = plan.DurationDays,
                Price = plan.Price
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int id, UpdatedPlanViewModel updatedPlan, CancellationToken ct)
        {
            if(!ModelState.IsValid)
            {
                return View(updatedPlan);
            }

            var plan = await _planRepo.GetByIdAsync(id, ct);
            if (plan is null || !plan.IsActive)
            {
                TempData["ErrorMessage"] = "This Plan can't be edited!";
                return RedirectToAction(nameof(Index));
            }

            var hasCurrentMemberships = await _membershipRepo.AnyAsync(M => M.PlanId == id && M.EndDate > DateTime.Now, ct);
            if (hasCurrentMemberships)
            {
                TempData["ErrorMessage"] = "This Plan can't be edited!";
                return RedirectToAction(nameof(Index));
            }
            plan.Description = updatedPlan.Description;
            plan.DurationDays = updatedPlan.DurationDays;
            plan.Price = updatedPlan.Price;
            plan.UpdatedAt = DateTime.Now;

            var result = await _planRepo.UpdateAsync(ct);
            if(result > 0)
            {
                TempData["SuccessMessage"] = "Plan Updated Successfully.";
                return RedirectToAction(nameof(Index));
            }else
            {
                ModelState.AddModelError(string.Empty,"Failed To Update Plan!");
                return View(updatedPlan);
            }
        }
    }
}
