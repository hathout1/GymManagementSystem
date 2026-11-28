using GymManagementSystem.Entities;
using System.Linq.Expressions;

namespace GymManagementSystem.Repositories.Interfaces
{
    public interface IMemberRepository
    {
        Task<IEnumerable<Member>> GetAllAsync(CancellationToken ct = default);
        Task<Member?> GetMemberWithMembershipAsync(int id, CancellationToken ct = default);
        Task<bool> AnyAsync(Expression<Func<Member, bool>> predicate, CancellationToken ct = default);
        Task<int> AddAsync(Member member, CancellationToken ct = default);
    }
}
