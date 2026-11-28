using GymManagementSystem.ViewModels;
using GymManagementSystem.ViewModels.Plan;

namespace GymManagementSystem.Services.Interfaces
{
    public interface IPlanService
    {
        Task<IEnumerable<PlanViewModel>> GetAllPlansSync(CancellationToken ct = default);
        Task<UpdatedPlanViewModel?> GetPlanToUpdateAsync(int id, CancellationToken ct = default);
        Task<bool> UpdatePlanAsync(int id, UpdatedPlanViewModel updatedPlan, CancellationToken ct = default);
        Task<bool> ToggleActivationAsync(int id, CancellationToken ct = default);
    }
}
