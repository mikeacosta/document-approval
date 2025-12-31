using DocumentApproval.Application.Abstractions;
using DocumentApproval.Application.Common.Errors;

namespace DocumentApproval.Application.Documents.Submit;

public class SubmitDocumentCommandHandler
{
    private readonly IDocumentRepository _documentRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public SubmitDocumentCommandHandler(IDocumentRepository documentRepository, IUnitOfWork unitOfWork)
    {
        _documentRepository = documentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task HandleAsync(SubmitDocumentCommand command, CancellationToken cancellationToken)
    {
        // 1. Load aggregate
        var document = await _documentRepository.GetByIdAsync(
            command.DocumentId,
            cancellationToken);     
        
        // 2. Not found is an application concern
        if (document is null)
        {
            throw new NotFoundException(
                $"Document '{command.DocumentId}' was not found.");
        }
        
        // 3. Delegate to domain (invariants enforced here)
        document.Submit();

        // 4. Persist
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}