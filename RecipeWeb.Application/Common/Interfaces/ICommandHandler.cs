using MediatR;

namespace RecipeWeb.Application.Common.Interfaces;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand>
    where TCommand : ICommand
{
}
