using DocumentApproval.Application.Abstractions;
using DocumentApproval.Application.Common.Errors;

namespace DocumentApproval.Application.Documents.Reject;

public sealed class RejectDocumentCommandHandler : ICommandHandler<RejectDocumentCommand>
{
    private readonly IDocumentRepository _documentRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public RejectDocumentCommandHandler(
        IDocumentRepository documents,
        IUnitOfWork unitOfWork)
    {
        _documentRepository = documents;
        _unitOfWork = unitOfWork;
    }
    
    public async Task HandleAsync(
        RejectDocumentCommand command,
        CancellationToken cancellationToken = default)
    {
        // 1️⃣ Load aggregate
        var document = await _documentRepository.GetByIdAsync(
            command.DocumentId,
            cancellationToken);
        
        if (document is null)
        {
            throw new NotFoundException(
                $"Document '{command.DocumentId}' was not found.");
        }

        // 2️⃣ Invoke domain behavior
        document.Reject(command.ApproverUserId);

        // 3️⃣ Commit transaction
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}