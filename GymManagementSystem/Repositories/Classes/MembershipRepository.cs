using GymManagementSystem.Contexts;
using GymManagementSystem.Entities;
using GymManagementSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GymManagementSystem.Repositories.Classes
{
    public class MembershipRepository : IMembershipRepository
    {
        private readonly GymDbContext _dbContext;

        public MembershipRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> AnyAsync(Expression<Func<Membership, bool>> predicate, CancellationToken ct)
        {
            return await _dbContext.Memberships.AnyAsync(predicate, ct);
        }
    }
}
