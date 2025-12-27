using DocumentApproval.Domain.Enums;
using DocumentApproval.Domain.Exception;

namespace DocumentApproval.Domain.Entities;

public class Document
{
    private readonly List<ApprovalStep> _approvalSteps = new();
    
    private Document() { } // EF

    public Document(string title, IEnumerable<ApprovalStep> steps)
    {
        Id = Guid.NewGuid();
        Title = title;
        Status = DocumentStatus.Draft;
        CreatedAt = DateTime.UtcNow;

        _approvalSteps = steps
            .OrderBy(s => s.StepOrder)
            .ToList();
    }    
    
    public Guid Id { get; private set; }
    public string Title { get; private set; } = default!;
    public DocumentStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    
    public IReadOnlyCollection<ApprovalStep> ApprovalSteps => _approvalSteps;
    
    // Optimistic concurrency (EF Core will populate this)
    public byte[] RowVersion { get; private set; } = Array.Empty<byte>();
    
    public void Submit()
    {
        if (Status != DocumentStatus.Draft)
            throw new DomainException("Only draft documents can be submitted.");

        Status = DocumentStatus.InReview;
    }
    
    public void Approve(Guid approverUserId)
    {
        EnsureInReview();

        var currentStep = GetCurrentStep();

        currentStep.Approve(approverUserId);

        if (_approvalSteps.All(s => s.Status == StepStatus.Approved))
            Status = DocumentStatus.Approved;
    }

    public void Reject(Guid approverUserId)
    {
        EnsureInReview();

        var currentStep = GetCurrentStep();
        currentStep.Reject(approverUserId);

        Status = DocumentStatus.Rejected;
    }

    private ApprovalStep GetCurrentStep()
    {
        var step = _approvalSteps
            .OrderBy(s => s.StepOrder)
            .FirstOrDefault(s => s.Status == StepStatus.Pending);

        if (step is null)
            throw new DomainException("No pending approval step.");

        return step;
    }

    private void EnsureInReview()
    {
        if (Status != DocumentStatus.InReview)
            throw new DomainException("Document is not in review.");
    }    
}