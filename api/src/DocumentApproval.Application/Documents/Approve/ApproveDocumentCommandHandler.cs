using DocumentApproval.Application.Abstractions;
using DocumentApproval.Application.Common.Errors;

namespace DocumentApproval.Application.Documents.Approve;

public sealed class ApproveDocumentCommandHandler : ICommandHandler<ApproveDocumentCommand>
{
    private readonly IDocumentRepository _documentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ApproveDocumentCommandHandler(
        IDocumentRepository documents,
        IUnitOfWork unitOfWork)
    {
        _documentRepository = documents;
        _unitOfWork = unitOfWork;
    }

    public async Task HandleAsync(
        ApproveDocumentCommand command,
        CancellationToken cancellationToken = default)
    {
        // Load aggregate
        var document = await _documentRepository.GetByIdAsync(
            command.DocumentId,
            cancellationToken);  
        
        if (document is null)
        {
            throw new NotFoundException(
                $"Document '{command.DocumentId}' was not found.");
        }

        // Invoke domain behavior
        document.Approve(command.ApproverUserId);

        // Commit transaction
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}