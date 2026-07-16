using NoviCode.Domain.Entity;

namespace NoviCode.API.DTO
{
    public record CreatePlayerRequest(string Name,int Score);

    public record PlayerResponse(int id,string Name,int Score)
    {
        public static PlayerResponse From(Player player)
            => new(player.Id, player.Name, player.Score);
    }

}
