using GymManagementSystem.Entities;
using System.Linq.Expressions;

namespace GymManagementSystem.Repositories.Interfaces
{
    public interface IMembershipRepository
    {
        Task<bool> AnyAsync(Expression<Func<Membership, bool>> predicate, CancellationToken ct);
    }
}
