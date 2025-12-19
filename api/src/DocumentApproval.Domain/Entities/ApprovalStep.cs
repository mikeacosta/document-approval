using DocumentApproval.Domain.Enums;

namespace DocumentApproval.Domain.Entities;

public class ApprovalStep
{
    public Guid Id { get; set; }
    public Guid DocumentId { get; set; }
    public int StepOrder { get; set; }
    public Guid ApproverUserId { get; set; }
    public StepStatus Status { get; set; }
    public DateTime? DecidedAt { get; set; }
}