using AutoMapper;
using GymManagementSystem.Entities;
using GymManagementSystem.Repositories.Interfaces;
using GymManagementSystem.Services.Interfaces;
using GymManagementSystem.ViewModels;
using GymManagementSystem.ViewModels.Member;

namespace GymManagementSystem.Services.Classes
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepo;
        private readonly IMapper _mapper;

        public MemberService(IMemberRepository memberRepo, IMapper mapper)
        {
            _memberRepo = memberRepo;
            _mapper = mapper;
        }

        public async Task<bool> CreateMemberAsync(CreateMemberViewModel model, CancellationToken ct = default)
        {
            var isOldEnough = model.DateOfBirth <= DateOnly.FromDateTime(DateTime.Now).AddYears(-16);
            if (!isOldEnough)
                return false;

            var isPhoneExists = await _memberRepo.AnyAsync(M => M.Phone == model.Phone, ct);
            if (isPhoneExists)
                return false;

            var isEmailExists = await _memberRepo.AnyAsync(M => M.Email == model.Email, ct);
            if (isEmailExists)
                return false;

            var member = _mapper.Map<Member>(model);

            var result = await _memberRepo.AddAsync(member, ct);

            return result > 0;
        }

        public async Task<IEnumerable<MemberViewModel>> GetAllMembersAsync(CancellationToken ct)
        {
            var members = await _memberRepo.GetAllAsync(ct);
            return _mapper.Map<IEnumerable<Member>, IEnumerable<MemberViewModel>>(members);
        }

        public async Task<MemberDetailsViewModel?> GetMemberDetailsAsync(int id, CancellationToken ct = default)
        {
            var member = await _memberRepo.GetMemberWithMembershipAsync(id, ct);
            if (member is null)
                return null;

            return _mapper.Map<MemberDetailsViewModel>(member);
        }
    }
}
