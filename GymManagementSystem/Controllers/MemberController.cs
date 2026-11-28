using GymManagementSystem.Services.Interfaces;
using GymManagementSystem.ViewModels.Member;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var result = await _memberService.GetAllMembersAsync(ct);
            return View(result);
        }
        public async Task<IActionResult> Details(int id, CancellationToken ct)
        {
            var result = await _memberService.GetMemberDetailsAsync(id, ct);
            if(result is null)
            {
                TempData["ErrorMessage"] = "Member Not Found!";
                return RedirectToAction(nameof(Index));
            }
            return View(result);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateMemberViewModel model, CancellationToken ct)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _memberService.CreateMemberAsync(model, ct);

            if (!result)
            {
                ModelState.AddModelError("", "Email address or Phone number is already Exist, Or Member is not old enough!");
                return View(model);
            }

            TempData["SuccessMessage"] = "Member Created Successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
