namespace GymManagementSystem.ViewModels.Member
{
    public class MemberDetailsViewModel : MemberViewModel
    {
        public string Address { get; set; } = default!;
        public string DateOfBirth { get; set; } = default!;

        public string PlanName { get; set; } = default!;
        public string MembershipStatus { get; set; } = default!;
        public string MembershipStartDate { get; set; } = default!;
        public string MembershipEndDate { get; set; } = default!;
    }
}
