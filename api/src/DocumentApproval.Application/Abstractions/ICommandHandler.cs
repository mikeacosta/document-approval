namespace DocumentApproval.Application.Abstractions;

public interface ICommandHandler<TCommand>
{
    Task HandleAsync(TCommand command, CancellationToken cancellationToken = default);
}