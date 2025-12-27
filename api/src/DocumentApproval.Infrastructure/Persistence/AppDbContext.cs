using DocumentApproval.Domain.Entities;
using DocumentApproval.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace DocumentApproval.Infrastructure.Persistence;

public sealed class AppDbContext : DbContext
{
    public DbSet<Document> Documents => Set<Document>();
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DocumentConfiguration());
    }
}