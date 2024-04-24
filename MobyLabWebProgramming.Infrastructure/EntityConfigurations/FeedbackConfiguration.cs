using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

/// <summary>
/// This is the entity configuration for the UserFile entity.
/// </summary>
public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
{
    public void Configure(EntityTypeBuilder<Feedback> builder)
    {
        builder.Property(e => e.Id)
            .IsRequired();
        builder.HasKey(x => x.Id);
        builder.Property(e => e.Stars)
            .IsRequired();
        builder.Property(e => e.Description)
            .HasMaxLength(4095)
            .IsRequired();
        builder.Property(e => e.CreatedAt)
            .IsRequired();
        builder.Property(e => e.UpdatedAt)
            .IsRequired();

        builder.HasOne(e => e.FromUser)
            .WithMany(e => e.OfferedFeedbacks)
            .HasForeignKey(e => e.FromUserId)
            .HasPrincipalKey(e => e.Id)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.ToUser)
            .WithMany(e => e.ReceivedFeedbacks)
            .HasForeignKey(e => e.ToUserId)
            .HasPrincipalKey(e => e.Id)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
