namespace GymManagementSystem.Entities
{
    public class Member : GymUser
    {
        public string? PhotoUrl { get; set; }

        public ICollection<Membership> Memberships { get; set; } = default!;
    }
}
