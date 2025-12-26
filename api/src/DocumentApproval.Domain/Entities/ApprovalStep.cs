using DocumentApproval.Domain.Enums;
using DocumentApproval.Domain.Exception;

namespace DocumentApproval.Domain.Entities;

public class ApprovalStep
{
    private ApprovalStep() { } // EF

    public ApprovalStep(int stepOrder, Guid approverUserId)
    {
        Id = Guid.NewGuid();
        StepOrder = stepOrder;
        ApproverUserId = approverUserId;
        Status = StepStatus.Pending;
    }    
    
    public Guid Id { get; set; }
    public Guid DocumentId { get; set; }
    public int StepOrder { get; set; }
    public Guid ApproverUserId { get; set; }
    public StepStatus Status { get; set; }
    public DateTime? DecidedAt { get; set; }
    
    public void Approve(Guid userId)
    {
        EnsurePending(userId);

        Status = StepStatus.Approved;
        DecidedAt = DateTime.UtcNow;
    }

    public void Reject(Guid userId)
    {
        EnsurePending(userId);

        Status = StepStatus.Rejected;
        DecidedAt = DateTime.UtcNow;
    }

    private void EnsurePending(Guid userId)
    {
        if (ApproverUserId != userId)
            throw new DomainException("User is not authorized for this step.");

        if (Status != StepStatus.Pending)
            throw new DomainException("Step has already been decided.");
    }    
}