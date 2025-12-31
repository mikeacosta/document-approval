namespace DocumentApproval.Application.Documents.Reject;

public sealed record RejectDocumentCommand(
    Guid DocumentId,
    Guid ApproverUserId
);