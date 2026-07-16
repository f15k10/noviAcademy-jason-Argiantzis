
using NoviCode.Domain.Entity;
using NoviCode.Domain.Enums;
using NoviCode.Domain.Exceptions;
using System.ComponentModel;

namespace NoviCode.Tests.Entites
{
    public class WalletTests
    {
        [Fact( DisplayName="Verify the constructor is Initializing")]
        public void Constructor_AllPropertiesInitializedCorrectly()
        {
            // Arrange
            int id = 0;
            int playerId = 123;
            Currency currency = Currency.EUR;
            decimal balance = 121.21m;
            bool isBlocked = true;
            // Act
            var wallet = new Wallet(id, playerId, currency, balance, isBlocked);
            // Assert

            Assert.Equal(id,wallet.Id);
            Assert.Equal(playerId, wallet.PlayerId);
            Assert.Equal(currency, wallet.Currency);
            Assert.Equal(balance, wallet.Balance);
            Assert.Equal(isBlocked, wallet.IsBlocked);

        }

        [Theory (DisplayName = "Checking InvalidAmountException if it works ")]
        [InlineData(0)]
        [InlineData(-200)]
        [InlineData(-100)]
        public void Deposit_NotPositiveAmount_ThrowsInvalidAmountException(int amount2)
        {
            //Arrange
            var playerId = 1;
            Currency currency = Currency.EUR;
            var wallet = new Wallet(playerId,currency);

   
            Assert.Throws<InvalidAmountException>(() => wallet.Deposit(amount2));
        }
    }
}
