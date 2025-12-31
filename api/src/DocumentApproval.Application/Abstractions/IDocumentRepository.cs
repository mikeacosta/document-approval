using DocumentApproval.Domain.Entities;

namespace DocumentApproval.Application.Abstractions;

public interface IDocumentRepository
{
    /// <summary>
    /// Gets a document by ID, including all ApprovalSteps.
    /// Returns null if not found.
    /// </summary>
    Task<Document?> GetByIdAsync(Guid documentId, CancellationToken cancellationToken);

    /// <summary>
    /// Adds a new document aggregate.
    /// </summary>
    Task SaveAsync(Document document, CancellationToken cancellationToken);    
}