namespace DocumentApproval.Application.Abstractions;

public interface IUnitOfWork
{
    /// <summary>
    /// Commits all changes made to aggregates within a transaction.
    /// </summary>
    Task CommitAsync(CancellationToken cancellationToken);    
}