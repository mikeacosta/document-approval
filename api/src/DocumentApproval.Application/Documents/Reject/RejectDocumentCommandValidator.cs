using FluentValidation;

namespace DocumentApproval.Application.Documents.Reject;

public sealed class RejectDocumentCommandValidator : AbstractValidator<RejectDocumentCommand>
{
    public RejectDocumentCommandValidator()
    {
        RuleFor(x => x.DocumentId)
            .NotEmpty();

        RuleFor(x => x.ApproverUserId)
            .NotEmpty();
    }    
}