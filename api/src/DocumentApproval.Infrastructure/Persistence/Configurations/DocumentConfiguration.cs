using DocumentApproval.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocumentApproval.Infrastructure.Persistence.Configurations;

internal sealed class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.ToTable("Documents");
        
        builder.HasKey(d => d.Id);
        
        builder.Property(d => d.Status)
            .HasConversion<string>()
            .IsRequired();
        
        builder.Property(d => d.RowVersion)
            .IsRowVersion();
        
        builder.HasMany(d => d.ApprovalSteps)
            .WithOne()
            .HasForeignKey("DocumentId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(d => d.ApprovalSteps)
            .UsePropertyAccessMode(PropertyAccessMode.Field);   
    }
}