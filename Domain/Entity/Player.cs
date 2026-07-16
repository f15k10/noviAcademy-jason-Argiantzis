namespace NoviCode.Domain.Entity;

public class Player : IPlayer
{
	public int Id { get; }
	public string Name { get; private set; }
	public int Score { get; private set; }
    private static readonly Random Random = new();
    public Player(int id, string name , int score)
	{
		if (string.IsNullOrWhiteSpace(name))
			throw new ArgumentException("Name cannot be empty.", nameof(name));

		Id = id;
		Name = name;
		Score = score;
	}

    public Player(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty.", nameof(name));

        Id = GenerateRandomInt(1, 9999);
        Name = name;

    }

	public static Player CreateNew(String name)
	{
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty.", nameof(name));

        return new Player(GenerateRandomInt(1, 9999), name,0);
    }

    public static int GenerateRandomInt(int min, int max)
    {
        return Random.Next(min, max + 1);
    }
    public void AddScore(int points)
	{
		if (points < 0)
			throw new ArgumentOutOfRangeException(nameof(points), "Points cannot be negative.");

		Score += points;
	}

	public override string ToString() => $"[{Id}] {Name} - Score: {Score}";
}
