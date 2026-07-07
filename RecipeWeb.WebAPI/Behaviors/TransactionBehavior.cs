using MediatR;
using RecipeWeb.Application.Common.Interfaces;

namespace RecipeWeb.WebAPI.Behaviors;

public class TransactionBehavior<TCommand, TResponse>(IUnitOfWork unitOfWork) : IPipelineBehavior<TCommand, TResponse>
    where TCommand : ICommand // только команды
{
    /// <inheritdoc/>
    public async Task<TResponse> Handle(
        TCommand command,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        // Выполняем команду
        TResponse? response = await next(cancellationToken);

        // Сохраняем изменения
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return response;
    }
}
