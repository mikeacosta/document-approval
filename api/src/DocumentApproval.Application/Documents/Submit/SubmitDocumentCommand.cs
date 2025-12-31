namespace DocumentApproval.Application.Documents.Submit;

/// <summary>
/// Represents a request to submit a document for approval.
/// </summary>
public sealed record SubmitDocumentCommand(
    Guid DocumentId
);