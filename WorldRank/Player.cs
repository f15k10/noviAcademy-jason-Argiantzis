namespace WorldRank;


interface IPlayer
{
    Guid Id { get; }
    string Name { get; set; }
    int Score { get; set; }
}

public class Player : IPlayer
{
    public Guid Id { get; }
    public string Name { get; set; }
    public int Score { get;  set; }

    public Dictionary<Currency,Wallet> wallet { get; set; }

    public Player(string name , Dictionary<Currency, Wallet> wallet)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name cannot be null or empty.", nameof(name));

        Id = Guid.NewGuid();
        Name = name;
        this.wallet = wallet;

    }

    public void UpdateScore(int newScore)
    {
        if (newScore < 0)
            throw new ArgumentOutOfRangeException(nameof(newScore), "Score cannot be negative.");

        Score = newScore;
    }

    public override string ToString() =>
            $"[{Id}] {Name} - Score: {Score}";
}
