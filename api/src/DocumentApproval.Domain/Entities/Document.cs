using DocumentApproval.Domain.Enums;

namespace DocumentApproval.Domain.Entities;

public class Document
{
    public Guid Id { get; set; }
    public string Title { get; private set; } = String.Empty;
    public DocumentStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<ApprovalStep> ApprovalSteps { get; set; } = new();
}