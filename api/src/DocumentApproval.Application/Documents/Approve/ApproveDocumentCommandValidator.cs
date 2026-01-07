using FluentValidation;

namespace DocumentApproval.Application.Documents.Approve;

public sealed class ApproveDocumentCommandValidator : AbstractValidator<ApproveDocumentCommand>
{
    public ApproveDocumentCommandValidator()
    {
        RuleFor(x => x.DocumentId)
            .NotEmpty()
            .WithMessage("DocumentId is required.");

        RuleFor(x => x.ApproverUserId)
            .NotEmpty()
            .WithMessage("ApproverUserId is required.");
    }    
}