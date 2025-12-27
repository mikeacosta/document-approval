using DocumentApproval.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocumentApproval.Infrastructure.Persistence.Configurations;

internal sealed class ApprovalStepConfiguration : IEntityTypeConfiguration<ApprovalStep>
{
    public void Configure(EntityTypeBuilder<ApprovalStep> builder)
    {
        builder.ToTable("ApprovalSteps");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.ApproverUserId)
            .IsRequired();

        builder.Property(s => s.Status)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(s => s.StepOrder)
            .IsRequired();
    }
}