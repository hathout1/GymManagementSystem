using GymManagementSystem.Contexts;
using GymManagementSystem.Entities;
using GymManagementSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GymManagementSystem.Repositories.Classes
{
    public class MemberRepository : IMemberRepository
    {
        private readonly GymDbContext _dbContext;

        public MemberRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddAsync(Member member, CancellationToken ct = default)
        {
            _dbContext.Members.Add(member);
            return await _dbContext.SaveChangesAsync(ct);
        }

        public async Task<bool> AnyAsync(Expression<Func<Member, bool>> predicate, CancellationToken ct = default)
        {
            return await _dbContext.Members.AnyAsync(predicate, ct);
        }

        public async Task<IEnumerable<Member>> GetAllAsync(CancellationToken ct = default)
        {
            return await _dbContext.Members.AsNoTracking().ToListAsync(ct);
        }

        public async Task<Member?> GetMemberWithMembershipAsync(int id, CancellationToken ct = default)
        {
            return await _dbContext.Members.AsNoTracking()
                                           .Include(M => M.Memberships
                                                          .Where(MS => MS.EndDate > DateTime.Now)
                                                          .OrderBy(MS => MS.StartDate)
                                                          .Take(1))
                                           .ThenInclude(MS => MS.Plan)
                                           .FirstOrDefaultAsync(M => M.Id == id, ct);
        }
    }
}
