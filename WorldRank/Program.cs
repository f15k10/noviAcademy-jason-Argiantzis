using WorldRank;
using System.Linq;

var players = new List<Player>();
InMemoryWalletRepository walletRepository = new InMemoryWalletRepository();
InMemoryPlayerRepository playerRepository = new InMemoryPlayerRepository(players);
while (true)
{
    Console.WriteLine("\n=== WorldRank Player Registry ===");
    Console.WriteLine("1. Add player");
    Console.WriteLine("2. List all players");
    Console.WriteLine("3. Find player by name");
    Console.WriteLine("0. Exit");
    Console.Write("> ");

    Action? action = Console.ReadLine() switch
    {
        "1" => AddPlayer,
        "2" => ListPlayers,
        "3" => FindPlayer,
        "0" => null,
        _ => () => Console.WriteLine("Unknown option.")
    };

    if (action is null)
        return; // "0" selected — exit

    action();
}

void AddPlayer()
{
    Console.Write("Name: ");
    var name = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(name))
    {
        Console.WriteLine("Name cannot be empty.");
        return;
    }

    Console.Write("Score: ");
    var scoreInput = Console.ReadLine();

    if (!int.TryParse(scoreInput, out var score))
    {
        Console.WriteLine("Score must be a whole number.");
        return;
    }
    Console.Write("Wallet Balance: ");
    decimal balance = Console.Read();
    //Later check functions

    Console.Write("Currency: ");
    //For now i use only those 3 currencies, but in the future i will add more currencies
    Currency currency = Console.ReadLine() switch
    {
        "USD" => Currency.USD,
        "EUR" => Currency.EUR,
        "GBP" => Currency.GBP,
        _ => Currency.USD // Default to USD if input is invalid
    };

    Dictionary<Currency,Wallet> wallet= new Dictionary<Currency, Wallet>();

    //Pass the values to the wallet dictionary, and then pass the dictionary to the player constructor
    wallet.Add(currency, new Wallet(balance, currency, false));
    
    var player = new Player(name, wallet);

   

    player.UpdateScore(score);

    playerRepository.AddPlayer(player);

    Console.WriteLine("Player added successfully.");
}

void ListPlayers()
{
    if (players.Count == 0)
    {
        Console.WriteLine("No players registered.");
        return;
    }

    foreach (var p in players)
        Console.WriteLine(p);
}

void FindPlayer()
{
    Console.Write("Search by name: ");
    var term = Console.ReadLine() ?? string.Empty;

    var player = players
            .FirstOrDefault(p => p.Name.Equals(term, StringComparison.OrdinalIgnoreCase));

    if (player is null)
    {
        Console.WriteLine("No player found.");
        return;
    }

    Console.WriteLine(player);
}

