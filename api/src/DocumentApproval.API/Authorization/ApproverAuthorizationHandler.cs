using System.Security.Claims;
using DocumentApproval.Application.Abstractions;
using Microsoft.AspNetCore.Authorization;

namespace DocumentApproval.API.Authorization;

public sealed class ApproverAuthorizationHandler : AuthorizationHandler<ApproverRequirement>
{
    private readonly IDocumentRepository _repository;

    public ApproverAuthorizationHandler(IDocumentRepository repository)
    {
        _repository = repository;
    }
    
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        ApproverRequirement requirement)
    {
        var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim is null)
            return;

        if (context.Resource is not HttpContext httpContext)
            return;

        if (!httpContext.Request.RouteValues.TryGetValue("id", out var idValue))
            return;

        if (!Guid.TryParse(idValue?.ToString(), out var documentId))
            return;

        var document = await _repository.GetByIdAsync(documentId, httpContext.RequestAborted);
        if (document is null)
            return;

        if (document.IsCurrentApprover(Guid.Parse(userIdClaim.Value)))
        {
            context.Succeed(requirement);
        }
    }
}