namespace DocumentApproval.Application.Documents.Approve;

public sealed record ApproveDocumentCommand(
    Guid DocumentId,
    Guid ApproverUserId
);