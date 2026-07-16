
using Application.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using NoviCode.Application.Cache;
using NoviCode.Application.Service;
using NoviCode.Domain.Entity;

namespace NoviCode.Tests.Services
{
    public class PlayerServicesTest
    {
        private readonly Mock<IPlayerRepository> _playerRepositoryMock = new();
        private readonly Mock<ICache> _cacheMock = new();
        private readonly Mock<ILogger<PlayerService>> _loggerMock = new();

        private readonly PlayerService _sut;

        public PlayerServicesTest()
        { 
           _sut  = new PlayerService(_playerRepositoryMock.Object,_cacheMock.Object,_loggerMock.Object);
        }

        [Fact]
        public async Task GetById_IdExists_ReturnsPlayer()
        {
            // Arrange
            //  _cacheMock.Setup(mock => mock.TryGet(It.IsAny<string>(),);
            var id = 1;
            var name = "Koulouris";
            var score = 120;
            var expexctedPlayer = new Player
                (
                 id,
                 name,
                 score
                );
            _playerRepositoryMock.Setup(mock => mock.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(expexctedPlayer);
            //Act
            var player = await _sut.GetByIdAsync(1, CancellationToken.None);

            //Assert
            Assert.Equal(expexctedPlayer.Id, player?.Id);
        }
    }
}
