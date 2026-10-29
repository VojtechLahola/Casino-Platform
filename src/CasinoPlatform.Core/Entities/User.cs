namespace CasinoPlatform.Core.Entities;

public class User
{
    public Guid Id { get; set; }

    public string Username { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public decimal Balance { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<GameLog> GameLogs { get; set; } = new List<GameLog>();

    public ICollection<ActiveGame> ActiveGames { get; set; } = new List<ActiveGame>();
}
