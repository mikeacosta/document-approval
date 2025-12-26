using DocumentApproval.Domain.Entities;
using DocumentApproval.Domain.Exception;

namespace DocumentApproval.Domain.Tests;

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
}