using NoviCode.Domain.Entity;

namespace NoviCode.Application.Interfaces
{
    public interface ICreatePlayerPersistence
    {
        Task Persist(Player player);
    }
}
