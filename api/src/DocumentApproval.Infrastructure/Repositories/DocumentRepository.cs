using DocumentApproval.Application.Abstractions;
using DocumentApproval.Domain.Entities;
using DocumentApproval.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DocumentApproval.Infrastructure.Repositories;

public sealed class DocumentRepository : IDocumentRepository
{
    private readonly AppDbContext _dbContext;

    public DocumentRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Document?> GetByIdAsync(Guid documentId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Documents
                   .Include(d => d.ApprovalSteps)
                   .FirstOrDefaultAsync(d => d.Id == documentId, cancellationToken)
               ?? throw new KeyNotFoundException($"Document {documentId} not found.");
    }

    public async Task SaveAsync(Document document, CancellationToken cancellationToken = default)
    {
        // EF Core tracks changes automatically
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
