using GymManagementSystem.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagementSystem.Configurations
{
    public class MembershipConfigurations : IEntityTypeConfiguration<Membership>
    {
        public void Configure(EntityTypeBuilder<Membership> builder)
        {
            builder.Property(M => M.CreatedAt)
                   .HasDefaultValueSql("GetDate()");

            builder.HasOne(M => M.Plan)
                   .WithMany(P => P.Memberships)
                   .HasForeignKey(M => M.PlanId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
