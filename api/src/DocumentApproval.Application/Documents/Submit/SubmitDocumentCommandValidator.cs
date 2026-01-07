using FluentValidation;

namespace DocumentApproval.Application.Documents.Submit;

public sealed class SubmitDocumentCommandValidator : AbstractValidator<SubmitDocumentCommand>
{
    public SubmitDocumentCommandValidator()
    {
        RuleFor(x => x.DocumentId)
            .NotEmpty()
            .WithMessage("DocumentId is required.");
    }
}