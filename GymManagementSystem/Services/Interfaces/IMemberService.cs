using GymManagementSystem.ViewModels.Member;

namespace GymManagementSystem.Services.Interfaces
{
    public interface IMemberService
    {
        Task<IEnumerable<MemberViewModel>> GetAllMembersAsync(CancellationToken ct = default);
        Task<MemberDetailsViewModel?> GetMemberDetailsAsync(int id, CancellationToken ct = default);
        Task<bool> CreateMemberAsync(CreateMemberViewModel model, CancellationToken ct = default);
    }
}
