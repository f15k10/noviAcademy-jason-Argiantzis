namespace WorldRank;


interface IPlayer
{
    int Id { get; set; }
    string Name { get; set; }
    int Score { get; set; }
}

public class Player : IPlayer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Score { get;  set; }

    public Dictionary<Currency,Wallet> _wallet { get; set; }
    public Player(int id , string name) {
       Name = name;
        Id = id;
    }
    public Player(int id ,string name , Dictionary<Currency, Wallet> wallet)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name cannot be null or empty.", nameof(name));

        Id = id;
        Name = name;
        _wallet = wallet;

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
