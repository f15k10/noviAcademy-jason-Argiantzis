using MediatR;

namespace NoviCode.Application.Commands.Players
{
    public record CreatePlayerCommand(string name, int score) : IRequest<int>;
}
