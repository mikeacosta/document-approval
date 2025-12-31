namespace DocumentApproval.Application.Common.Errors;

/// <summary>
/// Thrown when an entity requested by the application cannot be found.
/// </summary>
public sealed class NotFoundException : Exception
{
    public NotFoundException()
    {
    }

    public NotFoundException(string message)
        : base(message)
    {
    }

    public NotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }    
}