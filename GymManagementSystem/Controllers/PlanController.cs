using GymManagementSystem.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Controllers
{
    public class PlanController : Controller
    {
        private readonly GymDbContext dbContext = new GymDbContext();

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var plans = await dbContext.Plans.ToListAsync();
            return View(plans);
        }
    }
}
