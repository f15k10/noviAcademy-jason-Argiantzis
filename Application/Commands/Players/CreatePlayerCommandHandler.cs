using MediatR;

using NoviCode.Application.Interfaces;
using NoviCode.Domain.Entity;

namespace NoviCode.Application.Commands.Players
{
    public class CreatePlayerCommandHandler : IRequestHandler<CreatePlayerCommand,int>
    {
        private readonly ICreatePlayerPersistence _createPlayerPersistence;

        public CreatePlayerCommandHandler(ICreatePlayerPersistence createPlayerPersistence)
        {
            _createPlayerPersistence = createPlayerPersistence;
        }

        public async Task<int> Handle(CreatePlayerCommand request,CancellationToken cancellationToken)
        { 
            var player = Player.CreateNew(request.name);

            await _createPlayerPersistence.Persist(player);
            return player.Id;
        }
    }
}
