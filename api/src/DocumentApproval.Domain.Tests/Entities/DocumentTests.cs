using DocumentApproval.Domain.Entities;
using DocumentApproval.Domain.Exception;

namespace DocumentApproval.Domain.Tests.Entities;

public class DocumentTests
{
    [Fact]
    public void Cannot_approve_step_out_of_order()
    {
        // Arrange
        var approver1 = Guid.NewGuid();
        var approver2 = Guid.NewGuid();

        var steps = new[]
        {
            new ApprovalStep(stepOrder: 1, approverUserId: approver1),
            new ApprovalStep(stepOrder: 2, approverUserId: approver2)
        };

        var document = new Document("Test Document", steps);
        document.Submit();

        // Act
        var act = () => document.Approve(approver2);

        // Assert
        var exception = Assert.Throws<DomainException>(act);
        Assert.Equal("User is not authorized for this step.", exception.Message);
    }   
    
    [Fact]
    public void IsCurrentApprover_returns_true_for_current_pending_step_approver()
    {
        // Arrange
        var approverId = Guid.NewGuid();
        var otherApproverId = Guid.NewGuid();

        var steps = new[]
        {
            new ApprovalStep(1, approverId),       // StepOrder = 1
            new ApprovalStep(2, otherApproverId)  // StepOrder = 2
        };

        var document = new Document("Test Document", steps);

        // Act
        document.Submit(); // Status becomes InReview

        // Assert
        Assert.True(document.IsCurrentApprover(approverId));
    }    
}