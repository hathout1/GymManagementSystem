using GymManagementSystem.Repositories.Interfaces;
using GymManagementSystem.Services.Interfaces;
using GymManagementSystem.ViewModels;
using GymManagementSystem.ViewModels.Plan;

namespace GymManagementSystem.Services.Classes
{
    public class PlanService : IPlanService
    {
        private readonly IPlanRepository _planRepo;
        private readonly IMembershipRepository _membershipRepo;

        public PlanService(IPlanRepository planRepo, IMembershipRepository membershipRepo)
        {
            _planRepo = planRepo;
            _membershipRepo = membershipRepo;
        }
        public async Task<IEnumerable<PlanViewModel>> GetAllPlansSync(CancellationToken ct = default)
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
            return plansViewModel;
        }

        public async Task<UpdatedPlanViewModel?> GetPlanToUpdateAsync(int id, CancellationToken ct = default)
        {
            var plan = await _planRepo.GetByIdAsync(id, ct);
            if (plan is null || !plan.IsActive)
                return null;

            var hasCurrentMemberships = await _membershipRepo.AnyAsync(M => M.PlanId == id && M.EndDate > DateTime.Now, ct);
            if (hasCurrentMemberships)
                return null;

            var viewModel = new UpdatedPlanViewModel()
            {
                PlanName = plan.Name,
                Description = plan.Description,
                DurationDays = plan.DurationDays,
                Price = plan.Price
            };
            return viewModel;
        }

        public async Task<bool> ToggleActivationAsync(int id, CancellationToken ct = default)
        {
            var plan = await _planRepo.GetByIdAsync(id, ct);
            if (plan is null)
                return false;

            var hasCurrentMemberships = await _membershipRepo.AnyAsync(M => M.PlanId == id && M.EndDate > DateTime.Now, ct);
            if (hasCurrentMemberships && plan.IsActive)
                return false;

            plan.IsActive = !plan.IsActive;
            plan.UpdatedAt = DateTime.Now;
            return await _planRepo.UpdateAsync(ct) > 0;

        }

        public async Task<bool> UpdatePlanAsync(int id, UpdatedPlanViewModel updatedPlan, CancellationToken ct = default)
        {
            var plan = await _planRepo.GetByIdAsync(id, ct);
            if (plan is null || !plan.IsActive)
                return false;

            var hasCurrentMemberships = await _membershipRepo.AnyAsync(M => M.PlanId == id && M.EndDate > DateTime.Now, ct);
            if (hasCurrentMemberships)
                return false;

            plan.Description = updatedPlan.Description;
            plan.DurationDays = updatedPlan.DurationDays;
            plan.Price = updatedPlan.Price;
            plan.UpdatedAt = DateTime.Now;

            var result = await _planRepo.UpdateAsync(ct);
            return result > 0;
        }
    }
}
